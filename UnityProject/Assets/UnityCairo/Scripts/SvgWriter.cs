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
                if (Fill.HasValue)
                {
                    var rgb = Fill.Value;
                    cr.set_source_rgb(rgb.r, rgb.g, rgb.b);
                    if (Stroke.HasValue)
                    {
                        cr.fill_preserve();
                    }
                    else
                    {
                        cr.fill();
                    }
                }
                if (Stroke.HasValue)
                {
                    var rgb = Stroke.Value;
                    cr.set_source_rgb(rgb.r, rgb.g, rgb.b);
                    cr.stroke();
                }
            }
        }

        struct Group
        {
            public double x;
            public double y;

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

                        default:
                            throw new NotImplementedException(a.Name.LocalName);
                    }

                }

                return g;
            }
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

        public static void Draw(Cairo cr, XElement e)
        {
            switch (e.Name.LocalName)
            {
                case "svg":
                    {
                        cr.save();
                        {
                            var g = Group.Parse(e);
                            cr.translate(g.x, g.y);

                            foreach (var child in e.Elements())
                            {
                                Draw(cr, child);
                            }
                        }
                        cr.restore();
                    }
                    break;

                case "rect":
                    DrawRect(cr, e);
                    break;

                case "circle":
                    DrawCircle(cr, e);
                    break;

                default:
                    throw new NotImplementedException(e.ToString());
            }
        }
    }
}
