using System;
using System.Collections.Generic;
using WebApi.Models;

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
        public int OldId { get; set; }

        public Match() { }
        public Match(MatchProxy proxy)
        {
            Id = 0;
            OldId = proxy.Id;
            Name = proxy.Name;
            Date = proxy.Date;
        }

        public int GetTypeId(string stringType) => stringType switch
        {
            "PreMatch" => 1,
            "Live" => 2,
            "Outright" => 3,
            _ => 0,
        };

    }
}