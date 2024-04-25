using System.Xml.Serialization;

namespace Tools.IO.Kodi.Models;

[XmlRoot(ElementName="streamdetails")]
public class Streamdetails {
    [XmlElement(ElementName="video")]
    public Video Video { get; set; }
    [XmlElement(ElementName="audio")]
    public List<Audio> Audio { get; set; }
    [XmlElement(ElementName="subtitle")]
    public Subtitle Subtitle { get; set; }
}