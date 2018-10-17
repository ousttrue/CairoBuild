cmake_minimum_required(VERSION 3.0)
project(cairo C)

# Add include directories
include_directories("."  "src" "src/win32")

file(GLOB SOURCES
"src/cairo-analysis-surface.c"
"src/cairo-arc.c"
"src/cairo-array.c"
"src/cairo-atomic.c"
"src/cairo-base64-stream.c"
"src/cairo-base85-stream.c"
"src/cairo-bentley-ottmann.c"
"src/cairo-bentley-ottmann-rectangular.c"
"src/cairo-bentley-ottmann-rectilinear.c"
"src/cairo-botor-scan-converter.c"
"src/cairo-boxes.c"
"src/cairo-boxes-intersect.c"
"src/cairo.c"
"src/cairo-cache.c"
"src/cairo-clip.c"
"src/cairo-clip-boxes.c"
"src/cairo-clip-polygon.c"
"src/cairo-clip-region.c"
"src/cairo-clip-surface.c"
"src/cairo-color.c"
"src/cairo-composite-rectangles.c"
"src/cairo-compositor.c"
"src/cairo-contour.c"
"src/cairo-damage.c"
"src/cairo-debug.c"
"src/cairo-default-context.c"
"src/cairo-device.c"
"src/cairo-error.c"
"src/cairo-fallback-compositor.c"
"src/cairo-fixed.c"
"src/cairo-font-face.c"
"src/cairo-font-face-twin.c"
"src/cairo-font-face-twin-data.c"
"src/cairo-font-options.c"
"src/cairo-freelist.c"
"src/cairo-freed-pool.c"
"src/cairo-gstate.c"
"src/cairo-hash.c"
"src/cairo-hull.c"
"src/cairo-image-compositor.c"
"src/cairo-image-info.c"
"src/cairo-image-source.c"
"src/cairo-image-surface.c"
"src/cairo-line.c"
"src/cairo-lzw.c"
"src/cairo-matrix.c"
"src/cairo-mask-compositor.c"
"src/cairo-mesh-pattern-rasterizer.c"
"src/cairo-mempool.c"
"src/cairo-misc.c"
"src/cairo-mono-scan-converter.c"
"src/cairo-mutex.c"
"src/cairo-no-compositor.c"
"src/cairo-observer.c"
"src/cairo-output-stream.c"
"src/cairo-paginated-surface.c"
"src/cairo-path-bounds.c"
"src/cairo-path.c"
"src/cairo-path-fill.c"
"src/cairo-path-fixed.c"
"src/cairo-path-in-fill.c"
"src/cairo-path-stroke.c"
"src/cairo-path-stroke-boxes.c"
"src/cairo-path-stroke-polygon.c"
"src/cairo-path-stroke-traps.c"
"src/cairo-path-stroke-tristrip.c"
"src/cairo-pattern.c"
"src/cairo-pen.c"
"src/cairo-polygon.c"
"src/cairo-polygon-intersect.c"
"src/cairo-polygon-reduce.c"
"src/cairo-raster-source-pattern.c"
"src/cairo-recording-surface.c"
"src/cairo-rectangle.c"
"src/cairo-rectangular-scan-converter.c"
"src/cairo-region.c"
"src/cairo-rtree.c"
"src/cairo-scaled-font.c"
"src/cairo-shape-mask-compositor.c"
"src/cairo-slope.c"
"src/cairo-spans.c"
"src/cairo-spans-compositor.c"
"src/cairo-spline.c"
"src/cairo-stroke-dash.c"
"src/cairo-stroke-style.c"
"src/cairo-surface.c"
"src/cairo-surface-clipper.c"
"src/cairo-surface-fallback.c"
"src/cairo-surface-observer.c"
"src/cairo-surface-offset.c"
"src/cairo-surface-snapshot.c"
"src/cairo-surface-subsurface.c"
"src/cairo-surface-wrapper.c"
"src/cairo-time.c"
"src/cairo-tor-scan-converter.c"
"src/cairo-tor22-scan-converter.c"
"src/cairo-clip-tor-scan-converter.c"
"src/cairo-tag-attributes.c"
"src/cairo-tag-stack.c"
"src/cairo-toy-font-face.c"
"src/cairo-traps.c"
"src/cairo-tristrip.c"
"src/cairo-traps-compositor.c"
"src/cairo-unicode.c"
"src/cairo-user-font.c"
"src/cairo-version.c"
"src/cairo-wideint.c"
# win32
"src/win32/cairo-win32-debug.c"
"src/win32/cairo-win32-device.c"
"src/win32/cairo-win32-gdi-compositor.c"
"src/win32/cairo-win32-system.c"
"src/win32/cairo-win32-surface.c"
"src/win32/cairo-win32-display-surface.c"
"src/win32/cairo-win32-printing-surface.c"
"src/win32/cairo-win32-font.c"
# generic font support
"src/cairo-cff-subset.c"
"src/cairo-scaled-font-subsets.c"
"src/cairo-truetype-subset.c"
"src/cairo-type1-fallback.c"
"src/cairo-type1-glyph-names.c"
"src/cairo-type1-subset.c"
"src/cairo-type3-glyph-surface.c"
# pdf
"src/cairo-pdf-interchange.c"
"src/cairo-pdf-operators.c"
"src/cairo-pdf-shading.c"
"src/cairo-pdf-surface.c"
# png
"src/cairo-png.c"
# ps surface
"src/cairo-ps-surface.c"
# deflate source
"src/cairo-deflate-stream.c"
# svg surface
"src/cairo-svg-surface.c"
# script surface
"src/cairo-script-surface.c"
# fontconfig + freetype
"src/cairo-ft-font.c"
)

