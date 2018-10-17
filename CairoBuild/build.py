'''
# Cairo build script

## licenses
* zlib: https://www.zlib.net/zlib_license.html
* libpng: src/libpng-1.6.35/LICENSE"
* freetype: https://www.freetype.org/license.html
* pixman
* cairo: https://cairographics.org/ LGPL or MPL

'''
import pathlib
import os
import sys
import urllib.request
import tarfile
import subprocess
import contextlib
import http.client
from typing import Optional, List

from logging import getLogger
logger = getLogger(__name__)

CMD_ENCODING = 'cp932'

HERE = pathlib.Path(__file__).absolute().parent


@contextlib.contextmanager
def pushd(path: pathlib.Path):
    current = os.getcwd()
    try:
        os.chdir(path)
        yield
    finally:
        os.chdir(current)


def get_vswhere()->Optional[pathlib.Path]:
    key = 'PROGRAMFILES(X86)'
    if key in os.environ:
        exe = pathlib.Path(
            os.environ[key]) / 'Microsoft Visual Studio' / 'Installer' / 'vswhere.exe'
        if exe.exists():
            return exe
    return None


def get_cmake()->Optional[pathlib.Path]:
    vswhere = get_vswhere()
    if not vswhere:
        return None

    p = subprocess.Popen([str(vswhere),
                          '-latest',
                          '-products',
                          '*',
                          '-requires',
                          'Microsoft.VisualStudio.Component.VC.CMake.Project',
                          '-property',
                          'installationPath'], stdout=subprocess.PIPE, stderr=subprocess.PIPE)
    stdout, _ = p.communicate()
    decoded = stdout.decode(CMD_ENCODING).strip()
    cmake = pathlib.Path(
        f'{decoded}/Common7/IDE/CommonExtensions/Microsoft/CMake/CMake/bin/cmake.exe')
    if not cmake.exists():
        return None
    return cmake


def get_msbuild()->Optional[pathlib.Path]:
    vswhere = get_vswhere()
    if not vswhere:
        return None

    p = subprocess.Popen([str(vswhere),
                          '-latest',
                          '-products',
                          '*',
                          '-requires',
                          'Microsoft.Component.MSBuild',
                          '-property',
                          'installationPath'], stdout=subprocess.PIPE, stderr=subprocess.PIPE)
    stdout, _ = p.communicate()
    decoded = stdout.decode(CMD_ENCODING).strip()
    msbuild = pathlib.Path(
        f'{decoded}/MSBuild/15.0/Bin/MSBuild.exe')
    if not msbuild.exists():
        return None
    return msbuild


def chunk_report(bytes_so_far, _chunk_size, total_size):
    percent = float(bytes_so_far) / total_size
    percent = round(percent*100, 2)
    sys.stdout.write("Downloaded %d of %d bytes (%0.2f%%)\r" %
                     (bytes_so_far, total_size, percent))

    if bytes_so_far >= total_size:
        sys.stdout.write('\n')


def chunk_read(response: http.client.HTTPResponse, w, report_hook, chunk_size=8192):
    info = response.info()
    total_size = info['Content-Length'].strip()
    total_size = int(total_size)
    bytes_so_far = 0

    while True:
        chunk = response.read(chunk_size)
        bytes_so_far += len(chunk)

        if not chunk:
            break

        report_hook(bytes_so_far, chunk_size, total_size)
        w.write(chunk)

    return bytes_so_far


