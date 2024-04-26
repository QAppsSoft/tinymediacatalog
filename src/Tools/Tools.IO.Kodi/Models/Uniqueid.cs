using System.Diagnostics.CodeAnalysis;
using System.Xml.Serialization;

namespace Tools.IO.Kodi.Models;

[XmlRoot(ElementName = "uniqueid")]
public class UniqueId
{
    private string _default = string.Empty;
    private string _text = string.Empty;
    private string _type = string.Empty;

    [AllowNull]
    [XmlAttribute(AttributeName = "default")]
    public string Default
    {
        get => _default;
        set => _default = value ?? string.Empty;
    }

    [AllowNull]
    [XmlAttribute(AttributeName = "type")]
    public string Type
    {
        get => _type;
        set => _type = value ?? string.Empty;
    }

    [AllowNull]
    [XmlText]
    public string Text
    {
        get => _text;
        set => _text = value ?? string.Empty;
    }
}