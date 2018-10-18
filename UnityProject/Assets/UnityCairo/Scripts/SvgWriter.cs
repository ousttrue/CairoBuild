using System;
using System.Xml.Linq;


namespace UnityCairo
{
    static class SvgWriter
    {
        struct Rgb
        {
            public double r;
            public double g;
            public double b;

            public static Rgb Parse(string rgb)
            {
                if (rgb[0] != '#') throw new Exception();
                if (rgb.Length != 7) throw new Exception();
                return new Rgb
                {
                    r = Convert.ToByte(rgb.Substring(1, 2), 16) / 255.0,
                    g = Convert.ToByte(rgb.Substring(3, 2), 16) / 255.0,
                    b = Convert.ToByte(rgb.Substring(5, 2), 16) / 255.0,
                };
            }
        }

        struct Style
        {
            public Rgb? Stroke;
            public Rgb? Fill;

            public static Style Parse(string src)
            {
                var style = new Style
                {
                };

                foreach (var token in src.Split(';'))
                {
                    var splited = token.Split(':');
                    switch (splited[0].Trim())
                    {
                        case "stroke":
                            style.Stroke = Rgb.Parse(splited[1].Trim());
                            break;

                        case "fill":
                            style.Fill = Rgb.Parse(splited[1].Trim());
                            break;
                    }
                }

                return style;
            }

            public void Apply(Cairo cr)
            {
                if (Fill.HasValue && Stroke.HasValue)
                {
                    {
                        var rgb = Fill.Value;
                        cr.set_source_rgb(rgb.r, rgb.g, rgb.b);
                        cr.fill_preserve();
                    }
                    {
                        var rgb = Stroke.Value;
                        cr.set_source_rgb(rgb.r, rgb.g, rgb.b);
                        cr.stroke();
                    }
                }
                else if (Fill.HasValue)
                { 
                    var rgb = Fill.Value;
                    cr.set_source_rgb(rgb.r, rgb.g, rgb.b);
                    cr.fill();
                }
                else if (Stroke.HasValue)
                {
                    var rgb = Stroke.Value;
                    cr.set_source_rgb(rgb.r, rgb.g, rgb.b);
                    cr.stroke();
                }
            }
        }

        struct Rotate
        {
            public double degree;
            public double x;
            public double y;

            public static Rotate Parse(string src)
            {
                var splited = src.Split();
                var r= new Rotate
                { };

                r.degree = double.Parse(splited[0]);
                r.x = double.Parse(splited[1]);
                r.y = double.Parse(splited[2]);

                return r;
            }
        }

        struct Transform
        {
            public Rotate? rotate;

            public static Transform Parse(String src)
            {
                var t = new Transform
                {

                };

                if (src.StartsWith("rotate(") && src.EndsWith(")"))
                {
                    t.rotate = Rotate.Parse(src.Substring(7, src.Length-8));
                }

                return t;
            }

            public void Apply(Cairo cr)
            {
                if (rotate.HasValue)
                {
                    var r = rotate.Value;
                    cr.translate(r.x, r.y);
                    cr.rotate(UnityEngine.Mathf.Deg2Rad * r.degree);
                    cr.translate(-r.x, -r.y);
                }
            }
        }

        struct Group
        {
            public double x;
            public double y;
            public Transform? transform;

            public static Group Parse(XElement x)
            {
                var g = new Group
                {

                };

                foreach (var a in x.Attributes())
                {
                    switch (a.Name.LocalName)
                    {
                        case "x":
                            g.x = double.Parse(a.Value);
                            break;

                        case "y":
                            g.y = double.Parse(a.Value);
                            break;

                        case "transform":
                            g.transform = Transform.Parse(a.Value);
                            break;

                        default:
                            throw new NotImplementedException(a.Name.LocalName);
                    }

                }

                return g;
            }

            public void Apply(Cairo cr)
            {
                cr.translate(x, y);

                if (transform.HasValue)
                {
                    var t = transform.Value;
                    t.Apply(cr);
                }
            }
        }

        static void DrawLine(Cairo cr, XElement e)
        {
            var x1 = double.Parse(e.Attribute("x1").Value);
            var y1 = double.Parse(e.Attribute("y1").Value);
            var x2 = double.Parse(e.Attribute("x2").Value);
            var y2 = double.Parse(e.Attribute("y2").Value);
            var style = Style.Parse(e.Attribute("style").Value);
            cr.move_to(x1, y1);
            cr.line_to(x2, y2);
            style.Apply(cr);
        }

        static void DrawRect(Cairo cr, XElement e)
        {
            var x = double.Parse(e.Attribute("x").Value);
            var y = double.Parse(e.Attribute("y").Value);
            var w = double.Parse(e.Attribute("width").Value);
            var h = double.Parse(e.Attribute("height").Value);
            var style = Style.Parse(e.Attribute("style").Value);
            cr.rectangle(x, y, w, h);
            style.Apply(cr);
        }

        static void DrawCircle(Cairo cr, XElement e)
        {
            var cx = double.Parse(e.Attribute("cx").Value);
            var cy = double.Parse(e.Attribute("cy").Value);
            var r = double.Parse(e.Attribute("r").Value);
            var style = Style.Parse(e.Attribute("style").Value);
            cr.arc(cx, cy, r, 0, Math.PI * 2);
            style.Apply(cr);
        }

        static void DrawText(Cairo cr, XElement e)
        {
            var x = double.Parse(e.Attribute("x").Value);
            var y = double.Parse(e.Attribute("y").Value);
            var style = Style.Parse(e.Attribute("style").Value);

            cr.select_font_face("Sans",
                cairo_font_slant_t.CAIRO_FONT_SLANT_NORMAL,
                cairo_font_weight_t.CAIRO_FONT_WEIGHT_NORMAL);
            cr.set_font_size(24);

#if true
            style.Apply(cr);
            cr.move_to(x, y);
            cr.show_text(e.Value.Trim());
#else
            cr.move_to(x, y);
            cr.text_path(e.Value.Trim()); // use cairo_glyph_path ?
            style.Apply(cr);
#endif
        }

        public static void Draw(Cairo cr, XElement e)
        {
            switch (e.Name.LocalName)
            {
                case "g":
                case "svg":
                    {
                        cr.save();
                        {
                            var g = Group.Parse(e);
                            g.Apply(cr);

                            foreach (var child in e.Elements())
                            {
                                Draw(cr, child);
                            }
                        }
                        cr.restore();
                    }
                    break;

                case "line":
                    DrawLine(cr, e);
                    break;

                case "rect":
                    DrawRect(cr, e);
                    break;

                case "circle":
                    DrawCircle(cr, e);
                    break;

                case "text":
                    DrawText(cr, e);
                    break;

                default:
                    throw new NotImplementedException(e.Name.LocalName);
            }
        }
    }
}
