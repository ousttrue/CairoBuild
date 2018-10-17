using UnityCairo;
using System;
using UnityEngine;


namespace VRI.Sandbox
{
    public class HelloCairo : MonoBehaviour
    {
        [SerializeField]
        Color m_foreground = Color.blue;

        [SerializeField]
        Color m_background = Color.white;

        [SerializeField]
        float m_lineWidth = 10.0f;

        [SerializeField]
        int m_width = 120;

        [SerializeField]
        int m_height = 120;

        [SerializeField]
        double m_fontSize = 36;

        [SerializeField]
        string m_text = "日本語";

        static void Extents(Cairo cr, double width, double height,
            double fontSize, string utf8, Color fg, Color bg, double lineWidth)
        {
            cr.translate(0, height);
            cr.scale(1, -1);
            //cr.translate(0, -height);

            cr.rectangle(0, 0, width, height);
            cr.set_source_rgba(1, 1, 1, 1);
            cr.fill();

            var div = 5;

            var dy = height / div;
            cr.set_source_rgba(1, 0, 0, 1);
            for (int i = 0; i <= div; ++i)
            {
                var y = dy * i;
                cr.move_to(0, y);
                cr.line_to(width, y);
                cr.stroke();
            }

            var dx = width / div;
            cr.set_source_rgba(0, 1, 0, 1);
            for (int i = 0; i <= div; ++i)
            {
                var x = dx * i;
                cr.move_to(x, 0);
                cr.line_to(x, height);
                cr.stroke();
            }

            var fs = 9;
            cr.select_font_face("Sans",
                cairo_font_slant_t.CAIRO_FONT_SLANT_NORMAL,
                cairo_font_weight_t.CAIRO_FONT_WEIGHT_NORMAL);
            cr.set_font_size(fs);
            for (int j = 0; j < div; ++j)
            {
                for (int i = 0; i < div; ++i)
                {
                    var x = dx * i;
                    var y = dy * j + fs;
                    cr.move_to(x, y);
                    cr.show_text(string.Format("{0}, {1}", i, j));
                }
            }

            // extents
            {
                cr.select_font_face("Sans",
                    cairo_font_slant_t.CAIRO_FONT_SLANT_NORMAL,
                    cairo_font_weight_t.CAIRO_FONT_WEIGHT_NORMAL);
                cr.set_font_size(fontSize);
                var extents = default(cairo_text_extents_t);
                cr.text_extents(utf8, ref extents);

                var x = width / 2 - extents.width / 2 - extents.x_bearing;
                var y = height / 2 + extents.height / 2 - (extents.height + extents.y_bearing);

                cr.set_source_rgb(0.7, 0.7, 0.7);
                cr.move_to(x, y);
                cr.line_to(x + extents.x_advance, y);
                cr.stroke();

                /*
                cr.move_to(x, y);
                cr.line_to(x, y + extents.y_advance);
                cr.stroke();
                */

                // point
                cr.arc(x, y, 4, 0, 2 * Math.PI);
                cr.fill();

                // width height
                cr.rectangle(x + extents.x_bearing, y + extents.y_bearing,
                    extents.width, extents.height);
                cr.stroke();

                // text
                cr.set_source_rgb(fg.r, fg.g, fg.b);
                cr.move_to(x, y);
                cr.show_text(utf8);
            }
        }

        static void CircleLabel(Cairo cr, double width, double height,
            double fontSize, string utf8, Color fg, Color bg, double lineWidth)
        {
            /*
            cr.rectangle(0, 0, width, height);
            cr.stroke();
            cr.move_to(width / 2, 0);
            cr.line_to(width / 2, height);
            cr.stroke();
            cr.move_to(0, height / 2);
            cr.line_to(width, height / 2);
            cr.stroke();
            */

            cr.scale(1, -1);
            cr.translate(0, -height);

            var x = width / 2;
            var y = height / 2;
            var r = x * 0.95;
            cr.set_source_rgb(bg.r, bg.g, bg.b);
            cr.arc(x, y, r, 0, 2 * Math.PI);
            cr.fill();

            cr.set_source_rgb(fg.r, fg.g, fg.b);
            cr.arc(x, y, r, 0, 2 * Math.PI);
            cr.set_line_width(lineWidth);
            cr.stroke();

            cr.select_font_face("Sans",
                cairo_font_slant_t.CAIRO_FONT_SLANT_NORMAL,
                cairo_font_weight_t.CAIRO_FONT_WEIGHT_NORMAL);
            cr.set_font_size(fontSize);
            var extents = default(cairo_text_extents_t);
            cr.text_extents(utf8, ref extents);

            cr.move_to(
                x - extents.x_advance / 2,
                y - extents.y_bearing / 2
                );
            cr.show_text(utf8);
        }

