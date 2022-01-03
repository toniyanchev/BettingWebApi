using System.Xml.Serialization;

namespace WebApi.Models
{
    public class BetProxy
    {
        [XmlAttribute("ID")]
        public int Id { get; set; }
        [XmlAttribute("Name")]
        public string Name { get; set; }
        [XmlIgnore]
        public MatchProxy Match { get; set; }
        [XmlAttribute("IsLive")]
        public bool IsLive { get; set; }
        [XmlElement("Odd")]
        public OddProxy[] Odds { get; set; }
    }
}