using System;
using System.Runtime.InteropServices;



namespace UnityCairo
{
    
        public enum cairo_status_t
        {
            CAIRO_STATUS_SUCCESS,
            CAIRO_STATUS_NO_MEMORY,
            CAIRO_STATUS_INVALID_RESTORE,
            CAIRO_STATUS_INVALID_POP_GROUP,
            CAIRO_STATUS_NO_CURRENT_POINT,
            CAIRO_STATUS_INVALID_MATRIX,
            CAIRO_STATUS_INVALID_STATUS,
            CAIRO_STATUS_NULL_POINTER,
            CAIRO_STATUS_INVALID_STRING,
            CAIRO_STATUS_INVALID_PATH_DATA,
            CAIRO_STATUS_READ_ERROR,
            CAIRO_STATUS_WRITE_ERROR,
            CAIRO_STATUS_SURFACE_FINISHED,
            CAIRO_STATUS_SURFACE_TYPE_MISMATCH,
            CAIRO_STATUS_PATTERN_TYPE_MISMATCH,
            CAIRO_STATUS_INVALID_CONTENT,
            CAIRO_STATUS_INVALID_FORMAT,
            CAIRO_STATUS_INVALID_VISUAL,
            CAIRO_STATUS_FILE_NOT_FOUND,
            CAIRO_STATUS_INVALID_DASH,
            CAIRO_STATUS_INVALID_DSC_COMMENT,
            CAIRO_STATUS_INVALID_INDEX,
            CAIRO_STATUS_CLIP_NOT_REPRESENTABLE,
            CAIRO_STATUS_TEMP_FILE_ERROR,
            CAIRO_STATUS_INVALID_STRIDE,
            CAIRO_STATUS_FONT_TYPE_MISMATCH,
            CAIRO_STATUS_USER_FONT_IMMUTABLE,
            CAIRO_STATUS_USER_FONT_ERROR,
            CAIRO_STATUS_NEGATIVE_COUNT,
            CAIRO_STATUS_INVALID_CLUSTERS,
            CAIRO_STATUS_INVALID_SLANT,
            CAIRO_STATUS_INVALID_WEIGHT,
            CAIRO_STATUS_INVALID_SIZE,
            CAIRO_STATUS_USER_FONT_NOT_IMPLEMENTED,
            CAIRO_STATUS_DEVICE_TYPE_MISMATCH,
            CAIRO_STATUS_DEVICE_ERROR,
            CAIRO_STATUS_INVALID_MESH_CONSTRUCTION,
            CAIRO_STATUS_DEVICE_FINISHED,
            CAIRO_STATUS_JBIG2_GLOBAL_MISSING,
            CAIRO_STATUS_PNG_ERROR,
            CAIRO_STATUS_FREETYPE_ERROR,
            CAIRO_STATUS_WIN32_GDI_ERROR,
            CAIRO_STATUS_TAG_ERROR,
            CAIRO_STATUS_LAST_STATUS,

        }

        public enum cairo_content_t
        {
            CAIRO_CONTENT_COLOR = 4096,
            CAIRO_CONTENT_ALPHA = 8192,
            CAIRO_CONTENT_COLOR_ALPHA = 12288,

        }

        public enum cairo_format_t
        {
            CAIRO_FORMAT_INVALID = -1,
            CAIRO_FORMAT_ARGB32 = 0,
            CAIRO_FORMAT_RGB24 = 1,
            CAIRO_FORMAT_A8 = 2,
            CAIRO_FORMAT_A1 = 3,
            CAIRO_FORMAT_RGB16_565 = 4,
            CAIRO_FORMAT_RGB30 = 5,

        }

        public enum cairo_operator_t
        {
            CAIRO_OPERATOR_CLEAR,
            CAIRO_OPERATOR_SOURCE,
            CAIRO_OPERATOR_OVER,
            CAIRO_OPERATOR_IN,
            CAIRO_OPERATOR_OUT,
            CAIRO_OPERATOR_ATOP,
            CAIRO_OPERATOR_DEST,
            CAIRO_OPERATOR_DEST_OVER,
            CAIRO_OPERATOR_DEST_IN,
            CAIRO_OPERATOR_DEST_OUT,
            CAIRO_OPERATOR_DEST_ATOP,
            CAIRO_OPERATOR_XOR,
            CAIRO_OPERATOR_ADD,
            CAIRO_OPERATOR_SATURATE,
            CAIRO_OPERATOR_MULTIPLY,
            CAIRO_OPERATOR_SCREEN,
            CAIRO_OPERATOR_OVERLAY,
            CAIRO_OPERATOR_DARKEN,
            CAIRO_OPERATOR_LIGHTEN,
            CAIRO_OPERATOR_COLOR_DODGE,
            CAIRO_OPERATOR_COLOR_BURN,
            CAIRO_OPERATOR_HARD_LIGHT,
            CAIRO_OPERATOR_SOFT_LIGHT,
            CAIRO_OPERATOR_DIFFERENCE,
            CAIRO_OPERATOR_EXCLUSION,
            CAIRO_OPERATOR_HSL_HUE,
            CAIRO_OPERATOR_HSL_SATURATION,
            CAIRO_OPERATOR_HSL_COLOR,
            CAIRO_OPERATOR_HSL_LUMINOSITY,

        }

        public enum cairo_antialias_t
        {
            CAIRO_ANTIALIAS_DEFAULT,
            CAIRO_ANTIALIAS_NONE,
            CAIRO_ANTIALIAS_GRAY,
            CAIRO_ANTIALIAS_SUBPIXEL,
            CAIRO_ANTIALIAS_FAST,
            CAIRO_ANTIALIAS_GOOD,
            CAIRO_ANTIALIAS_BEST,

        }

        public enum cairo_fill_rule_t
        {
            CAIRO_FILL_RULE_WINDING,
            CAIRO_FILL_RULE_EVEN_ODD,

        }

        public enum cairo_line_cap_t
        {
            CAIRO_LINE_CAP_BUTT,
            CAIRO_LINE_CAP_ROUND,
            CAIRO_LINE_CAP_SQUARE,

        }

        public enum cairo_line_join_t
        {
            CAIRO_LINE_JOIN_MITER,
            CAIRO_LINE_JOIN_ROUND,
            CAIRO_LINE_JOIN_BEVEL,

        }

        public enum cairo_text_cluster_flags_t
        {
            CAIRO_TEXT_CLUSTER_FLAG_BACKWARD = 1,

        }

        public enum cairo_font_slant_t
        {
            CAIRO_FONT_SLANT_NORMAL,
            CAIRO_FONT_SLANT_ITALIC,
            CAIRO_FONT_SLANT_OBLIQUE,

        }

        public enum cairo_font_weight_t
        {
            CAIRO_FONT_WEIGHT_NORMAL,
            CAIRO_FONT_WEIGHT_BOLD,

        }

        public enum cairo_subpixel_order_t
        {
            CAIRO_SUBPIXEL_ORDER_DEFAULT,
            CAIRO_SUBPIXEL_ORDER_RGB,
            CAIRO_SUBPIXEL_ORDER_BGR,
            CAIRO_SUBPIXEL_ORDER_VRGB,
            CAIRO_SUBPIXEL_ORDER_VBGR,

        }

        public enum cairo_hint_style_t
        {
            CAIRO_HINT_STYLE_DEFAULT,
            CAIRO_HINT_STYLE_NONE,
            CAIRO_HINT_STYLE_SLIGHT,
            CAIRO_HINT_STYLE_MEDIUM,
            CAIRO_HINT_STYLE_FULL,

        }

