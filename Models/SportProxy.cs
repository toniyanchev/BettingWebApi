using System.Xml.Serialization;

namespace WebApi.Models
{
    public class SportProxy
    {
        [XmlAttribute("ID")]
        public int Id { get; set; }
        [XmlAttribute("Name")]
        public string Name { get; set; }
        [XmlElement("Event")]
        public EventProxy[] Events { get; set; }
    }
}