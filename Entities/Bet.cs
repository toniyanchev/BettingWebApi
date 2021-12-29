using System.Collections.Generic;

namespace WebApi.Entities
{
    public class Bet
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Match Match { get; set; }
        public bool IsLive { get; set; }
        public IList<Odd> Odds { get; set; }
    }
}