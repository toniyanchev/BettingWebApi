using System.Collections.Generic;

namespace WebApi.Entities
{
    public class MatchType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<Match> Matches { get; set; }
    }
}