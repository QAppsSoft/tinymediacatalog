namespace Tools.XML.Interfaces;

public interface IXmlRead
{
    T? ParseXml<T>(string fileName);
}