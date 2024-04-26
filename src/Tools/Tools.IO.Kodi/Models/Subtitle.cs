using System.Diagnostics.CodeAnalysis;
using System.Xml.Serialization;

namespace Tools.IO.Kodi.Models;

[XmlRoot(ElementName = "subtitle")]
public class Subtitle
{
    private string _language = string.Empty;

    [AllowNull]
    [XmlElement(ElementName = "language")]
    public string Language
    {
        get => _language;
        set => _language = value ?? string.Empty;
    }
}