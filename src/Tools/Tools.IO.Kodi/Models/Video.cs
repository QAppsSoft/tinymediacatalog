using System.Xml.Serialization;

namespace Tools.IO.Kodi.Models;

[XmlRoot(ElementName = "video")]
public class Video
{
    [XmlElement(ElementName = "codec")]
    public string Codec { get; set; }

    [XmlElement(ElementName = "aspect")]
    public string Aspect { get; set; }

    [XmlElement(ElementName = "width")]
    public string Width { get; set; }

    [XmlElement(ElementName = "height")]
    public string Height { get; set; }

    [XmlElement(ElementName = "durationinseconds")]
    public string DurationInSeconds { get; set; }

    [XmlElement(ElementName = "stereomode")]
    public string StereoMode { get; set; }
}