class Lib:
    def __init__(self, url: str, downloadname: str = None,
                 cmakefile: Optional[pathlib.Path] = None)->None:
        self.url = url
        self.downloadname = downloadname
        self.cmakefile = cmakefile
        self.archive: Optional[pathlib.Path] = None
        self.extracted: Optional[pathlib.Path] = None
        self.build_dir: Optional[pathlib.Path] = None

    def get_name(self)->str:
        basename = os.path.basename(self.url)
        if basename.endswith('.tar.gz'):
            return basename[0:-7]
        elif basename.endswith('.tar.xz'):
            return basename[0:-7]
        elif basename.endswith('.tar.bz2'):
            return basename[0:-8]
        else:
            raise Exception(f'unknown: {basename}')

    def download_if_not_exists(self, download_dir: pathlib.Path)->None:
        if self.downloadname:
            self.archive = download_dir / self.downloadname
        else:
            self.archive = download_dir / os.path.basename(self.url)
        if not self.archive.exists() or self.archive.stat().st_size == 0:
            logger.debug('download: %s\n    => %s', self.url, self.archive)

            req = urllib.request.Request(self.url)
            with urllib.request.urlopen(req) as res:
                with self.archive.open('wb') as f:
                    chunk_read(res, f, chunk_report)

    def extract(self, source_dir: pathlib.Path)->None:
        tf = tarfile.open(self.archive, 'r')

        def get_rootname(names: List[str])->Optional[str]:
            it = iter(names)
            root = next(it).split('/')[0]
            for name in it:
                next_root = name.split('/')[0]
                if root != next_root:
                    return None
            return root

        rootname = get_rootname(tf.getnames())
        if rootname:
            self.extracted = source_dir / rootname
            tf.extractall(source_dir)
        else:
            self.extracted = source_dir / self.get_name()
            if not self.extracted.exists():
                self.extracted.mkdir()
            tf.extractall(self.extracted)
        logger.info('extract to %s', self.extracted)

        if self.cmakefile:
            for f in self.cmakefile.iterdir():
                with f.open() as r:
                    with (self.extracted / f.name).open('w') as w:
                        w.write(r.read())

    def build(self, build_dir: pathlib.Path, install_dir: pathlib.Path,
              cmake: pathlib.Path, msbuild: pathlib.Path)->None:
        if not self.extracted:
            return

        self.build_dir = build_dir / self.extracted.name
        if not self.build_dir.exists():
            self.build_dir.mkdir()

        with pushd(self.build_dir):
            p = subprocess.Popen([str(cmake),
                                  f'-DCMAKE_INSTALL_PREFIX={install_dir}',
                                  '-G',
                                  'Visual Studio 15 2017 Win64',
                                  str(self.extracted)],
                                 stdout=subprocess.PIPE, stderr=subprocess.PIPE)
            stdout, _ = p.communicate()
            decoded = stdout.decode(CMD_ENCODING)
            print(decoded)

            # build
            p = subprocess.Popen([str(msbuild),
                                  'Install.vcxproj',  # str(sln),
                                  '/p:Configuration=Release',
                                  '/p:Platform=x64'
                                  ], stdout=subprocess.PIPE, stderr=subprocess.PIPE)
            stdout, _ = p.communicate()
            decoded = stdout.decode(CMD_ENCODING)
            print(decoded)


LIBS = [
    Lib('https://zlib.net/zlib-1.2.11.tar.gz'),
    Lib('https://downloads.sourceforge.net/libpng/libpng-1.6.35.tar.xz'),
    #Lib('https://download.savannah.gnu.org/releases/freetype/freetype-2.8.1.tar.bz2'),
    Lib('https://download-mirror.savannah.gnu.org/releases/freetype/freetype-2.8.1.tar.bz2'),
    Lib('https://www.cairographics.org/releases/pixman-0.34.0.tar.gz', None, HERE / 'cmake' / 'pixman'),
    Lib('http://cairographics.org/snapshots/cairo-1.15.14.tar.xz',
        None, HERE / 'cmake' / 'cairo'),
]


def main():
    from logging import basicConfig, DEBUG
    basicConfig(
        level=DEBUG,
        datefmt='%H:%M:%S',
        format='%(asctime)s[%(levelname)s][%(name)s.%(funcName)s] %(message)s'
    )

    download_dir = HERE / 'downloads'
    if not download_dir.is_dir():
        download_dir.mkdir()

    source_dir = HERE / 'src'
    if not source_dir.is_dir():
        source_dir.mkdir()

    build_dir = HERE / 'build'
    if not build_dir.is_dir():
        build_dir.mkdir()

    install_dir = HERE / 'install'
    if not install_dir.is_dir():
        install_dir.mkdir()

    cmake = get_cmake()
    if not cmake:
        return

    msbuild = get_msbuild()
    if not msbuild:
        return

    for lib in LIBS:
        lib.download_if_not_exists(download_dir)
        lib.extract(source_dir)
        lib.build(build_dir, install_dir, cmake, msbuild)


if __name__ == '__main__':
    main()
