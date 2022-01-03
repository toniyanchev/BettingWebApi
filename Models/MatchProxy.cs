using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace WebApi.Models
{
    public class MatchProxy
    {
        [XmlAttribute("ID")]
        public int Id { get; set; }
        [XmlAttribute("Name")]
        public string Name { get; set; }
        public EventProxy Event { get; set; }
        [XmlAttribute("StartDate")]
        public DateTime Date { get; set; }
        [XmlAttribute("MatchType")]
        public string Type { get; set; }
        [XmlElement("Bet")]
        public BetProxy[] Bets { get; set; }
    }
}