using System.Diagnostics.CodeAnalysis;
using System.Xml;
using System.Xml.Serialization;

namespace Tools.XML
{
    /// <summary>
    /// XmlDocument helper class to read from XmlDocument object.
    /// </summary>
    public static class XRead
    {
        [RequiresUnreferencedCode("XmlSerializer")]
        public static T? ParseXml<T>(string fileName)
        {
            var reader = new XmlSerializer(typeof(T));
            using var file = XmlReader.Create(fileName);
            return (T?)reader.Deserialize(file);
        }
    }
}