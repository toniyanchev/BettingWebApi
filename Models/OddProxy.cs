using System.Xml.Serialization;

namespace WebApi.Models
{
    public class OddProxy
    {
        [XmlAttribute("ID")]
        public int Id { get; set; }
        [XmlAttribute("Name")]
        public string Name { get; set; }
        public BetProxy Bet { get; set; }
        [XmlAttribute("Value")]
        public decimal Value { get; set; }
        [XmlAttribute("SpecialBetValue")]
        public string SpecialValue { get; set; }
    }
}