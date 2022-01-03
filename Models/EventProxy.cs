using System.Xml.Serialization;

namespace WebApi.Models
{
    public class EventProxy
    {
        [XmlAttribute("ID")]
        public int Id { get; set; }
        [XmlAttribute("Name")]
        public string Name { get; set; }
        [XmlIgnore]
        public SportProxy Sport { get; set; }
        [XmlAttribute("IsLive")]
        public bool IsLive { get; set; }
        [XmlElement("Match")]
        public MatchProxy[] Matches { get; set; }
    }
}