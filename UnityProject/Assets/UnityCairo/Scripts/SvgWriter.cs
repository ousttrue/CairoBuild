using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;


namespace UnityCairo
{
    static class SvgWriter
    {
        public struct Rgb
        {
            public double r;
            public double g;
            public double b;

            public Rgb(double _r, double _g, double _b)
            {
                r = _r;
                g = _g;
                b = _b;
            }

            public static Rgb Parse(string rgb)
            {
                if (rgb[0] != '#') throw new Exception();
                if (rgb.Length == 7)
                {
                    return new Rgb
                    {
                        r = Convert.ToByte(rgb.Substring(1, 2), 16) / 255.0,
                        g = Convert.ToByte(rgb.Substring(3, 2), 16) / 255.0,
                        b = Convert.ToByte(rgb.Substring(5, 2), 16) / 255.0,
                    };
                }
                else if(rgb.Length == 4)
                {
                    return new Rgb
                    {
                        r = Convert.ToByte(rgb.Substring(1, 1), 16) / 15.0,
                        g = Convert.ToByte(rgb.Substring(2, 1), 16) / 15.0,
                        b = Convert.ToByte(rgb.Substring(3, 1), 16) / 15.0,
                    };
                }
                else
                {
                    throw new NotImplementedException();
                }
            }
        }

        public struct Style
        {
            public Rgb? Stroke;
            public Rgb? Fill;
            public double? StrokeWidth;

            public void ParseFill(string src)
            {
                if (src == "transparent")
                {
                    Fill = null;
                }
                else if(src == "none")
                {
                    Fill = null;
                }
                else if (src.StartsWith("#"))
                {
                    Fill = Rgb.Parse(src);
                }
                else
                {
                    throw new NotImplementedException(src);
                }
            }

            public void ParseStroke(string src)
            {
                if (src == "black")
                {
                    Stroke = new Rgb(0, 0, 0);
                }
                else if (src.StartsWith("#"))
                {
                    Stroke = Rgb.Parse(src);
                }
                else
                {
                    throw new NotImplementedException(src);
                }
            }

            public void ParseStrokeWidth(string src)
            {
                StrokeWidth = double.Parse(src);
            }

            public void ParseStyle(string src)
            {
                foreach (var token in src.Split(';'))
                {
                    var splited = token.Split(':');
                    switch (splited[0].Trim())
                    {
                        case "stroke":
                            {
                                var value = splited[1].Trim();
                                if (value == "none")
                                {
                                    Stroke = null;
                                }
                                else
                                {
                                    Stroke = Rgb.Parse(value);
                                }
                            }
                            break;

                        case "fill":
                            if (splited[1].Trim().StartsWith("none"))
                            {
                                Fill = null;
                            }
                            else
                            {
                                Fill = Rgb.Parse(splited[1].Trim());
                            }
                            break;
                    }
                }
            }

            public void Parse(XElement e)
            {
                {
                    var attr = e.Attribute("style");
                    if (attr != null)
                    {
                        ParseStyle(attr.Value);
                    }
                }

                {
                    var attr = e.Attribute("stroke");
                    if (attr != null)
                    {
                        ParseStroke(attr.Value);
                    }
                }

                {
                    var attr = e.Attribute("fill");
                    if (attr != null)
                    {
                        ParseFill(attr.Value);
                    }
                }
            }