        public enum cairo_hint_metrics_t
        {
            CAIRO_HINT_METRICS_DEFAULT,
            CAIRO_HINT_METRICS_OFF,
            CAIRO_HINT_METRICS_ON,

        }

        public enum cairo_font_type_t
        {
            CAIRO_FONT_TYPE_TOY,
            CAIRO_FONT_TYPE_FT,
            CAIRO_FONT_TYPE_WIN32,
            CAIRO_FONT_TYPE_QUARTZ,
            CAIRO_FONT_TYPE_USER,

        }

        public enum cairo_path_data_type_t
        {
            CAIRO_PATH_MOVE_TO,
            CAIRO_PATH_LINE_TO,
            CAIRO_PATH_CURVE_TO,
            CAIRO_PATH_CLOSE_PATH,

        }

        public enum cairo_device_type_t
        {
            CAIRO_DEVICE_TYPE_DRM = 0,
            CAIRO_DEVICE_TYPE_GL = 1,
            CAIRO_DEVICE_TYPE_SCRIPT = 2,
            CAIRO_DEVICE_TYPE_XCB = 3,
            CAIRO_DEVICE_TYPE_XLIB = 4,
            CAIRO_DEVICE_TYPE_XML = 5,
            CAIRO_DEVICE_TYPE_COGL = 6,
            CAIRO_DEVICE_TYPE_WIN32 = 7,
            CAIRO_DEVICE_TYPE_INVALID = -1,

        }

        public enum cairo_surface_type_t
        {
            CAIRO_SURFACE_TYPE_IMAGE,
            CAIRO_SURFACE_TYPE_PDF,
            CAIRO_SURFACE_TYPE_PS,
            CAIRO_SURFACE_TYPE_XLIB,
            CAIRO_SURFACE_TYPE_XCB,
            CAIRO_SURFACE_TYPE_GLITZ,
            CAIRO_SURFACE_TYPE_QUARTZ,
            CAIRO_SURFACE_TYPE_WIN32,
            CAIRO_SURFACE_TYPE_BEOS,
            CAIRO_SURFACE_TYPE_DIRECTFB,
            CAIRO_SURFACE_TYPE_SVG,
            CAIRO_SURFACE_TYPE_OS2,
            CAIRO_SURFACE_TYPE_WIN32_PRINTING,
            CAIRO_SURFACE_TYPE_QUARTZ_IMAGE,
            CAIRO_SURFACE_TYPE_SCRIPT,
            CAIRO_SURFACE_TYPE_QT,
            CAIRO_SURFACE_TYPE_RECORDING,
            CAIRO_SURFACE_TYPE_VG,
            CAIRO_SURFACE_TYPE_GL,
            CAIRO_SURFACE_TYPE_DRM,
            CAIRO_SURFACE_TYPE_TEE,
            CAIRO_SURFACE_TYPE_XML,
            CAIRO_SURFACE_TYPE_SKIA,
            CAIRO_SURFACE_TYPE_SUBSURFACE,
            CAIRO_SURFACE_TYPE_COGL,

        }

        public enum cairo_pattern_type_t
        {
            CAIRO_PATTERN_TYPE_SOLID,
            CAIRO_PATTERN_TYPE_SURFACE,
            CAIRO_PATTERN_TYPE_LINEAR,
            CAIRO_PATTERN_TYPE_RADIAL,
            CAIRO_PATTERN_TYPE_MESH,
            CAIRO_PATTERN_TYPE_RASTER_SOURCE,

        }

        public enum cairo_extend_t
        {
            CAIRO_EXTEND_NONE,
            CAIRO_EXTEND_REPEAT,
            CAIRO_EXTEND_REFLECT,
            CAIRO_EXTEND_PAD,

        }

        public enum cairo_filter_t
        {
            CAIRO_FILTER_FAST,
            CAIRO_FILTER_GOOD,
            CAIRO_FILTER_BEST,
            CAIRO_FILTER_NEAREST,
            CAIRO_FILTER_BILINEAR,
            CAIRO_FILTER_GAUSSIAN,

        }

        public enum cairo_region_overlap_t
        {
            CAIRO_REGION_OVERLAP_IN,
            CAIRO_REGION_OVERLAP_OUT,
            CAIRO_REGION_OVERLAP_PART,

        }


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

        
        [DllImport(DllName)]
        public extern static int cairo_version();

        [DllImport(DllName)]
        public extern static string cairo_version_string();

        [DllImport(DllName)]
        public extern static IntPtr cairo_create(IntPtr target);

        [DllImport(DllName)]
        public extern static IntPtr cairo_reference(IntPtr cr);

        [DllImport(DllName)]
        public extern static void cairo_destroy(IntPtr cr);

        [DllImport(DllName)]
        public extern static uint cairo_get_reference_count(IntPtr cr);

        [DllImport(DllName)]
        public extern static IntPtr cairo_get_user_data(IntPtr cr, IntPtr key);

        [DllImport(DllName)]
        public extern static void cairo_save(IntPtr cr);

        [DllImport(DllName)]
        public extern static void cairo_restore(IntPtr cr);

        [DllImport(DllName)]
        public extern static void cairo_push_group(IntPtr cr);

        [DllImport(DllName)]
        public extern static void cairo_push_group_with_content(IntPtr cr, cairo_content_t content);

        [DllImport(DllName)]
        public extern static IntPtr cairo_pop_group(IntPtr cr);

        [DllImport(DllName)]
        public extern static void cairo_pop_group_to_source(IntPtr cr);

        [DllImport(DllName)]
        public extern static void cairo_set_operator(IntPtr cr, cairo_operator_t op);

        [DllImport(DllName)]
        public extern static void cairo_set_source(IntPtr cr, IntPtr source);

        [DllImport(DllName)]
        public extern static void cairo_set_source_rgb(IntPtr cr, double red, double green, double blue);

        [DllImport(DllName)]
        public extern static void cairo_set_source_rgba(IntPtr cr, double red, double green, double blue, double alpha);

        [DllImport(DllName)]
        public extern static void cairo_set_source_surface(IntPtr cr, IntPtr surface, double x, double y);

        [DllImport(DllName)]
        public extern static void cairo_set_tolerance(IntPtr cr, double tolerance);

        [DllImport(DllName)]
        public extern static void cairo_set_antialias(IntPtr cr, cairo_antialias_t antialias);

        [DllImport(DllName)]
        public extern static void cairo_set_fill_rule(IntPtr cr, cairo_fill_rule_t fill_rule);

        [DllImport(DllName)]
        public extern static void cairo_set_line_width(IntPtr cr, double width);

        [DllImport(DllName)]
        public extern static void cairo_set_line_cap(IntPtr cr, cairo_line_cap_t line_cap);

        [DllImport(DllName)]
        public extern static void cairo_set_line_join(IntPtr cr, cairo_line_join_t line_join);

        [DllImport(DllName)]
        public extern static void cairo_set_dash(IntPtr cr, ref double dashes, int num_dashes, double offset);

        [DllImport(DllName)]
        public extern static void cairo_set_miter_limit(IntPtr cr, double limit);

        [DllImport(DllName)]
        public extern static void cairo_translate(IntPtr cr, double tx, double ty);

        [DllImport(DllName)]
        public extern static void cairo_scale(IntPtr cr, double sx, double sy);

        [DllImport(DllName)]
        public extern static void cairo_rotate(IntPtr cr, double angle);

        [DllImport(DllName)]
        public extern static void cairo_transform(IntPtr cr, ref cairo_matrix_t matrix);

        [DllImport(DllName)]
        public extern static void cairo_set_matrix(IntPtr cr, ref cairo_matrix_t matrix);

