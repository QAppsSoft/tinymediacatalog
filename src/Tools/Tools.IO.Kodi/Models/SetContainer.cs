using System.Diagnostics.CodeAnalysis;
using System.Xml.Serialization;

namespace Tools.IO.Kodi.Models;

[XmlRoot(ElementName = "set")]
public class SetContainer
{
    private string _name = string.Empty;
    private string _overview = string.Empty;

    [AllowNull]
    [XmlElement(ElementName = "name")]
    public string Name
    {
        get => _name;
        set => _name = value ?? string.Empty;
    }

    [AllowNull]
    [XmlElement(ElementName = "overview")]
    public string Overview
    {
        get => _overview;
        set => _overview = value ?? string.Empty;
    }
}