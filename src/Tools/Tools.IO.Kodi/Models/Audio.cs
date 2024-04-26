using System.Xml.Serialization;

namespace Tools.IO.Kodi.Models;

[XmlRoot(ElementName = "audio")]
public class Audio
{
    [XmlElement(ElementName = "codec")]
    public string Codec { get; set; }

    [XmlElement(ElementName = "language")]
    public string Language { get; set; }

    [XmlElement(ElementName = "channels")]
    public string Channels { get; set; }
}