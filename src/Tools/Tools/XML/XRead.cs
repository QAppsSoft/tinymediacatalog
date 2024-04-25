using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Models.Common;

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
        
        /// <summary>
        /// The get bool.
        /// </summary>
        /// <param name="doc">
        /// The doc.
        /// </param>
        /// <param name="tag">
        /// The tag.
        /// </param>
        /// <returns>
        /// The get bool.
        /// </returns>
        public static bool GetBool(XmlDocument doc, string tag)
        {
            string value = GetString(doc, tag);
            bool outValue = false;
            bool.TryParse(value, out outValue);
            return outValue;
        }

        /// <summary>
        /// Gets a date time value from an XmlDocument.
        /// </summary>
        /// <param name="doc">
        /// The XMlDocument Object
        /// </param>
        /// <param name="tag">
        /// The tag to extract
        /// </param>
        /// <returns>
        /// </returns>
        public static DateTime GetDateTime(XmlDocument doc, string tag)
        {
            string value = GetString(doc, tag);
            var outValue = new DateTime();
            if (string.IsNullOrEmpty(value))
            {
                return outValue;
            }

            DateTime dt;
            DateTime.TryParse(value, out dt);
            return dt;
        }

        /// <summary>
        /// The get date time.
        /// </summary>
        /// <param name="doc">
        /// The doc.
        /// </param>
        /// <param name="tag">
        /// The tag.
        /// </param>
        /// <param name="format">
        /// The format.
        /// </param>
        /// <returns>
        /// </returns>
        public static DateTime GetDateTime(XmlDocument doc, string tag, string format)
        {
            string value = GetString(doc, tag, 0);
            DateTime outValue;

            if (!string.IsNullOrEmpty(format))
            {
                DateTime.TryParseExact(value, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out outValue);
                return outValue;
            }

            DateTime.TryParse(value, out outValue);
            return outValue;
        }

        /// <summary>
        /// Gets a double value from an XmlDocument.
        /// </summary>
        /// <param name="doc">
        /// The XMlDocument Object
        /// </param>
        /// <param name="tag">
        /// The tag to extract
        /// </param>
        /// <returns>
        /// The get double.
        /// </returns>
        public static double GetDouble(XmlDocument doc, string tag)
        {
            string value = GetString(doc, tag);
            double outValue;
            double.TryParse(value, out outValue);
            return outValue;
        }
        
        /// <summary>
        /// Gets a float value from an XmlDocument.
        /// </summary>
        /// <param name="doc">
        /// The XMlDocument Object
        /// </param>
        /// <param name="tag">
        /// The tag to extract
        /// </param>
        /// <returns>
        /// The get float.
        /// </returns>
        public static float GetFloat(XmlDocument doc, string tag)
        {
            var value = GetString(doc, tag);
            float.TryParse(value, CultureInfo.InvariantCulture, out var outValue);
            return outValue;
        }

        /// <summary>
        /// Gets a int value from an XmlDocument.
        /// </summary>
        /// <param name="doc">
        /// The XMlDocument Object
        /// </param>
        /// <param name="tag">
        /// The tag to extract
        /// </param>
        /// <returns>
        /// The get int.
        /// </returns>
        public static int GetInt(XmlDocument doc, string tag)
        {
            string value = GetString(doc, tag);
            int outValue;
            int.TryParse(value, out outValue);
            return outValue;
        }

        /// <summary>
        /// The get long.
        /// </summary>
        /// <param name="doc">
        /// The doc.
        /// </param>
        /// <param name="tag">
        /// The tag.
        /// </param>
        /// <returns>
        /// The get long.
        /// </returns>
        public static long GetLong(XmlDocument doc, string tag)
        {
            string value = GetString(doc, tag);
            long outValue;
            long.TryParse(value, out outValue);
            return outValue;
        }

        /// <summary>
        /// Gets a string object from an XmlDocument.
        /// </summary>
        /// <param name="doc">
        /// The XMlDocument Object
        /// </param>
        /// <param name="tag">
        /// The tag to extract
        /// </param>
        /// <param name="index">
        /// The index of the tag (Default = 0)
        /// </param>
        /// <returns>
        /// The get string.
        /// </returns>
        public static string GetString(XmlDocument doc, string tag, int index = 0)
        {
            if (doc.GetElementsByTagName(tag).Count > 0)
            {
                string value = doc.GetElementsByTagName(tag)[index].InnerText;

                value = value.Replace("&#35;", "#");
                value = value.Replace("&amp;", "&");

                return value;
            }

            return string.Empty;
        }

        /// <summary>
        /// The get strings.
        /// </summary>
        /// <param name="doc">
        /// The doc.
        /// </param>
        /// <param name="tag">
        /// The tag.
        /// </param>
        /// <returns>
        /// </returns>
        public static List<string> GetStrings(XmlDocument doc, string tag)
        {
            return (from XmlNode s in doc.GetElementsByTagName(tag) select s.InnerText).ToList();
        }

        /// <summary>
        /// The get u int.
        /// </summary>
        /// <param name="doc">
        /// The doc.
        /// </param>
        /// <param name="tag">
        /// The tag.
        /// </param>
        /// <returns>
        /// </returns>
        public static uint? GetUInt(XmlDocument doc, string tag)
        {
            string value = GetString(doc, tag);
            uint outValue;
            uint.TryParse(value, out outValue);
            return outValue;
        }

        /// <summary>
        /// Get a list of RatingModel
        /// </summary>
        /// <param name="doc">
        /// The doc.
        /// </param>
        /// <returns></returns>
        public static List<RatingModel> GetRatings(XmlDocument doc)
        {
            var sets = doc.GetElementsByTagName("rating");

            if (sets.Count == 0)
            {
                return [];
            }

            var ratings = new List<RatingModel>(sets.Count);

            foreach (XmlElement node in sets)
            {
                var document = OpenXml("<x>" + node.InnerXml + "</x>");
                
                var name = string.Empty;
                if (TryGetAttribute(node, "name" , out var nameValue))
                {
                    name = nameValue;
                }
                
                var isDefault = false;
                if (TryGetAttribute(node, "default" , out var defaultResult) && bool.TryParse(defaultResult, out var defaultValue))
                {
                    isDefault = defaultValue;
                }
                
                var max = 0;
                if (TryGetAttribute(node, "max" , out var defaultMax) && int.TryParse(defaultMax, CultureInfo.InvariantCulture, out var maxValue))
                {
                    max = maxValue;
                }

                var value = Math.Round(GetFloat(document, "value"), 1);
                var votes = GetInt(document, "votes");

                ratings.Add(new RatingModel
                {
                    Name = name,
                    Default = isDefault,
                    Max = max,
                    Value = value,
                    Votes = votes,
                });
            }

            return ratings;
        }

        /// <summary>
        /// The open path.
        /// </summary>
        /// <param name="path">
        /// The path.
        /// </param>
        /// <returns>
        /// </returns>
        public static XmlDocument? OpenPath(string path)
        {
            string xml = File.ReadAllText(path, Encoding.UTF8);
            var doc = new XmlDocument();
            try
            {
                doc.LoadXml(xml);
                return doc;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// The open xml.
        /// </summary>
        /// <param name="xml">
        /// The xml.
        /// </param>
        /// <returns>
        /// </returns>
        public static XmlDocument OpenXml(string xml)
        {
            var doc = new XmlDocument();
            doc.LoadXml(xml);
            return doc;
        }

        private static bool TryGetAttribute(XmlNode node, string tag, [NotNullWhen(true)] out string? value)
        {
            value = null;
                    
            var attribute = node.Attributes?[tag];
                
            if (attribute == null) return false;
                
            value = attribute.Value;
            return true;
        }
    }
}