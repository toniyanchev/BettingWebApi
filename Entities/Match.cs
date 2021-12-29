using System;
using System.Collections.Generic;

namespace WebApi.Entities
{
    public class Match
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Event Event { get; set; }
        public DateTime Date { get; set; }
        public MatchType Type { get; set; }
        public IList<Bet> Bets { get; set; }
    }
}