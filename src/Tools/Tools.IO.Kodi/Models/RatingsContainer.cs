using System.Diagnostics.CodeAnalysis;
using System.Xml.Serialization;

namespace Tools.IO.Kodi.Models;

[XmlRoot(ElementName = "ratings")]
public class RatingsContainer
{
    private List<Rating> _rating = [];

    [AllowNull]
    [XmlElement(ElementName = "rating")]
    public List<Rating> Rating
    {
        get => _rating;
        set => _rating = value ?? [];
    }
}