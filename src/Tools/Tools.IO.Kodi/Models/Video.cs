using System.Diagnostics.CodeAnalysis;
using System.Xml.Serialization;

namespace Tools.IO.Kodi.Models;

[XmlRoot(ElementName = "video")]
public class Video
{
    private string _aspect = string.Empty;
    private string _codec = string.Empty;
    private string _durationInSeconds = string.Empty;
    private string _height = string.Empty;
    private string _stereoMode = string.Empty;
    private string _width = string.Empty;

    [AllowNull]
    [XmlElement(ElementName = "codec")]
    public string Codec
    {
        get => _codec;
        set => _codec = value ?? string.Empty;
    }

    [AllowNull]
    [XmlElement(ElementName = "aspect")]
    public string Aspect
    {
        get => _aspect;
        set => _aspect = value ?? string.Empty;
    }

    [AllowNull]
    [XmlElement(ElementName = "width")]
    public string Width
    {
        get => _width;
        set => _width = value ?? string.Empty;
    }

    [AllowNull]
    [XmlElement(ElementName = "height")]
    public string Height
    {
        get => _height;
        set => _height = value ?? string.Empty;
    }

    [AllowNull]
    [XmlElement(ElementName = "durationinseconds")]
    public string DurationInSeconds
    {
        get => _durationInSeconds;
        set => _durationInSeconds = value ?? string.Empty;
    }

    [AllowNull]
    [XmlElement(ElementName = "stereomode")]
    public string StereoMode
    {
        get => _stereoMode;
        set => _stereoMode = value ?? string.Empty;
    }
}