        [DllImport(DllName)]
        public extern static void cairo_identity_matrix(IntPtr cr);

        [DllImport(DllName)]
        public extern static void cairo_user_to_device(IntPtr cr, ref double x, ref double y);

        [DllImport(DllName)]
        public extern static void cairo_user_to_device_distance(IntPtr cr, ref double dx, ref double dy);

        [DllImport(DllName)]
        public extern static void cairo_device_to_user(IntPtr cr, ref double x, ref double y);

        [DllImport(DllName)]
        public extern static void cairo_device_to_user_distance(IntPtr cr, ref double dx, ref double dy);

        [DllImport(DllName)]
        public extern static void cairo_new_path(IntPtr cr);

        [DllImport(DllName)]
        public extern static void cairo_move_to(IntPtr cr, double x, double y);

        [DllImport(DllName)]
        public extern static void cairo_new_sub_path(IntPtr cr);

        [DllImport(DllName)]
        public extern static void cairo_line_to(IntPtr cr, double x, double y);

        [DllImport(DllName)]
        public extern static void cairo_curve_to(IntPtr cr, double x1, double y1, double x2, double y2, double x3, double y3);

        [DllImport(DllName)]
        public extern static void cairo_arc(IntPtr cr, double xc, double yc, double radius, double angle1, double angle2);

        [DllImport(DllName)]
        public extern static void cairo_arc_negative(IntPtr cr, double xc, double yc, double radius, double angle1, double angle2);

        [DllImport(DllName)]
        public extern static void cairo_rel_move_to(IntPtr cr, double dx, double dy);

        [DllImport(DllName)]
        public extern static void cairo_rel_line_to(IntPtr cr, double dx, double dy);

        [DllImport(DllName)]
        public extern static void cairo_rel_curve_to(IntPtr cr, double dx1, double dy1, double dx2, double dy2, double dx3, double dy3);

        [DllImport(DllName)]
        public extern static void cairo_rectangle(IntPtr cr, double x, double y, double width, double height);

        [DllImport(DllName)]
        public extern static void cairo_close_path(IntPtr cr);

        [DllImport(DllName)]
        public extern static void cairo_path_extents(IntPtr cr, ref double x1, ref double y1, ref double x2, ref double y2);

        [DllImport(DllName)]
        public extern static void cairo_paint(IntPtr cr);

        [DllImport(DllName)]
        public extern static void cairo_paint_with_alpha(IntPtr cr, double alpha);

        [DllImport(DllName)]
        public extern static void cairo_mask(IntPtr cr, IntPtr pattern);

        [DllImport(DllName)]
        public extern static void cairo_mask_surface(IntPtr cr, IntPtr surface, double surface_x, double surface_y);

        [DllImport(DllName)]
        public extern static void cairo_stroke(IntPtr cr);

        [DllImport(DllName)]
        public extern static void cairo_stroke_preserve(IntPtr cr);

        [DllImport(DllName)]
        public extern static void cairo_fill(IntPtr cr);

        [DllImport(DllName)]
        public extern static void cairo_fill_preserve(IntPtr cr);

        [DllImport(DllName)]
        public extern static void cairo_copy_page(IntPtr cr);

        [DllImport(DllName)]
        public extern static void cairo_show_page(IntPtr cr);

        [DllImport(DllName)]
        public extern static int cairo_in_stroke(IntPtr cr, double x, double y);

        [DllImport(DllName)]
        public extern static int cairo_in_fill(IntPtr cr, double x, double y);

        [DllImport(DllName)]
        public extern static int cairo_in_clip(IntPtr cr, double x, double y);

        [DllImport(DllName)]
        public extern static void cairo_stroke_extents(IntPtr cr, ref double x1, ref double y1, ref double x2, ref double y2);

        [DllImport(DllName)]
        public extern static void cairo_fill_extents(IntPtr cr, ref double x1, ref double y1, ref double x2, ref double y2);

        [DllImport(DllName)]
        public extern static void cairo_reset_clip(IntPtr cr);

        [DllImport(DllName)]
        public extern static void cairo_clip(IntPtr cr);

        [DllImport(DllName)]
        public extern static void cairo_clip_preserve(IntPtr cr);

        [DllImport(DllName)]
        public extern static void cairo_clip_extents(IntPtr cr, ref double x1, ref double y1, ref double x2, ref double y2);

        [DllImport(DllName)]
        public extern static IntPtr cairo_copy_clip_rectangle_list(IntPtr cr);

        [DllImport(DllName)]
        public extern static void cairo_rectangle_list_destroy(IntPtr rectangle_list);

        [DllImport(DllName)]
        public extern static void cairo_tag_begin(IntPtr cr, string tag_name, string attributes);

        [DllImport(DllName)]
        public extern static void cairo_tag_end(IntPtr cr, string tag_name);

        [DllImport(DllName)]
        public extern static IntPtr cairo_glyph_allocate(int num_glyphs);

        [DllImport(DllName)]
        public extern static void cairo_glyph_free(IntPtr glyphs);

        [DllImport(DllName)]
        public extern static IntPtr cairo_text_cluster_allocate(int num_clusters);

        [DllImport(DllName)]
        public extern static void cairo_text_cluster_free(IntPtr clusters);

        [DllImport(DllName)]
        public extern static IntPtr cairo_font_options_create();

        [DllImport(DllName)]
        public extern static IntPtr cairo_font_options_copy(IntPtr original);

        [DllImport(DllName)]
        public extern static void cairo_font_options_destroy(IntPtr options);

        [DllImport(DllName)]
        public extern static cairo_status_t cairo_font_options_status(IntPtr options);

        [DllImport(DllName)]
        public extern static void cairo_font_options_merge(IntPtr options, IntPtr other);

        [DllImport(DllName)]
        public extern static int cairo_font_options_equal(IntPtr options, IntPtr other);

        [DllImport(DllName)]
        public extern static ulong cairo_font_options_hash(IntPtr options);

        [DllImport(DllName)]
        public extern static void cairo_font_options_set_antialias(IntPtr options, cairo_antialias_t antialias);

        [DllImport(DllName)]
        public extern static cairo_antialias_t cairo_font_options_get_antialias(IntPtr options);

        [DllImport(DllName)]
        public extern static void cairo_font_options_set_subpixel_order(IntPtr options, cairo_subpixel_order_t subpixel_order);

        [DllImport(DllName)]
        public extern static cairo_subpixel_order_t cairo_font_options_get_subpixel_order(IntPtr options);

        [DllImport(DllName)]
        public extern static void cairo_font_options_set_hint_style(IntPtr options, cairo_hint_style_t hint_style);

        [DllImport(DllName)]
        public extern static cairo_hint_style_t cairo_font_options_get_hint_style(IntPtr options);

        [DllImport(DllName)]
        public extern static void cairo_font_options_set_hint_metrics(IntPtr options, cairo_hint_metrics_t hint_metrics);

        [DllImport(DllName)]
        public extern static cairo_hint_metrics_t cairo_font_options_get_hint_metrics(IntPtr options);

        [DllImport(DllName)]
        public extern static string cairo_font_options_get_variations(IntPtr options);

        [DllImport(DllName)]
        public extern static void cairo_font_options_set_variations(IntPtr options, string variations);

        [DllImport(DllName)]
        public extern static void cairo_select_font_face(IntPtr cr, string family, cairo_font_slant_t slant, cairo_font_weight_t weight);

        [DllImport(DllName)]
        public extern static void cairo_set_font_size(IntPtr cr, double size);

        [DllImport(DllName)]
        public extern static void cairo_set_font_matrix(IntPtr cr, ref cairo_matrix_t matrix);