            public void Apply(Cairo cr)
            {
                if (StrokeWidth.HasValue)
                {
                    cr.set_line_width(StrokeWidth.Value);
                }

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
                var r = new Rotate
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
                    t.rotate = Rotate.Parse(src.Substring(7, src.Length - 8));
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

            public static Group Parse(XElement x, Context context)
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

                        case "stroke":
                            context.Style.ParseStroke(a.Value);
                            break;

                        case "stroke-width":
                            context.Style.ParseStrokeWidth(a.Value);
                            break;

                        case "stroke-linejoin":
                        case "stroke-linecap":
                            break;

                        case "fill":
                            context.Style.ParseFill(a.Value);
                            break;

                        case "id":
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

        static void DrawLine(Cairo cr, XElement e, Context context)
        {
            var x1 = double.Parse(e.Attribute("x1").Value);
            var y1 = double.Parse(e.Attribute("y1").Value);
            var x2 = double.Parse(e.Attribute("x2").Value);
            var y2 = double.Parse(e.Attribute("y2").Value);
            cr.move_to(x1, y1);
            cr.line_to(x2, y2);
            context.Style.Parse(e);
            context.Style.Apply(cr);
        }

        static void DrawRect(Cairo cr, XElement e, Context context)
        {
            var x = double.Parse(e.Attribute("x").Value);
            var y = double.Parse(e.Attribute("y").Value);
            var w = double.Parse(e.Attribute("width").Value);
            var h = double.Parse(e.Attribute("height").Value);
            cr.rectangle(x, y, w, h);
            context.Style.Parse(e);
            context.Style.Apply(cr);
        }

        static void DrawCircle(Cairo cr, XElement e, Context context)
        {
            var cx = double.Parse(e.Attribute("cx").Value);
            var cy = double.Parse(e.Attribute("cy").Value);
            var r = double.Parse(e.Attribute("r").Value);
            cr.arc(cx, cy, r, 0, Math.PI * 2);
            context.Style.Parse(e);
            context.Style.Apply(cr);
        }

        static void DrawText(Cairo cr, XElement e, Context context)
        {
            var x = double.Parse(e.Attribute("x").Value);
            var y = double.Parse(e.Attribute("y").Value);

            cr.select_font_face("Sans",
                cairo_font_slant_t.CAIRO_FONT_SLANT_NORMAL,
                cairo_font_weight_t.CAIRO_FONT_WEIGHT_NORMAL);
            cr.set_font_size(24);

#if true
            context.Style.Parse(e);
            //context.Style.Apply(cr);
            cr.move_to(x, y);
            cr.show_text(e.Value.Trim());
#else
            cr.move_to(x, y);
            cr.text_path(e.Value.Trim()); // use cairo_glyph_path ?
            style.Apply(cr);
#endif
        }

        public class ParsePosition
        {
            public string Src { get; private set; }

            int m_pos;

            public char Value
            {
                get
                {
                    return Src[m_pos];
                }
            }

            public bool IsEnd
            {
                get
                {
                    return m_pos >= Src.Length;
                }
            }

            public ParsePosition(string src)
            {
                Src = src;
            }

            public void Skip(char c)
            {
                if (Src[m_pos] != c) throw new Exception();
                ++m_pos;
            }

            public void SkipSpace()
            {
                for (; m_pos < Src.Length; ++m_pos)
                {
                    if (!char.IsWhiteSpace(Src[m_pos]))
                    {
                        break;
                    }
                }
            }

            /*
            public void SkipComma()
            {
                if (Src[m_pos] != ',') throw new Exception("not comma");
                ++m_pos;
            }
            */

            void SkipNonNumber()
            {
                for(; m_pos<Src.Length; ++m_pos)
                {
                    switch(Src[m_pos])
                    {
                        case ' ':
                        case ',':
                        case 'z':
                            break;

                        case '-':
                        case '0':
                        case '1':
                        case '2':
                        case '3':
                        case '4':
                        case '5':
                        case '6':
                        case '7':
                        case '8':
                        case '9':
                            return;

                        default:
                            throw new NotImplementedException(string.Format("SkipNonNumber: {0}", Src.Substring(m_pos)));
                    }
                }
            }

            public double ParseDouble()
            {
                SkipNonNumber();

                var start = m_pos;
                for (; m_pos < Src.Length; ++m_pos)
                {
                    if (m_pos == start && Src[m_pos]=='-')
                    {
                        // ok
                    }
                    else if(char.IsDigit(Src[m_pos]) || Src[m_pos] == '.')
                    {
                        // ok
                    }
                    else
                    {
                        break;
                    }
                }
                var substr = Src.Substring(start, m_pos - start);
                return double.Parse(substr);
            }

            public List<double> ParseDoubles()
            {
                List<double> values = new List<double>();
                try
                {
                    while (!IsEnd)
                    {
                        values.Add(ParseDouble());
                    }
                }
                catch (Exception ex)
                {
                    //UnityEngine.Debug.LogWarningFormat("{0}", ex);
                }
                return values;
            }

            public override string ToString()
            {
                return Src.Substring(m_pos);
            }
        }

        public interface ICommand
        {
            void Apply(Cairo cr);
        }

        public class MoveCommand: ICommand
        {
            bool isRelative;
            double tx;
            double ty;

            // M50, 50
            public static MoveCommand Parse(ParsePosition current)
            {
                current.Skip('M');
                return new MoveCommand
                {
                    tx = current.ParseDouble(),
                    ty = current.ParseDouble()
                };
            }

            public static IEnumerable<ICommand> ParseRelative(ParsePosition current)
            {
                current.Skip('m');

                var values = current.ParseDoubles();
                if (values.Count % 2 != 0) throw new Exception();

                var it = values.GetEnumerator();
                while (it.MoveNext())
                {
                    var tx = it.Current;
                    it.MoveNext();
                    var ty = it.Current;

                    yield return new MoveCommand
                    {
                        isRelative = true,
                        tx = tx,
                        ty = ty
                    };
                }
            }           

            public void Apply(Cairo cr)
            {
                if (isRelative)
                {
                    cr.rel_move_to(tx, ty);
                }
                else
                {
                    cr.move_to(tx, ty);
                }
            }
        }

        public class ArcCommand : ICommand
        {
            double rx;
            double ry;
            double tx;
            double ty;

            // A30,30 0 0,0 70,70
            public static ArcCommand Parse(ParsePosition current)
            {
                current.Skip('A');

                var rx = current.ParseDouble();
                var ry = current.ParseDouble();
                var rot = current.ParseDouble();
                var large = current.ParseDouble();
                var sweep = current.ParseDouble();
                var tx = current.ParseDouble();
                var ty = current.ParseDouble();
                return new ArcCommand
                {
                    rx = rx,
                    ry = ry,
                    tx = tx,
                    ty = ty,
                };
            }

            public void Apply(Cairo cr)
            {
                if (rx == ry)
                {
                    double x = 0;
                    double y = 0;
                    cr.get_current_point(ref x, ref y);

                    cr.arc(
                        x + (tx - x) / 2, 
                        y + (ty - y) / 2, 
                        rx, 
                        0, 
                        UnityEngine.Mathf.PI);
                }
                else
                {
                    // ellipse. not implemented
                }
                cr.move_to(tx, ty);
            }
        }

        class LineCommand : ICommand
        {
            bool isRelative;
            double tx;
            double ty;

            public static IEnumerable<ICommand> Parse(ParsePosition current)
            {
                var value = current.Value;
                current.Skip(value);

                var values = current.ParseDoubles();

                if (value == 'h')
                {
                    if (values.Count != 1)
                    {
                        throw new Exception();
                    }
                    yield return new LineCommand
                    {
                        tx = values[0],
                        ty = 0
                    };
                    yield break;
                }

                if (value == 'v'){
                    if (values.Count != 1)
                    {
                        throw new Exception();
                    }
                    yield return new LineCommand
                    {
                        tx = 0,
                        ty = values[0]
                    };
                    yield break;
                }

                if (values.Count % 2 != 0) throw new Exception("2");

                var it = values.GetEnumerator();
                while (it.MoveNext())
                {
                    var tx = it.Current;
                    it.MoveNext();
                    var ty = it.Current;

                    switch (value)
                    {
                        case 'L':
                            yield return new LineCommand
                            {
                                tx = tx,
                                ty = ty,
                            };
                            break;

                        case 'l':
                            yield return new LineCommand
                            {
                                isRelative = true,
                                tx = tx,
                                ty = ty,
                            };
                            break;

                        default:
                            throw new NotImplementedException();
                    }
                }
            }

            public void Apply(Cairo cr)
            {
                if (isRelative)
                {
                    cr.rel_line_to(tx, ty);
                }
                else
                {
                    cr.line_to(tx, ty);
                }
            }
        }

        class CurveCommand : ICommand
        {
            bool isRelative;
            double x1, y1;
            double x2, y2;
            double x, y;

            public double SmoothX1
            {
                get
                {
                    if (isRelative)
                    {
                        return -x;
                    }
                    else
                    {
                        throw new NotImplementedException();
                    }
                }
            }

            public double SmoothY1
            {
                get
                {
                    if (isRelative)
                    {
                        return -y;
                    }
                    else
                    {
                        throw new NotImplementedException();
                    }
                }
            }

            public static IEnumerable<ICommand> Parse(ParsePosition current, bool isRelative)
            {
                current.Skip(isRelative
                    ? 'c'
                    : 'C'
                    );

                List<double> values = new List<double>();
                try
                {
                    while (!current.IsEnd)
                    {
                        values.Add(current.ParseDouble());
                    }
                }
                catch (Exception ex)
                {
                    UnityEngine.Debug.LogWarningFormat("{0}", ex);
                }
                if (values.Count % 6 != 0) throw new Exception("6");

                var it = values.GetEnumerator();
                while (it.MoveNext())
                {
                    var x1 = it.Current;
                    it.MoveNext();
                    var y1 = it.Current;
                    it.MoveNext();
                    var x2 = it.Current;
                    it.MoveNext();
                    var y2 = it.Current;
                    it.MoveNext();
                    var x = it.Current;
                    it.MoveNext();
                    var y = it.Current;

                    yield return new CurveCommand
                    {
                        isRelative = isRelative,
                        x1 = x1,
                        y1 = y1,
                        x2 = x2,
                        y2 = y2,
                        x = x,
                        y = y,
                    };
                }
            }

            public static IEnumerable<ICommand> ParseSmoothRelative(ParsePosition current, CurveCommand last)
            {
                current.Skip('s');

                List<double> values = new List<double>();
                try
                {
                    while (!current.IsEnd)
                    {
                        values.Add(current.ParseDouble());
                    }
                }
                catch (Exception ex)
                {
                    //UnityEngine.Debug.LogFormat("{0}", current);
                }
                if (values.Count % 4 != 0)
                {
                    throw new Exception("4");
                }


                var it = values.GetEnumerator();
                while (it.MoveNext())
                {
                    var x2 = it.Current;
                    it.MoveNext();
                    var y2 = it.Current;
                    it.MoveNext();
                    var x = it.Current;
                    it.MoveNext();
                    var y = it.Current;

                    double x1=0;
                    double y1=0;
                    if (last == null)
                    {

                    }
                    else if (last.isRelative)
                    {
                        x1 = last.SmoothX1;
                        y1 = last.SmoothY1;
                    }
                    else
                    {
                        throw new NotImplementedException();
                    }

                    last = new CurveCommand
                    {
                        isRelative = true,
                        x1 = x1,
                        y1 = y1,
                        x2 = x2,
                        y2 = y2,
                        x = x,
                        y = y,
                    };
                    yield return last;
                }
            }

            public void Apply(Cairo cr)
            {
                if (isRelative)
                {
                    cr.rel_curve_to(x1, y1, x2, y2, x, y);
                }
                else
                {
                    cr.curve_to(x1, y1, x2, y2, x, y);
                }
            }
        }

        class CloseCommand : ICommand
        {
            public static CloseCommand Parse(ParsePosition current)
            {
                current.Skip('z');
                return new CloseCommand();
            }

            public void Apply(Cairo cr)
            {
                cr.close_path();
            }
        }


        class Path
        {
            List<ICommand> m_commands = new List<ICommand>();

            public static Path Parse(string src)
            {
                var path = new Path();

                var current = new ParsePosition(src);
                while (!current.IsEnd)
                {
                    current.SkipSpace();
                    switch (current.Value)
                    {
                        case 'M':
                            path.m_commands.Add(MoveCommand.Parse(current));
                            break;

                        case 'm':
                            path.m_commands.AddRange(MoveCommand.ParseRelative(current));
                            break;

                        case 'A':
                            path.m_commands.Add(ArcCommand.Parse(current));
                            break;

                        case 'L':
                        case 'l':
                        case 'v':
                        case 'h':
                            path.m_commands.AddRange(LineCommand.Parse(current));
                            break;

                        case 'Q':
                            //path.m_commands.Add(QuadraticBezierCurve.Parse(current));
                            break;

                        case 'C':
                            path.m_commands.AddRange(CurveCommand.Parse(current, false));
                            break;

                        case 'c':
                            path.m_commands.AddRange(CurveCommand.Parse(current, true));
                            break;

                        case 's':
                            path.m_commands.AddRange(CurveCommand.ParseSmoothRelative(current, path.m_commands.Last() as CurveCommand));
                            break;

                        case 'z':
                            path.m_commands.Add(CloseCommand.Parse(current));
                            break;

                        default:
                            throw new NotImplementedException(string.Format("unknown: {0}", current));
                    }
                }

                return path;
            }

            public void Draw(Cairo cr)
            {
                foreach(var command in m_commands)
                {
                    command.Apply(cr);
                }
            }
        }

        static void DrawPath(Cairo cr, XElement e, Context context)
        {
            var d = e.Attribute("d");

            var path = Path.Parse(d.Value);
            path.Draw(cr);

            context.Style.Parse(e);
            context.Style.Apply(cr);
        }

        public class Context
        {
            public Style Style;
            public double X;
            public double Y;
        }

        public static void Draw(Cairo cr, XElement e, Context context = null)
        {
            if (context == null)
            {
                context = new Context();
            }

            switch (e.Name.LocalName)
            {
                case "g":
                case "svg":
                    {
                        cr.save();
                        {
                            var g = Group.Parse(e, context);
                            g.Apply(cr);

                            foreach (var child in e.Elements())
                            {
                                Draw(cr, child, context);
                            }
                        }
                        cr.restore();
                    }
                    break;

                case "line":
                    DrawLine(cr, e, context);
                    break;

                case "rect":
                    DrawRect(cr, e, context);
                    break;

                case "circle":
                    DrawCircle(cr, e, context);
                    break;

                case "text":
                    DrawText(cr, e, context);
                    break;

                case "path":
                    DrawPath(cr, e, context);
                    break;

                default:
                    throw new NotImplementedException(e.Name.LocalName);
            }
        }
    }
}