        /// <summary>
        /// https://www.cairographics.org/samples/text_extents/
        /// </summary>
        /// <param name="cr"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="fontSize"></param>
        /// <param name="utf8"></param>
        static void TextExtents(Cairo cr, int width, int height, double fontSize, string utf8)
        {
            cr.select_font_face("Sans",
                cairo_font_slant_t.CAIRO_FONT_SLANT_NORMAL,
                cairo_font_weight_t.CAIRO_FONT_WEIGHT_NORMAL);

            cr.set_font_size(fontSize);
            var extents = default(cairo_text_extents_t);
            cr.text_extents(utf8, ref extents);

            var x = 25.0;
            var y = height / 2;

            cr.move_to(x, y);
            cr.show_text(utf8);

            /* draw helping lines */
            cr.set_source_rgba(1, 0.2, 0.2, 0.6);
            cr.set_line_width(6.0);
            cr.arc(x, y, 10.0, 0, 2 * Math.PI);
            cr.fill();
            cr.move_to(x, y);
            cr.rel_line_to(0, -extents.height);
            cr.rel_line_to(extents.width, 0);
            cr.rel_line_to(extents.x_bearing, -extents.y_bearing);
            cr.stroke();
        }

        static void TextExtents2(Cairo cr, int width, int height, double fontSize, string utf8)
        {
            /*
            cr.set_line_width(0.1f);
            cr.set_source_rgb(255, 0, 0);
            cr.rectangle(0.25f * width, 0.25f * height, 0.5f * width, 0.5f * height);
            cr.fill();
            */

            cr.select_font_face("Sans",
                cairo_font_slant_t.CAIRO_FONT_SLANT_NORMAL,
                cairo_font_weight_t.CAIRO_FONT_WEIGHT_NORMAL);

            cr.set_font_size(fontSize);
            var extents = default(cairo_text_extents_t);
            cr.text_extents(utf8, ref extents);
            var x = width / 2 - (extents.width / 2 + extents.x_bearing);
            var y = height / 2 - (extents.height / 2 + extents.y_bearing);
            cr.move_to(x, y);
            cr.show_text(utf8);
        }

        static void WriteTexture(Texture2D texture, double fontSize, string utf8, Color fg, Color bg, float lineWidth)
        {
            var width = texture.width;
            var height = texture.height;
            var data = new Byte[width * height * 4];

            using (var surface = Surface.CreateFromBytes(data, width, height, width * 4))
            {
                //using (var surface = Surface.Create(120, 120))
                using (var cairo = Cairo.Create(surface))
                {
                    Extents(cairo, width, height, fontSize, utf8, fg, bg, lineWidth);
                }

                texture.LoadRawTextureData(data);
                texture.Apply();
            }
        }

        private void Start()
        {
            var texture = new Texture2D(m_width, m_height, TextureFormat.BGRA32, false);
            WriteTexture(texture, m_fontSize, m_text, m_foreground, m_background, m_lineWidth);

            var shader = Shader.Find("Unlit/Transparent");
            var material = new Material(shader);
            material.mainTexture = texture;

            //var aspect = (float)m_width / (float)m_height;
            var quad = GameObject.CreatePrimitive(PrimitiveType.Quad);
            //quad.transform.localScale = new Vector3(aspect, -1, 1);
            var renderer = quad.GetComponent<Renderer>();
            renderer.sharedMaterial = material;
        }
    }
}
