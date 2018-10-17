#!/usr/bin/python
# -*- coding: utf-8 -*-

import sys
import pathlib
from clang import cindex

HERE = pathlib.Path(__file__).absolute().parent

CAIRO_H = HERE / "../CairoBuild/install/include/cairo.h"
CS_PATH = HERE / "../UnityProject/Assets/UnityCairo/CairoDllImport.cs"


class Function:
    def __init__(self, name, result_type, params):
        self.name = name
        self.result_type = result_type
        self.params = params

    def resolve_types(self, types):
        if self.result_type in types:
            if types[self.result_type] == cindex.TypeKind.POINTER:
                return
            self.result_type = types[self.result_type]

    def __str__(self):
        param_strings = (f'{x}: {y}' for x, y in self.params)
        return f'{self.name}({", ".join(param_strings)}) => {self.result_type}'


class Enum:
    def __init__(self, name, values):
        self.name = name
        self.values = values

    def get_csharp_values(self, indent):
        mod = False
        for i, (k, v) in enumerate(self.values):
            if i != v:
                mod = True
                break
        if mod:
            return ''.join(f'{indent}{k} = {v},\n' for k, v in self.values)
        else:
            return ''.join(f'{indent}{k},\n' for k, v in self.values)


class Parser:

    def __init__(self):
        self.functions = []
        self.types = {
            'int': cindex.TypeKind.INT,
            'long': cindex.TypeKind.LONG,
            'unsigned int': cindex.TypeKind.UINT,
            'unsigned long': cindex.TypeKind.ULONG,
            'float': cindex.TypeKind.FLOAT,
            'double': cindex.TypeKind.DOUBLE,
            # 'const char *': False,
        }
        self.enums = []

    def parse(self, path: pathlib.Path, libclang: str):
        cindex.Config.set_library_file(libclang)
        index = cindex.Index.create()
        translation_unit = index.parse(str(path), ['-x', 'c++'])
        self.traverse(translation_unit.cursor)

    def process_function(self, cursor: cindex.Cursor):
        name = cursor.spelling if len(cursor.spelling) > 0 else 'no_name'

        params = []
        for child in cursor.get_children():
            if child.kind == cindex.CursorKind.DLLIMPORT_ATTR:
                pass

            elif child.kind == cindex.CursorKind.PARM_DECL:
                if child.type.spelling not in self.types:
                    if child.type.kind == cindex.TypeKind.POINTER:
                        pass
                    else:
                        #raise Exception(f'unknown type in {name} params: {child.type.spelling}')
                        pass
                params.append((child.spelling, child.type.spelling))

            elif child.kind == cindex.CursorKind.TYPE_REF:
                pass

            else:

                print(f'unknown kind in {name}: {child.kind}')
                pass

        # if cursor.result_type.spelling not in self.types:
        #    raise Exception(f'unknown type: {cursor.result_type.spelling}')

        self.functions.append(
            Function(name, cursor.result_type.spelling, params))

    def process_typedef(self, cursor: cindex.Cursor):
        if cursor.spelling in self.types:
            raise Exception(f'already exists: {cursor.spelling}')
        # if cursor.underlying_typedef_type.spelling not in self.types:
        #    raise Exception(f'unknown type: {cursor.underlying_typedef_type.spelling}')
        if cursor.underlying_typedef_type.kind == cindex.TypeKind.ELABORATED:
            for e in self.enums:
                if 'enum ' + e.name == cursor.underlying_typedef_type.spelling:
                    e.name = cursor.spelling
                    break
        elif cursor.underlying_typedef_type.spelling.startswith('enum '):
            pass
        else:
            self.types[cursor.spelling] = cursor.underlying_typedef_type.kind

    def process_enum(self, cursor: cindex.Cursor):
        self.enums.append(Enum(cursor.spelling,
                               [(x.spelling, x.enum_value)
                                for x in cursor.get_children()]
                               ))

    def process_struct(self, cursor: cindex.Cursor):
        if cursor.spelling in self.types:
            raise Exception(f'already exists: {cursor.spelling}')
        self.types[f'struct {cursor.spelling}'] = True

    def process_union(self, cursor: cindex.Cursor):
        name = cursor.spelling if len(cursor.spelling) > 0 else 'no_name'
        #print(f'union {name}')

    def traverse(self, cursor: cindex.Cursor, level: int = 0):

        if cursor.kind == cindex.CursorKind.FUNCTION_DECL:
            self.process_function(cursor)

        elif cursor.kind == cindex.CursorKind.TYPEDEF_DECL:
            self.process_typedef(cursor)

        elif cursor.kind == cindex.CursorKind.ENUM_DECL:
            self.process_enum(cursor)

        elif cursor.kind == cindex.CursorKind.STRUCT_DECL:
            self.process_struct(cursor)

        elif cursor.kind == cindex.CursorKind.UNION_DECL:
            self.process_union(cursor)

        elif (cursor.kind == cindex.CursorKind.TRANSLATION_UNIT
                or cursor.kind == cindex.CursorKind.UNEXPOSED_DECL):

            # print(f'{"  " * level}{cursor.spelling}: {cursor.kind}')
            for child in cursor.get_children():
                self.traverse(child, level+1)

        else:
            print(f'unknown: {cursor.kind}')
            return