        [DllImport(DllName)]
        public extern static void cairo_get_font_matrix(IntPtr cr, ref cairo_matrix_t matrix);

        [DllImport(DllName)]
        public extern static void cairo_set_font_options(IntPtr cr, IntPtr options);

        [DllImport(DllName)]
        public extern static void cairo_get_font_options(IntPtr cr, IntPtr options);

        [DllImport(DllName)]
        public extern static void cairo_set_font_face(IntPtr cr, IntPtr font_face);

        [DllImport(DllName)]
        public extern static IntPtr cairo_get_font_face(IntPtr cr);

        [DllImport(DllName)]
        public extern static void cairo_set_scaled_font(IntPtr cr, IntPtr scaled_font);

        [DllImport(DllName)]
        public extern static IntPtr cairo_get_scaled_font(IntPtr cr);

        [DllImport(DllName)]
        public extern static void cairo_show_text(IntPtr cr, string utf8);

        [DllImport(DllName)]
        public extern static void cairo_show_glyphs(IntPtr cr, IntPtr glyphs, int num_glyphs);

        [DllImport(DllName)]
        public extern static void cairo_show_text_glyphs(IntPtr cr, string utf8, int utf8_len, IntPtr glyphs, int num_glyphs, IntPtr clusters, int num_clusters, cairo_text_cluster_flags_t cluster_flags);

        [DllImport(DllName)]
        public extern static void cairo_text_path(IntPtr cr, string utf8);

        [DllImport(DllName)]
        public extern static void cairo_glyph_path(IntPtr cr, IntPtr glyphs, int num_glyphs);

        [DllImport(DllName)]
        public extern static void cairo_text_extents(IntPtr cr, string utf8, ref cairo_text_extents_t extents);

        [DllImport(DllName)]
        public extern static void cairo_glyph_extents(IntPtr cr, IntPtr glyphs, int num_glyphs, ref cairo_text_extents_t extents);

        [DllImport(DllName)]
        public extern static void cairo_font_extents(IntPtr cr, IntPtr extents);

        [DllImport(DllName)]
        public extern static IntPtr cairo_font_face_reference(IntPtr font_face);

        [DllImport(DllName)]
        public extern static void cairo_font_face_destroy(IntPtr font_face);

        [DllImport(DllName)]
        public extern static uint cairo_font_face_get_reference_count(IntPtr font_face);

        [DllImport(DllName)]
        public extern static cairo_status_t cairo_font_face_status(IntPtr font_face);

        [DllImport(DllName)]
        public extern static cairo_font_type_t cairo_font_face_get_type(IntPtr font_face);

        [DllImport(DllName)]
        public extern static IntPtr cairo_font_face_get_user_data(IntPtr font_face, IntPtr key);

        [DllImport(DllName)]
        public extern static IntPtr cairo_scaled_font_create(IntPtr font_face, ref cairo_matrix_t font_matrix, ref cairo_matrix_t ctm, IntPtr options);

        [DllImport(DllName)]
        public extern static IntPtr cairo_scaled_font_reference(IntPtr scaled_font);

        [DllImport(DllName)]
        public extern static void cairo_scaled_font_destroy(IntPtr scaled_font);

        [DllImport(DllName)]
        public extern static uint cairo_scaled_font_get_reference_count(IntPtr scaled_font);

        [DllImport(DllName)]
        public extern static cairo_status_t cairo_scaled_font_status(IntPtr scaled_font);

        [DllImport(DllName)]
        public extern static cairo_font_type_t cairo_scaled_font_get_type(IntPtr scaled_font);

        [DllImport(DllName)]
        public extern static IntPtr cairo_scaled_font_get_user_data(IntPtr scaled_font, IntPtr key);

        [DllImport(DllName)]
        public extern static void cairo_scaled_font_extents(IntPtr scaled_font, IntPtr extents);

        [DllImport(DllName)]
        public extern static void cairo_scaled_font_text_extents(IntPtr scaled_font, string utf8, ref cairo_text_extents_t extents);

        [DllImport(DllName)]
        public extern static void cairo_scaled_font_glyph_extents(IntPtr scaled_font, IntPtr glyphs, int num_glyphs, ref cairo_text_extents_t extents);

        [DllImport(DllName)]
        public extern static IntPtr cairo_scaled_font_get_font_face(IntPtr scaled_font);

        [DllImport(DllName)]
        public extern static void cairo_scaled_font_get_font_matrix(IntPtr scaled_font, ref cairo_matrix_t font_matrix);

        [DllImport(DllName)]
        public extern static void cairo_scaled_font_get_ctm(IntPtr scaled_font, ref cairo_matrix_t ctm);

        [DllImport(DllName)]
        public extern static void cairo_scaled_font_get_scale_matrix(IntPtr scaled_font, ref cairo_matrix_t scale_matrix);

        [DllImport(DllName)]
        public extern static void cairo_scaled_font_get_font_options(IntPtr scaled_font, IntPtr options);

        [DllImport(DllName)]
        public extern static IntPtr cairo_toy_font_face_create(string family, cairo_font_slant_t slant, cairo_font_weight_t weight);

        [DllImport(DllName)]
        public extern static string cairo_toy_font_face_get_family(IntPtr font_face);

        [DllImport(DllName)]
        public extern static cairo_font_slant_t cairo_toy_font_face_get_slant(IntPtr font_face);

        [DllImport(DllName)]
        public extern static cairo_font_weight_t cairo_toy_font_face_get_weight(IntPtr font_face);

        [DllImport(DllName)]
        public extern static IntPtr cairo_user_font_face_create();

        [DllImport(DllName)]
        public extern static cairo_operator_t cairo_get_operator(IntPtr cr);

        [DllImport(DllName)]
        public extern static IntPtr cairo_get_source(IntPtr cr);

        [DllImport(DllName)]
        public extern static double cairo_get_tolerance(IntPtr cr);

        [DllImport(DllName)]
        public extern static cairo_antialias_t cairo_get_antialias(IntPtr cr);

        [DllImport(DllName)]
        public extern static int cairo_has_current_point(IntPtr cr);

        [DllImport(DllName)]
        public extern static void cairo_get_current_point(IntPtr cr, ref double x, ref double y);

        [DllImport(DllName)]
        public extern static cairo_fill_rule_t cairo_get_fill_rule(IntPtr cr);

        [DllImport(DllName)]
        public extern static double cairo_get_line_width(IntPtr cr);

        [DllImport(DllName)]
        public extern static cairo_line_cap_t cairo_get_line_cap(IntPtr cr);

        [DllImport(DllName)]
        public extern static cairo_line_join_t cairo_get_line_join(IntPtr cr);

        [DllImport(DllName)]
        public extern static double cairo_get_miter_limit(IntPtr cr);

        [DllImport(DllName)]
        public extern static int cairo_get_dash_count(IntPtr cr);

        [DllImport(DllName)]
        public extern static void cairo_get_dash(IntPtr cr, ref double dashes, ref double offset);

        [DllImport(DllName)]
        public extern static void cairo_get_matrix(IntPtr cr, ref cairo_matrix_t matrix);

        [DllImport(DllName)]
        public extern static IntPtr cairo_get_target(IntPtr cr);

        [DllImport(DllName)]
        public extern static IntPtr cairo_get_group_target(IntPtr cr);

        [DllImport(DllName)]
        public extern static IntPtr cairo_copy_path(IntPtr cr);

        [DllImport(DllName)]
        public extern static IntPtr cairo_copy_path_flat(IntPtr cr);

