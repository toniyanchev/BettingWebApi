using System.Xml.Serialization;

namespace WebApi.Models
{
    public class MatchTypeProxy
    {
        [XmlIgnore]
        public int Id { get; set; }
        [XmlIgnore]
        public string Name { get; set; }
        [XmlIgnore]
        public MatchProxy[] Matches { get; set; }
    }
}