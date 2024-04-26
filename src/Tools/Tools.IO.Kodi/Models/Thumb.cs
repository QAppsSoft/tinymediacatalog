using System.Diagnostics.CodeAnalysis;
using System.Xml.Serialization;

namespace Tools.IO.Kodi.Models;

[XmlRoot(ElementName = "thumb")]
public class Thumb
{
    private string _aspect = string.Empty;
    private string _text = string.Empty;

    [AllowNull]
    [XmlAttribute(AttributeName = "aspect")]
    public string Aspect
    {
        get => _aspect;
        set => _aspect = value ?? string.Empty;
    }

    [AllowNull]
    [XmlText]
    public string Text
    {
        get => _text;
        set => _text = value ?? string.Empty;
    }
}