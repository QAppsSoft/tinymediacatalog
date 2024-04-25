using System.Xml.Serialization;

namespace Tools.IO.Kodi.Models;

[XmlRoot(ElementName="subtitle")]
public class Subtitle {
    [XmlElement(ElementName="language")]
    public string Language { get; set; }
}