set(CMAKE_DEBUG_POSTFIX "d")

find_package(ZLIB REQUIRED)
find_package(PNG REQUIRED)

# Make the pixman library available
if(CMAKE_BUILD_TYPE STREQUAL "Debug")
    set(PIXMAN_SUFFIX "d")
endif()
find_library(PIXMAN_LIBRARY NAMES pixman-1${PIXMAN_SUFFIX})

find_package(Freetype CONFIG REQUIRED)

# Cairo needs to be told which features of FreeType are availible
add_definitions(
    -DHAVE_FT_GLYPHSLOT_EMBOLDEN=1
    -DHAVE_FT_LIBRARY_SETLCDFILTER=1
    -DHAVE_FT_GLYPHSLOT_OBLIQUE=1
    -DHAVE_FT_LOAD_SFNT_TABLE=1
    -DHAVE_FT_GET_X11_FONT_FORMAT=1)

## Find FontConfig
#find_library(FONTCONFIG_LIBRARY fontconfig)

add_library(cairo SHARED ${SOURCES})
# cairo produces a lot of warnings which are disabled here because they otherwise fill up the log files
target_compile_options(cairo PUBLIC "/wd4244" PUBLIC "/wd4146" PUBLIC "/wd4312" PUBLIC "/wd4267" 
                             PUBLIC "/wd4996" PUBLIC "/wd4311" PUBLIC "/wd4334" PUBLIC "/wd4101" 
                             PUBLIC "/wd4819"
                             )
target_link_libraries(cairo gdi32 msimg32 user32 ZLIB::ZLIB PNG::PNG freetype ${PIXMAN_LIBRARY} 
#${FONTCONFIG_LIBRARY}
)

set_target_properties(cairo PROPERTIES PUBLIC_HEADER "src/cairo.h;src/cairo-deprecated.h;src/cairo-features.h;src/cairo-pdf.h;src/cairo-ps.h;src/cairo-script.h;src/cairo-svg.h;/cairo-version.h;src/cairo-win32.h;/util/cairo-gobject/cairo-gobject.h;src/cairo-ft.h")

install(TARGETS cairo
  RUNTIME DESTINATION bin
  LIBRARY DESTINATION bin
  ARCHIVE DESTINATION lib
  PUBLIC_HEADER DESTINATION include
)

# GObject support module

#set(CAIRO_GOBJECT_SOURCES
#    "util/cairo-gobject/cairo-gobject-enums.c"
#    "util/cairo-gobject/cairo-gobject-structs.c")

# GObject support sources do not include header with export macro
#if(BUILD_SHARED_LIBS)
#    set_source_files_properties(
#        "util/cairo-gobject/cairo-gobject-enums.c"
#        "util/cairo-gobject/cairo-gobject-structs.c"
#        PROPERTIES COMPILE_DEFINITIONS "cairo_public=__declspec(dllexport)")
#endif()

# Make GLib's GObject available
#find_path(GLIB_INCLUDE_DIR glib.h)
#find_library(GLIB_LIBRARY NAMES glib-2.0)
#find_library(GOBJECT_LIBRARY NAMES gobject-2.0)
#set(GLIB_LIBRARIES ${GLIB_LIBRARY} ${GOBJECT_LIBRARY})

#add_library(cairo-gobject ${CAIRO_GOBJECT_SOURCES}) 
#target_link_libraries(cairo-gobject cairo ${GLIB_LIBRARIES})
#target_include_directories(cairo-gobject PRIVATE ${GLIB_INCLUDE_DIR})
#install(TARGETS cairo-gobject
#    RUNTIME DESTINATION bin
#    LIBRARY DESTINATION lib
#    ARCHIVE DESTINATION lib)