        [DllImport(DllName)]
        public extern static void cairo_append_path(IntPtr cr, IntPtr path);

        [DllImport(DllName)]
        public extern static void cairo_path_destroy(IntPtr path);

        [DllImport(DllName)]
        public extern static cairo_status_t cairo_status(IntPtr cr);

        [DllImport(DllName)]
        public extern static string cairo_status_to_string(cairo_status_t status);

        [DllImport(DllName)]
        public extern static IntPtr cairo_device_reference(IntPtr device);

        [DllImport(DllName)]
        public extern static cairo_device_type_t cairo_device_get_type(IntPtr device);

        [DllImport(DllName)]
        public extern static cairo_status_t cairo_device_status(IntPtr device);

        [DllImport(DllName)]
        public extern static cairo_status_t cairo_device_acquire(IntPtr device);

        [DllImport(DllName)]
        public extern static void cairo_device_release(IntPtr device);

        [DllImport(DllName)]
        public extern static void cairo_device_flush(IntPtr device);

        [DllImport(DllName)]
        public extern static void cairo_device_finish(IntPtr device);

        [DllImport(DllName)]
        public extern static void cairo_device_destroy(IntPtr device);

        [DllImport(DllName)]
        public extern static uint cairo_device_get_reference_count(IntPtr device);

        [DllImport(DllName)]
        public extern static IntPtr cairo_device_get_user_data(IntPtr device, IntPtr key);

        [DllImport(DllName)]
        public extern static IntPtr cairo_surface_create_similar(IntPtr other, cairo_content_t content, int width, int height);

        [DllImport(DllName)]
        public extern static IntPtr cairo_surface_create_similar_image(IntPtr other, cairo_format_t format, int width, int height);

        [DllImport(DllName)]
        public extern static IntPtr cairo_surface_map_to_image(IntPtr surface, IntPtr extents);

        [DllImport(DllName)]
        public extern static void cairo_surface_unmap_image(IntPtr surface, IntPtr image);

        [DllImport(DllName)]
        public extern static IntPtr cairo_surface_create_for_rectangle(IntPtr target, double x, double y, double width, double height);

        [DllImport(DllName)]
        public extern static double cairo_surface_observer_elapsed(IntPtr surface);

        [DllImport(DllName)]
        public extern static double cairo_device_observer_elapsed(IntPtr device);

        [DllImport(DllName)]
        public extern static double cairo_device_observer_paint_elapsed(IntPtr device);

        [DllImport(DllName)]
        public extern static double cairo_device_observer_mask_elapsed(IntPtr device);

        [DllImport(DllName)]
        public extern static double cairo_device_observer_fill_elapsed(IntPtr device);

        [DllImport(DllName)]
        public extern static double cairo_device_observer_stroke_elapsed(IntPtr device);

        [DllImport(DllName)]
        public extern static double cairo_device_observer_glyphs_elapsed(IntPtr device);

        [DllImport(DllName)]
        public extern static IntPtr cairo_surface_reference(IntPtr surface);

        [DllImport(DllName)]
        public extern static void cairo_surface_finish(IntPtr surface);

        [DllImport(DllName)]
        public extern static void cairo_surface_destroy(IntPtr surface);

        [DllImport(DllName)]
        public extern static IntPtr cairo_surface_get_device(IntPtr surface);

        [DllImport(DllName)]
        public extern static uint cairo_surface_get_reference_count(IntPtr surface);

        [DllImport(DllName)]
        public extern static cairo_status_t cairo_surface_status(IntPtr surface);

        [DllImport(DllName)]
        public extern static cairo_surface_type_t cairo_surface_get_type(IntPtr surface);

        [DllImport(DllName)]
        public extern static cairo_content_t cairo_surface_get_content(IntPtr surface);

        [DllImport(DllName)]
        public extern static IntPtr cairo_surface_get_user_data(IntPtr surface, IntPtr key);

        [DllImport(DllName)]
        public extern static void cairo_surface_get_mime_data(IntPtr surface, string mime_type, ref byte[] data, ref uint length);

        [DllImport(DllName)]
        public extern static int cairo_surface_supports_mime_type(IntPtr surface, string mime_type);

        [DllImport(DllName)]
        public extern static void cairo_surface_get_font_options(IntPtr surface, IntPtr options);

        [DllImport(DllName)]
        public extern static void cairo_surface_flush(IntPtr surface);

        [DllImport(DllName)]
        public extern static void cairo_surface_mark_dirty(IntPtr surface);

        [DllImport(DllName)]
        public extern static void cairo_surface_mark_dirty_rectangle(IntPtr surface, int x, int y, int width, int height);

        [DllImport(DllName)]
        public extern static void cairo_surface_set_device_scale(IntPtr surface, double x_scale, double y_scale);

        [DllImport(DllName)]
        public extern static void cairo_surface_get_device_scale(IntPtr surface, ref double x_scale, ref double y_scale);

        [DllImport(DllName)]
        public extern static void cairo_surface_set_device_offset(IntPtr surface, double x_offset, double y_offset);

        [DllImport(DllName)]
        public extern static void cairo_surface_get_device_offset(IntPtr surface, ref double x_offset, ref double y_offset);

        [DllImport(DllName)]
        public extern static void cairo_surface_set_fallback_resolution(IntPtr surface, double x_pixels_per_inch, double y_pixels_per_inch);

        [DllImport(DllName)]
        public extern static void cairo_surface_get_fallback_resolution(IntPtr surface, ref double x_pixels_per_inch, ref double y_pixels_per_inch);

        [DllImport(DllName)]
        public extern static void cairo_surface_copy_page(IntPtr surface);

        [DllImport(DllName)]
        public extern static void cairo_surface_show_page(IntPtr surface);

        [DllImport(DllName)]
        public extern static int cairo_surface_has_show_text_glyphs(IntPtr surface);

        [DllImport(DllName)]
        public extern static IntPtr cairo_image_surface_create(cairo_format_t format, int width, int height);

        [DllImport(DllName)]
        public extern static int cairo_format_stride_for_width(cairo_format_t format, int width);

        [DllImport(DllName)]
        public extern static IntPtr cairo_image_surface_create_for_data(byte[] data, cairo_format_t format, int width, int height, int stride);

        [DllImport(DllName)]
        public extern static byte[] cairo_image_surface_get_data(IntPtr surface);

        [DllImport(DllName)]
        public extern static cairo_format_t cairo_image_surface_get_format(IntPtr surface);

        [DllImport(DllName)]
        public extern static int cairo_image_surface_get_width(IntPtr surface);

        [DllImport(DllName)]
        public extern static int cairo_image_surface_get_height(IntPtr surface);

        [DllImport(DllName)]
        public extern static int cairo_image_surface_get_stride(IntPtr surface);

        [DllImport(DllName)]
        public extern static IntPtr cairo_recording_surface_create(cairo_content_t content, IntPtr extents);

        [DllImport(DllName)]
        public extern static void cairo_recording_surface_ink_extents(IntPtr surface, ref double x0, ref double y0, ref double width, ref double height);

        [DllImport(DllName)]
        public extern static int cairo_recording_surface_get_extents(IntPtr surface, IntPtr extents);

        [DllImport(DllName)]
        public extern static IntPtr cairo_pattern_create_raster_source(IntPtr user_data, cairo_content_t content, int width, int height);

        [DllImport(DllName)]
        public extern static void cairo_raster_source_pattern_set_callback_data(IntPtr pattern, IntPtr data);

        [DllImport(DllName)]
        public extern static IntPtr cairo_raster_source_pattern_get_callback_data(IntPtr pattern);

