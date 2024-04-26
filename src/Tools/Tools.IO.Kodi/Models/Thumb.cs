using System.Xml.Serialization;

namespace Tools.IO.Kodi.Models;

[XmlRoot(ElementName = "thumb")]
public class Thumb
{
    [XmlAttribute(AttributeName = "aspect")]
    public string Aspect { get; set; }

    [XmlText]
    public string Text { get; set; }
}