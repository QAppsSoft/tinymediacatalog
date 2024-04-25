using System.Xml.Serialization;

namespace Tools.IO.Kodi.Models;

[XmlRoot(ElementName="fileinfo")]
public class Fileinfo {
    [XmlElement(ElementName="streamdetails")]
    public Streamdetails Streamdetails { get; set; }
}