        [DllImport(DllName)]
        public extern static IntPtr cairo_pattern_create_rgb(double red, double green, double blue);

        [DllImport(DllName)]
        public extern static IntPtr cairo_pattern_create_rgba(double red, double green, double blue, double alpha);

        [DllImport(DllName)]
        public extern static IntPtr cairo_pattern_create_for_surface(IntPtr surface);

        [DllImport(DllName)]
        public extern static IntPtr cairo_pattern_create_linear(double x0, double y0, double x1, double y1);

        [DllImport(DllName)]
        public extern static IntPtr cairo_pattern_create_radial(double cx0, double cy0, double radius0, double cx1, double cy1, double radius1);

        [DllImport(DllName)]
        public extern static IntPtr cairo_pattern_create_mesh();

        [DllImport(DllName)]
        public extern static IntPtr cairo_pattern_reference(IntPtr pattern);

        [DllImport(DllName)]
        public extern static void cairo_pattern_destroy(IntPtr pattern);

        [DllImport(DllName)]
        public extern static uint cairo_pattern_get_reference_count(IntPtr pattern);

        [DllImport(DllName)]
        public extern static cairo_status_t cairo_pattern_status(IntPtr pattern);

        [DllImport(DllName)]
        public extern static IntPtr cairo_pattern_get_user_data(IntPtr pattern, IntPtr key);

        [DllImport(DllName)]
        public extern static cairo_pattern_type_t cairo_pattern_get_type(IntPtr pattern);

        [DllImport(DllName)]
        public extern static void cairo_pattern_add_color_stop_rgb(IntPtr pattern, double offset, double red, double green, double blue);

        [DllImport(DllName)]
        public extern static void cairo_pattern_add_color_stop_rgba(IntPtr pattern, double offset, double red, double green, double blue, double alpha);

        [DllImport(DllName)]
        public extern static void cairo_mesh_pattern_begin_patch(IntPtr pattern);

        [DllImport(DllName)]
        public extern static void cairo_mesh_pattern_end_patch(IntPtr pattern);

        [DllImport(DllName)]
        public extern static void cairo_mesh_pattern_curve_to(IntPtr pattern, double x1, double y1, double x2, double y2, double x3, double y3);

        [DllImport(DllName)]
        public extern static void cairo_mesh_pattern_line_to(IntPtr pattern, double x, double y);

        [DllImport(DllName)]
        public extern static void cairo_mesh_pattern_move_to(IntPtr pattern, double x, double y);

        [DllImport(DllName)]
        public extern static void cairo_mesh_pattern_set_control_point(IntPtr pattern, uint point_num, double x, double y);

        [DllImport(DllName)]
        public extern static void cairo_mesh_pattern_set_corner_color_rgb(IntPtr pattern, uint corner_num, double red, double green, double blue);

        [DllImport(DllName)]
        public extern static void cairo_mesh_pattern_set_corner_color_rgba(IntPtr pattern, uint corner_num, double red, double green, double blue, double alpha);

        [DllImport(DllName)]
        public extern static void cairo_pattern_set_matrix(IntPtr pattern, ref cairo_matrix_t matrix);

        [DllImport(DllName)]
        public extern static void cairo_pattern_get_matrix(IntPtr pattern, ref cairo_matrix_t matrix);

        [DllImport(DllName)]
        public extern static void cairo_pattern_set_extend(IntPtr pattern, cairo_extend_t extend);

        [DllImport(DllName)]
        public extern static cairo_extend_t cairo_pattern_get_extend(IntPtr pattern);

        [DllImport(DllName)]
        public extern static void cairo_pattern_set_filter(IntPtr pattern, cairo_filter_t filter);

        [DllImport(DllName)]
        public extern static cairo_filter_t cairo_pattern_get_filter(IntPtr pattern);

        [DllImport(DllName)]
        public extern static cairo_status_t cairo_pattern_get_rgba(IntPtr pattern, ref double red, ref double green, ref double blue, ref double alpha);

        [DllImport(DllName)]
        public extern static cairo_status_t cairo_pattern_get_surface(IntPtr pattern, ref IntPtr surface);

        [DllImport(DllName)]
        public extern static cairo_status_t cairo_pattern_get_color_stop_rgba(IntPtr pattern, int index, ref double offset, ref double red, ref double green, ref double blue, ref double alpha);

        [DllImport(DllName)]
        public extern static cairo_status_t cairo_pattern_get_color_stop_count(IntPtr pattern, ref int count);

        [DllImport(DllName)]
        public extern static cairo_status_t cairo_pattern_get_linear_points(IntPtr pattern, ref double x0, ref double y0, ref double x1, ref double y1);

        [DllImport(DllName)]
        public extern static cairo_status_t cairo_pattern_get_radial_circles(IntPtr pattern, ref double x0, ref double y0, ref double r0, ref double x1, ref double y1, ref double r1);

        [DllImport(DllName)]
        public extern static cairo_status_t cairo_mesh_pattern_get_patch_count(IntPtr pattern, ref uint count);

        [DllImport(DllName)]
        public extern static IntPtr cairo_mesh_pattern_get_path(IntPtr pattern, uint patch_num);

        [DllImport(DllName)]
        public extern static cairo_status_t cairo_mesh_pattern_get_corner_color_rgba(IntPtr pattern, uint patch_num, uint corner_num, ref double red, ref double green, ref double blue, ref double alpha);

        [DllImport(DllName)]
        public extern static cairo_status_t cairo_mesh_pattern_get_control_point(IntPtr pattern, uint patch_num, uint point_num, ref double x, ref double y);

        [DllImport(DllName)]
        public extern static void cairo_matrix_init(ref cairo_matrix_t matrix, double xx, double yx, double xy, double yy, double x0, double y0);

        [DllImport(DllName)]
        public extern static void cairo_matrix_init_identity(ref cairo_matrix_t matrix);

        [DllImport(DllName)]
        public extern static void cairo_matrix_init_translate(ref cairo_matrix_t matrix, double tx, double ty);

        [DllImport(DllName)]
        public extern static void cairo_matrix_init_scale(ref cairo_matrix_t matrix, double sx, double sy);

        [DllImport(DllName)]
        public extern static void cairo_matrix_init_rotate(ref cairo_matrix_t matrix, double radians);

        [DllImport(DllName)]
        public extern static void cairo_matrix_translate(ref cairo_matrix_t matrix, double tx, double ty);

        [DllImport(DllName)]
        public extern static void cairo_matrix_scale(ref cairo_matrix_t matrix, double sx, double sy);

        [DllImport(DllName)]
        public extern static void cairo_matrix_rotate(ref cairo_matrix_t matrix, double radians);

        [DllImport(DllName)]
        public extern static cairo_status_t cairo_matrix_invert(ref cairo_matrix_t matrix);

        [DllImport(DllName)]
        public extern static void cairo_matrix_multiply(ref cairo_matrix_t result, ref cairo_matrix_t a, ref cairo_matrix_t b);

        [DllImport(DllName)]
        public extern static void cairo_matrix_transform_distance(ref cairo_matrix_t matrix, ref double dx, ref double dy);

        [DllImport(DllName)]
        public extern static void cairo_matrix_transform_point(ref cairo_matrix_t matrix, ref double x, ref double y);

        [DllImport(DllName)]
        public extern static IntPtr cairo_region_create();

        [DllImport(DllName)]
        public extern static IntPtr cairo_region_create_rectangle(IntPtr rectangle);

        [DllImport(DllName)]
        public extern static IntPtr cairo_region_create_rectangles(IntPtr rects, int count);

        [DllImport(DllName)]
        public extern static IntPtr cairo_region_copy(IntPtr original);

