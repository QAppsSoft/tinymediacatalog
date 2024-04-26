using System.Diagnostics.CodeAnalysis;
using System.Xml.Serialization;

namespace Tools.IO.Kodi.Models;

[XmlRoot(ElementName = "audio")]
public class Audio
{
    private string _codec = string.Empty;
    private string _language = string.Empty;
    private string _channels = string.Empty;

    [AllowNull]
    [XmlElement(ElementName = "codec")]
    public string Codec
    {
        get => _codec;
        set => _codec = value ?? string.Empty;
    }

    [AllowNull]
    [XmlElement(ElementName = "language")]
    public string Language
    {
        get => _language;
        set => _language = value ?? string.Empty;
    }

    [AllowNull]
    [XmlElement(ElementName = "channels")]
    public string Channels
    {
        get => _channels;
        set => _channels = value ?? string.Empty;
    }
}