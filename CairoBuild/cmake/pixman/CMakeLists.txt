cmake_minimum_required(VERSION 3.0)
project(pixman VERSION 0.32.0 LANGUAGES C)

set(CMAKE_WINDOWS_EXPORT_ALL_SYMBOLS ON)
set(CMAKE_DEBUG_POSTFIX "d")

include_directories("pixman")

file(GLOB SOURCES
"pixman/pixman.c"
"pixman/pixman-access.c"
"pixman/pixman-access-accessors.c"
"pixman/pixman-bits-image.c"
"pixman/pixman-combine32.c"
"pixman/pixman-combine-float.c"
"pixman/pixman-conical-gradient.c"
"pixman/pixman-filter.c"
"pixman/pixman-x86.c"
"pixman/pixman-mips.c"
"pixman/pixman-arm.c"
"pixman/pixman-ppc.c"
"pixman/pixman-edge.c"
"pixman/pixman-edge-accessors.c"
"pixman/pixman-fast-path.c"
"pixman/pixman-glyph.c"
"pixman/pixman-general.c"
"pixman/pixman-gradient-walker.c"
"pixman/pixman-image.c"
"pixman/pixman-implementation.c"
"pixman/pixman-linear-gradient.c"
"pixman/pixman-matrix.c"
"pixman/pixman-noop.c"
"pixman/pixman-radial-gradient.c"
"pixman/pixman-region16.c"
"pixman/pixman-region32.c"
"pixman/pixman-solid-fill.c"
"pixman/pixman-timer.c"
"pixman/pixman-trap.c"
"pixman/pixman-utils.c"
"pixman/pixman-sse2.c"
)

add_library(pixman-1 ${SOURCES})

# pixman requires the three PACKAGE* definitions in order to compile. The USE_SSE2 definition lets it use SSE2 instructions for speed. Every target machine should have SSE2 these days.
target_compile_definitions(pixman-1
    PUBLIC
        PACKAGE="pixman-1"
        PACKAGE_VERSION="0.34.0"
        PACKAGE_BUGREPORT=""
    PRIVATE
        USE_SSE2
)

# pixman produces a lot of warnings which are disabled here because they otherwise fill up the log files
if(MSVC)
    target_compile_options(pixman-1 PRIVATE "/wd4244" "/wd4146" "/wd4996") #  PUBLIC "/D_CRT_SECURE_NO_WARNINGS"
endif()

set_target_properties(pixman-1 PROPERTIES PUBLIC_HEADER "pixman/pixman.h;pixman/pixman-accessor.h;pixman/pixman-combine32.h;pixman/pixman-compiler.h;pixman/pixman-edge-imp.h;pixman/pixman-inlines.h;pixman/pixman-private.h;pixman/pixman-version.h")

install(TARGETS pixman-1
  RUNTIME DESTINATION bin
  LIBRARY DESTINATION lib
  ARCHIVE DESTINATION lib
  PUBLIC_HEADER DESTINATION include
)
