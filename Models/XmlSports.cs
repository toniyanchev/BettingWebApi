using System.Xml.Serialization;

namespace WebApi.Models
{
    public class XmlSports
    {
        [XmlElement("Sport")]
        public SportProxy[] Sports { get; set; }
    }
}