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
        }

        static void DrawRect(XElement e, Cairo cr)
        {
            var x = double.Parse(e.Attribute("x").Value);
            var y = double.Parse(e.Attribute("y").Value);
            var w = double.Parse(e.Attribute("width").Value);
            var h = double.Parse(e.Attribute("height").Value);
            var style = Style.Parse(e.Attribute("style").Value);
            cr.rectangle(x, y, w, h);
            if (style.Fill.HasValue)
            {
                var rgb = style.Fill.Value;
                cr.set_source_rgb(rgb.r, rgb.g, rgb.b);
                if (style.Stroke.HasValue)
                {
                    cr.fill_preserve();
                }
                else
                {
                    cr.fill();
                }
            }
            if (style.Stroke.HasValue)
            {
                var rgb = style.Stroke.Value;
                cr.set_source_rgb(rgb.r, rgb.g, rgb.b);
                cr.stroke();
            }
        }

        public static void Draw(XElement e, Cairo cr)
        {
            switch (e.Name.LocalName)
            {
                case "rect":
                    DrawRect(e, cr);
                    break;

                default:
                    throw new NotImplementedException(e.ToString());
            }
        }
    }
}