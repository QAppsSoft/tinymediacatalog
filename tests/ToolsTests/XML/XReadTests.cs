using System.Xml;
using Models.Common;
using TestsCommons;
using Tools.XML;

namespace ToolsTests.XML;

[TestFixture]
public class XReadTests
{
    #region OpenPath Tests

    [Test]
    public void OpenPath_with_empty_path_should_throw_ArgumentNullException()
    {
        var xmlPath = string.Empty;

        var action = () => _ = XRead.OpenPath(xmlPath);

        action.Should().Throw<ArgumentException>();
    }
    
    [Test]
    public void OpenPath_with_null_path_should_throw_ArgumentNullException()
    {
        const string xmlPath = null!;

        var action = () => _ = XRead.OpenPath(xmlPath);

        action.Should().Throw<ArgumentNullException>();
    }
    
    [Test]
    public void OpenPath_with_xml_path_should_return_xml_document()
    {
        var xmlPath = Resources.GetResourceFilePath("kodi-movie.nfo");

        var xml = XRead.OpenPath(xmlPath);

        xml.Should().NotBeNull();
    }

    #endregion

    #region GetBool Tests

    [Test]
    public void GetBool_correct_tag_should_return_correct_bool_value()
    {
        var xml = GetXmlDocument();

        var value = XRead.GetBool(xml, "lockdata");

        value.Should().BeTrue();
    }
    
    [Test]
    public void GetBool_wrong_tag_should_return_false_bool_value()
    {
        var xml = GetXmlDocument();

        var value = XRead.GetBool(xml, "title");

        value.Should().BeFalse();
    }

    #endregion

    #region GetRatings Tests

    [Test]
    public void Get_ratings_should_return_correct_values()
    {
        var xml = GetXmlDocument();

        var result = XRead.GetRatings(xml);

        result.Count.Should().Be(2);
        result[0].Should().BeEquivalentTo(new RatingModel
        {
            Name = "themoviedb",
            Default = false,
            Max = 10,
            Value = 7.0,
            Votes = 9572,
        });
        result[1].Should().BeEquivalentTo(new RatingModel
        {
            Name = "imdb",
            Default = true,
            Max = 10,
            Value = 3.2,
            Votes = 10000,
        });
    }

    #endregion
    
    
    private XmlDocument GetXmlDocument()
    {
        var xmlPath = Resources.GetResourceFilePath("kodi-movie.nfo");

        var xml = XRead.OpenPath(xmlPath);
        
        return xml;
    }
}