        [DllImport(DllName)]
        public extern static IntPtr cairo_region_reference(IntPtr region);

        [DllImport(DllName)]
        public extern static void cairo_region_destroy(IntPtr region);

        [DllImport(DllName)]
        public extern static int cairo_region_equal(IntPtr a, IntPtr b);

        [DllImport(DllName)]
        public extern static cairo_status_t cairo_region_status(IntPtr region);

        [DllImport(DllName)]
        public extern static void cairo_region_get_extents(IntPtr region, IntPtr extents);

        [DllImport(DllName)]
        public extern static int cairo_region_num_rectangles(IntPtr region);

        [DllImport(DllName)]
        public extern static void cairo_region_get_rectangle(IntPtr region, int nth, IntPtr rectangle);

        [DllImport(DllName)]
        public extern static int cairo_region_is_empty(IntPtr region);

        [DllImport(DllName)]
        public extern static cairo_region_overlap_t cairo_region_contains_rectangle(IntPtr region, IntPtr rectangle);

        [DllImport(DllName)]
        public extern static int cairo_region_contains_point(IntPtr region, int x, int y);

        [DllImport(DllName)]
        public extern static void cairo_region_translate(IntPtr region, int dx, int dy);

        [DllImport(DllName)]
        public extern static cairo_status_t cairo_region_subtract(IntPtr dst, IntPtr other);

        [DllImport(DllName)]
        public extern static cairo_status_t cairo_region_subtract_rectangle(IntPtr dst, IntPtr rectangle);

        [DllImport(DllName)]
        public extern static cairo_status_t cairo_region_intersect(IntPtr dst, IntPtr other);

        [DllImport(DllName)]
        public extern static cairo_status_t cairo_region_intersect_rectangle(IntPtr dst, IntPtr rectangle);

        [DllImport(DllName)]
        public extern static cairo_status_t cairo_region_union(IntPtr dst, IntPtr other);

        [DllImport(DllName)]
        public extern static cairo_status_t cairo_region_union_rectangle(IntPtr dst, IntPtr rectangle);

        [DllImport(DllName)]
        public extern static cairo_status_t cairo_region_xor(IntPtr dst, IntPtr other);

        [DllImport(DllName)]
        public extern static cairo_status_t cairo_region_xor_rectangle(IntPtr dst, IntPtr rectangle);

        [DllImport(DllName)]
        public extern static void cairo_debug_reset_static_data();

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
        
        public IntPtr reference(){
            return Dll.cairo_reference(Ptr);
        }

        public void destroy(){
             Dll.cairo_destroy(Ptr);
        }

        public uint get_reference_count(){
            return Dll.cairo_get_reference_count(Ptr);
        }

        public IntPtr get_user_data(IntPtr key){
            return Dll.cairo_get_user_data(Ptr, key);
        }

        public void save(){
             Dll.cairo_save(Ptr);
        }

        public void restore(){
             Dll.cairo_restore(Ptr);
        }

        public void push_group(){
             Dll.cairo_push_group(Ptr);
        }

        public void push_group_with_content(cairo_content_t content){
             Dll.cairo_push_group_with_content(Ptr, content);
        }

        public IntPtr pop_group(){
            return Dll.cairo_pop_group(Ptr);
        }

        public void pop_group_to_source(){
             Dll.cairo_pop_group_to_source(Ptr);
        }

        public void set_operator(cairo_operator_t op){
             Dll.cairo_set_operator(Ptr, op);
        }

        public void set_source(IntPtr source){
             Dll.cairo_set_source(Ptr, source);
        }

        public void set_source_rgb(double red, double green, double blue){
             Dll.cairo_set_source_rgb(Ptr, red, green, blue);
        }

        public void set_source_rgba(double red, double green, double blue, double alpha){
             Dll.cairo_set_source_rgba(Ptr, red, green, blue, alpha);
        }

        public void set_source_surface(IntPtr surface, double x, double y){
             Dll.cairo_set_source_surface(Ptr, surface, x, y);
        }

        public void set_tolerance(double tolerance){
             Dll.cairo_set_tolerance(Ptr, tolerance);
        }

        public void set_antialias(cairo_antialias_t antialias){
             Dll.cairo_set_antialias(Ptr, antialias);
        }

        public void set_fill_rule(cairo_fill_rule_t fill_rule){
             Dll.cairo_set_fill_rule(Ptr, fill_rule);
        }

        public void set_line_width(double width){
             Dll.cairo_set_line_width(Ptr, width);
        }

        public void set_line_cap(cairo_line_cap_t line_cap){
             Dll.cairo_set_line_cap(Ptr, line_cap);
        }

        public void set_line_join(cairo_line_join_t line_join){
             Dll.cairo_set_line_join(Ptr, line_join);
        }

        public void set_dash(ref double dashes, int num_dashes, double offset){
             Dll.cairo_set_dash(Ptr, ref dashes, num_dashes, offset);
        }

        public void set_miter_limit(double limit){
             Dll.cairo_set_miter_limit(Ptr, limit);
        }

        public void translate(double tx, double ty){
             Dll.cairo_translate(Ptr, tx, ty);
        }

        public void scale(double sx, double sy){
             Dll.cairo_scale(Ptr, sx, sy);
        }

        public void rotate(double angle){
             Dll.cairo_rotate(Ptr, angle);
        }

        public void transform(ref cairo_matrix_t matrix){
             Dll.cairo_transform(Ptr, ref matrix);
        }

        public void set_matrix(ref cairo_matrix_t matrix){
             Dll.cairo_set_matrix(Ptr, ref matrix);
        }

        public void identity_matrix(){
             Dll.cairo_identity_matrix(Ptr);
        }

        public void user_to_device(ref double x, ref double y){
             Dll.cairo_user_to_device(Ptr, ref x, ref y);
        }

        public void user_to_device_distance(ref double dx, ref double dy){
             Dll.cairo_user_to_device_distance(Ptr, ref dx, ref dy);
        }

        public void device_to_user(ref double x, ref double y){
             Dll.cairo_device_to_user(Ptr, ref x, ref y);
        }

        public void device_to_user_distance(ref double dx, ref double dy){
             Dll.cairo_device_to_user_distance(Ptr, ref dx, ref dy);
        }

        public void new_path(){
             Dll.cairo_new_path(Ptr);
        }

        public void move_to(double x, double y){
             Dll.cairo_move_to(Ptr, x, y);
        }

        public void new_sub_path(){
             Dll.cairo_new_sub_path(Ptr);
        }

        public void line_to(double x, double y){
             Dll.cairo_line_to(Ptr, x, y);
        }

        public void curve_to(double x1, double y1, double x2, double y2, double x3, double y3){
             Dll.cairo_curve_to(Ptr, x1, y1, x2, y2, x3, y3);
        }

        public void arc(double xc, double yc, double radius, double angle1, double angle2){
             Dll.cairo_arc(Ptr, xc, yc, radius, angle1, angle2);
        }

        public void arc_negative(double xc, double yc, double radius, double angle1, double angle2){
             Dll.cairo_arc_negative(Ptr, xc, yc, radius, angle1, angle2);
        }

        public void rel_move_to(double dx, double dy){
             Dll.cairo_rel_move_to(Ptr, dx, dy);
        }

        public void rel_line_to(double dx, double dy){
             Dll.cairo_rel_line_to(Ptr, dx, dy);
        }

        public void rel_curve_to(double dx1, double dy1, double dx2, double dy2, double dx3, double dy3){
             Dll.cairo_rel_curve_to(Ptr, dx1, dy1, dx2, dy2, dx3, dy3);
        }

