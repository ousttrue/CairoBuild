using System;
using System.Collections;
using System.Xml.Linq;
using UnityCairo;
using UnityEngine;


public class WriteSvg : MonoBehaviour
{
    [SerializeField, TextArea(4, 24)]
    string m_svg = @"<svg  xmlns=""http://www.w3.org/2000/svg""
      xmlns:xlink=""http://www.w3.org/1999/xlink"">
    <rect x = ""10"" y=""10"" height=""100"" width=""100""
          style=""stroke:#ff0000; fill: #0000ff""/>
</svg>
";

    [SerializeField]
    string m_url = "https://upload.wikimedia.org/wikipedia/commons/f/fd/Ghostscript_Tiger.svg";

    [SerializeField]
    bool m_useUrl;

    [SerializeField]
    int m_width = 512;

    [SerializeField]
    int m_height = 512;

    [SerializeField]
    Shader m_shader;

    private void Reset()
    {
        m_shader = Shader.Find("Unlit/Transparent");
    }

    public Texture2D Texture;

    private void OnValidate()
    {
        if (!Application.isPlaying)
        {
            return;
        }

        StopAllCoroutines();

        StartCoroutine(WriteSVG());
    }

    IEnumerator WriteSVG()
    {
        if (m_useUrl)
        {
            var www = new WWW(m_url);
            yield return www;
            if (!string.IsNullOrEmpty(www.error))
            {
                yield break;
            }
            m_svg = www.text;
        }

        var material = new Material(m_shader);

        Texture = new Texture2D(m_width, m_height, TextureFormat.BGRA32, false);
        material.mainTexture = Texture;
        var data = new Byte[m_width * m_height * 4];

        using (var surface = Surface.CreateFromBytes(data, m_width, m_height, m_width * 4))
        {
            //using (var surface = Surface.Create(120, 120))
            using (var cr = Cairo.Create(surface))
            {
                // flip vertical
                cr.translate(0, m_height);
                cr.scale(1, -1);

                var document = XDocument.Parse(m_svg);
                foreach (var x in document.Root.Elements())
                {
                    SvgWriter.Draw(cr, x);
                }
            }

            Texture.LoadRawTextureData(data);
            Texture.Apply();
        }

        var quad = GameObject.CreatePrimitive(PrimitiveType.Quad);
        var renderer = quad.GetComponent<Renderer>();
        renderer.sharedMaterial = material;
    }
}
