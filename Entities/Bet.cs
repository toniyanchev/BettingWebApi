using System.Collections.Generic;
using WebApi.Models;

namespace WebApi.Entities
{
    public class Bet
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Match Match { get; set; }
        public bool IsLive { get; set; }
        public IList<Odd> Odds { get; set; }

        public Bet(BetProxy proxy)
        {
            Id = proxy.Id;
            Name = proxy.Name;
            IsLive = proxy.IsLive;
        }
    }
}