        public void rectangle(double x, double y, double width, double height){
             Dll.cairo_rectangle(Ptr, x, y, width, height);
        }

        public void close_path(){
             Dll.cairo_close_path(Ptr);
        }

        public void path_extents(ref double x1, ref double y1, ref double x2, ref double y2){
             Dll.cairo_path_extents(Ptr, ref x1, ref y1, ref x2, ref y2);
        }

        public void paint(){
             Dll.cairo_paint(Ptr);
        }

        public void paint_with_alpha(double alpha){
             Dll.cairo_paint_with_alpha(Ptr, alpha);
        }

        public void mask(IntPtr pattern){
             Dll.cairo_mask(Ptr, pattern);
        }

        public void mask_surface(IntPtr surface, double surface_x, double surface_y){
             Dll.cairo_mask_surface(Ptr, surface, surface_x, surface_y);
        }

        public void stroke(){
             Dll.cairo_stroke(Ptr);
        }

        public void stroke_preserve(){
             Dll.cairo_stroke_preserve(Ptr);
        }

        public void fill(){
             Dll.cairo_fill(Ptr);
        }

        public void fill_preserve(){
             Dll.cairo_fill_preserve(Ptr);
        }

        public void copy_page(){
             Dll.cairo_copy_page(Ptr);
        }

        public void show_page(){
             Dll.cairo_show_page(Ptr);
        }

        public int in_stroke(double x, double y){
            return Dll.cairo_in_stroke(Ptr, x, y);
        }

        public int in_fill(double x, double y){
            return Dll.cairo_in_fill(Ptr, x, y);
        }

        public int in_clip(double x, double y){
            return Dll.cairo_in_clip(Ptr, x, y);
        }

        public void stroke_extents(ref double x1, ref double y1, ref double x2, ref double y2){
             Dll.cairo_stroke_extents(Ptr, ref x1, ref y1, ref x2, ref y2);
        }

        public void fill_extents(ref double x1, ref double y1, ref double x2, ref double y2){
             Dll.cairo_fill_extents(Ptr, ref x1, ref y1, ref x2, ref y2);
        }

        public void reset_clip(){
             Dll.cairo_reset_clip(Ptr);
        }

        public void clip(){
             Dll.cairo_clip(Ptr);
        }

        public void clip_preserve(){
             Dll.cairo_clip_preserve(Ptr);
        }

        public void clip_extents(ref double x1, ref double y1, ref double x2, ref double y2){
             Dll.cairo_clip_extents(Ptr, ref x1, ref y1, ref x2, ref y2);
        }

        public IntPtr copy_clip_rectangle_list(){
            return Dll.cairo_copy_clip_rectangle_list(Ptr);
        }

        public void tag_begin(string tag_name, string attributes){
             Dll.cairo_tag_begin(Ptr, tag_name, attributes);
        }

        public void tag_end(string tag_name){
             Dll.cairo_tag_end(Ptr, tag_name);
        }

        public void select_font_face(string family, cairo_font_slant_t slant, cairo_font_weight_t weight){
             Dll.cairo_select_font_face(Ptr, family, slant, weight);
        }

        public void set_font_size(double size){
             Dll.cairo_set_font_size(Ptr, size);
        }

        public void set_font_matrix(ref cairo_matrix_t matrix){
             Dll.cairo_set_font_matrix(Ptr, ref matrix);
        }

        public void get_font_matrix(ref cairo_matrix_t matrix){
             Dll.cairo_get_font_matrix(Ptr, ref matrix);
        }

        public void set_font_options(IntPtr options){
             Dll.cairo_set_font_options(Ptr, options);
        }

        public void get_font_options(IntPtr options){
             Dll.cairo_get_font_options(Ptr, options);
        }

        public void set_font_face(IntPtr font_face){
             Dll.cairo_set_font_face(Ptr, font_face);
        }

        public IntPtr get_font_face(){
            return Dll.cairo_get_font_face(Ptr);
        }

        public void set_scaled_font(IntPtr scaled_font){
             Dll.cairo_set_scaled_font(Ptr, scaled_font);
        }

        public IntPtr get_scaled_font(){
            return Dll.cairo_get_scaled_font(Ptr);
        }

        public void show_text(string utf8){
             Dll.cairo_show_text(Ptr, utf8);
        }

        public void show_glyphs(IntPtr glyphs, int num_glyphs){
             Dll.cairo_show_glyphs(Ptr, glyphs, num_glyphs);
        }

        public void show_text_glyphs(string utf8, int utf8_len, IntPtr glyphs, int num_glyphs, IntPtr clusters, int num_clusters, cairo_text_cluster_flags_t cluster_flags){
             Dll.cairo_show_text_glyphs(Ptr, utf8, utf8_len, glyphs, num_glyphs, clusters, num_clusters, cluster_flags);
        }

        public void text_path(string utf8){
             Dll.cairo_text_path(Ptr, utf8);
        }

        public void glyph_path(IntPtr glyphs, int num_glyphs){
             Dll.cairo_glyph_path(Ptr, glyphs, num_glyphs);
        }

        public void text_extents(string utf8, ref cairo_text_extents_t extents){
             Dll.cairo_text_extents(Ptr, utf8, ref extents);
        }

        public void glyph_extents(IntPtr glyphs, int num_glyphs, ref cairo_text_extents_t extents){
             Dll.cairo_glyph_extents(Ptr, glyphs, num_glyphs, ref extents);
        }

        public void font_extents(IntPtr extents){
             Dll.cairo_font_extents(Ptr, extents);
        }

        public cairo_operator_t get_operator(){
            return Dll.cairo_get_operator(Ptr);
        }

        public IntPtr get_source(){
            return Dll.cairo_get_source(Ptr);
        }

        public double get_tolerance(){
            return Dll.cairo_get_tolerance(Ptr);
        }

        public cairo_antialias_t get_antialias(){
            return Dll.cairo_get_antialias(Ptr);
        }

        public int has_current_point(){
            return Dll.cairo_has_current_point(Ptr);
        }

        public void get_current_point(ref double x, ref double y){
             Dll.cairo_get_current_point(Ptr, ref x, ref y);
        }

        public cairo_fill_rule_t get_fill_rule(){
            return Dll.cairo_get_fill_rule(Ptr);
        }

        public double get_line_width(){
            return Dll.cairo_get_line_width(Ptr);
        }

        public cairo_line_cap_t get_line_cap(){
            return Dll.cairo_get_line_cap(Ptr);
        }

        public cairo_line_join_t get_line_join(){
            return Dll.cairo_get_line_join(Ptr);
        }

        public double get_miter_limit(){
            return Dll.cairo_get_miter_limit(Ptr);
        }

        public int get_dash_count(){
            return Dll.cairo_get_dash_count(Ptr);
        }

        public void get_dash(ref double dashes, ref double offset){
             Dll.cairo_get_dash(Ptr, ref dashes, ref offset);
        }

        public void get_matrix(ref cairo_matrix_t matrix){
             Dll.cairo_get_matrix(Ptr, ref matrix);
        }

        public IntPtr get_target(){
            return Dll.cairo_get_target(Ptr);
        }

        public IntPtr get_group_target(){
            return Dll.cairo_get_group_target(Ptr);
        }

        public IntPtr copy_path(){
            return Dll.cairo_copy_path(Ptr);
        }

        public IntPtr copy_path_flat(){
            return Dll.cairo_copy_path_flat(Ptr);
        }

        public void append_path(IntPtr path){
             Dll.cairo_append_path(Ptr, path);
        }

        public cairo_status_t status(){
            return Dll.cairo_status(Ptr);
        }

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