def main(cairo_h, dll):
    parser = Parser()
    parser.parse(cairo_h, dll)

    ptr_types = [
        'void',
        # 'cairo_matrix_t',
        'cairo_t',
        'cairo_surface_t',
        'const cairo_user_data_key_t',
        'cairo_pattern_t',
        'cairo_rectangle_list_t',
        'cairo_glyph_t',
        'cairo_text_cluster_t',
        'cairo_font_options_t',
        'cairo_font_face_t',
        'cairo_scaled_font_t',
        # 'cairo_text_extents_t',
        'cairo_font_extents_t',
        'cairo_path_t',
        'cairo_device_t',
        'cairo_rectangle_int_t',
        'cairo_rectangle_t',
        'cairo_region_t',
    ]

    def ctype2cs(t):
        if type(t) is cindex.TypeKind:
            return t.spelling.lower()
        if t == 'unsigned int':
            return 'uint'
        if t == 'unsigned long':
            return 'uint'
        if t == 'const char *':
            return 'string'

        if t == 'int *':
            return 'ref int'
        if t == 'unsigned int *':
            return 'ref uint'
        if t == 'unsigned long *':
            return 'ref uint'
        if t == 'const double *':
            return 'ref double'
        if t == 'double *':
            return 'ref double'

        if t == 'const unsigned char *':
            return 'byte[]'
        if t == 'unsigned char *':
            return 'byte[]'
        if t == 'const unsigned char **':
            return 'ref byte[]'

        if t == 'cairo_surface_t **':
            return 'ref IntPtr'
        if t == 'cairo_text_extents_t *':
            return 'ref cairo_text_extents_t'
        if t == 'cairo_matrix_t *':
            return 'ref cairo_matrix_t'
        if t == 'const cairo_matrix_t *':
            return 'ref cairo_matrix_t'

        if t.endswith(' *'):
            if t[:-2] in ptr_types:
                return 'IntPtr'
            if t.startswith('const '):
                if t[6:-2] in ptr_types:
                    return 'IntPtr'

        return t

    skip_functions = [
        'cairo_set_user_data',
        'cairo_surface_write_to_png_stream',
        'cairo_surface_set_mime_data',
        'cairo_surface_set_user_data',
        'cairo_image_surface_create_from_png_stream',
        'cairo_raster_source_pattern_set_acquire',
        'cairo_raster_source_pattern_get_acquire',
        'cairo_raster_source_pattern_set_snapshot',
        'cairo_raster_source_pattern_get_snapshot',
        'cairo_raster_source_pattern_set_copy',
        'cairo_raster_source_pattern_get_copy',
        'cairo_raster_source_pattern_set_finish',
        'cairo_raster_source_pattern_get_finish',
        'cairo_pattern_set_user_data',
        'cairo_font_face_set_user_data',
        'cairo_scaled_font_set_user_data',
        'cairo_user_font_face_set_init_func',
        'cairo_user_font_face_set_render_glyph_func',
        'cairo_user_font_face_set_text_to_glyphs_func',
        'cairo_user_font_face_set_unicode_to_glyph_func',
        'cairo_user_font_face_get_init_func',
        'cairo_user_font_face_get_render_glyph_func',
        'cairo_user_font_face_get_text_to_glyphs_func',
        'cairo_user_font_face_get_unicode_to_glyph_func',
        'cairo_device_set_user_data',
        'cairo_surface_create_observer',
        'cairo_surface_observer_add_paint_callback',
        'cairo_surface_observer_add_mask_callback',
        'cairo_surface_observer_add_fill_callback',
        'cairo_surface_observer_add_stroke_callback',
        'cairo_surface_observer_add_glyphs_callback',
        'cairo_surface_observer_add_flush_callback',
        'cairo_surface_observer_add_finish_callback',
        'cairo_surface_observer_print',
        'cairo_device_observer_print',
        'cairo_scaled_font_text_to_glyphs',
    ]

    def csharp_function(f, types):
        if f.name in skip_functions:
            return ''

        f.resolve_types(types)

        params = (
            f'{ctype2cs(param_type)} {param_name}' for param_name, param_type in f.params)

        return f'''
        [DllImport(DllName)]
        public extern static {ctype2cs(f.result_type)} {f.name}({", ".join(params)});
'''

    def csharp_enum(e):
        return f'''
        public enum {e.name}
        {{
{e.get_csharp_values('            ')}
        }}
'''

    def csharp_method(f, types):
        if f.name in skip_functions:
            return ''

        if not f.name.startswith('cairo_'):
            return ''

        if len(f.params) == 0:
            return ''

        if f.params[0][1] != 'cairo_t *':
            return ''

        params = (f'{ctype2cs(param_type)} {param_name}' for param_name,
                  param_type in f.params[1:])

        def get_name(param_name, param_type):
            cs = ctype2cs(param_type)
            if not cs.startswith('ref '):
                return param_name
            return f'ref {param_name}'

        param_names = (
            f', {get_name(param_name, param_type)}' for param_name, param_type in f.params[1:])

        return f'''
        public {ctype2cs(f.result_type)} {f.name[6:]}({", ".join(params)}){{
            {'' if f.result_type == 'void' else 'return'} Dll.{f.name}(Ptr{''.join(param_names)});
        }}
'''

    using = ''.join(f'using {u};\n' for u in [
        'System',
        'System.Runtime.InteropServices',
    ])

    source = using + '''


namespace UnityCairo
{
    %s

    [StructLayout(LayoutKind.Sequential)]
    public struct cairo_text_extents_t {
        public double x_bearing;
        public double y_bearing;
        public double width;
        public double height;
        public double x_advance;
        public double y_advance;
    };

    [StructLayout(LayoutKind.Sequential)]
    public struct cairo_matrix_t {
        double xx; double yx;
        double xy; double yy;
        double x0; double y0;
    };

    public static class Dll
    {
        const string DllName = "cairo";

        %s
    }

    class Cairo : IDisposable
    {
        public IntPtr Ptr
        {
            get;
            private set;
        }

        Cairo(IntPtr p)
        {
            Ptr = p;
        }

        public static Cairo Create(Surface surface)
        {
            var p = Dll.cairo_create(surface.Ptr);
            if (p == IntPtr.Zero) return null;

            return new Cairo(p);
        }

        #region methods
        %s
        #endregion

        #region IDisposable Support
        private bool disposedValue = false; // 重複する呼び出しを検出するには

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: マネージド状態を破棄します (マネージド オブジェクト)。
                }

                // TODO: アンマネージド リソース (アンマネージド オブジェクト) を解放し、下のファイナライザーをオーバーライドします。
                // TODO: 大きなフィールドを null に設定します。
                if (Ptr != IntPtr.Zero)
                {
                    Dll.cairo_destroy(Ptr);
                    Ptr = IntPtr.Zero;
                }

                disposedValue = true;
            }
        }

        // TODO: 上の Dispose(bool disposing) にアンマネージド リソースを解放するコードが含まれる場合にのみ、ファイナライザーをオーバーライドします。
        // ~Cairo() {
        //   // このコードを変更しないでください。クリーンアップ コードを上の Dispose(bool disposing) に記述します。
        //   Dispose(false);
        // }

        // このコードは、破棄可能なパターンを正しく実装できるように追加されました。
        public void Dispose()
        {
            // このコードを変更しないでください。クリーンアップ コードを上の Dispose(bool disposing) に記述します。
            Dispose(true);
            // TODO: 上のファイナライザーがオーバーライドされる場合は、次の行のコメントを解除してください。
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
''' % (
        ''.join(csharp_enum(e) for e in parser.enums if len(e.name) > 0),
        ''.join(csharp_function(f, parser.types) for f in parser.functions),
        ''.join(csharp_method(f, parser.types) for f in parser.functions)
    )

    if not CS_PATH.parent.is_dir():
        CS_PATH.parent.mkdir()

    with open(CS_PATH, 'w', encoding='utf-8') as io:
        io.write(source)


if __name__ == '__main__':
    if len(sys.argv) == 1:
        print("require libclang.dll path. "
              "ex 'C:/Program Files/LLVM/bin/libclang.dll'")
        sys.exit(1)

    if not CAIRO_H.exists():
        print("%s is not found" % CAIRO_H)
        sys.exit(2)

    main(CAIRO_H, sys.argv[1])
