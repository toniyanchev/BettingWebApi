using System.Collections.Generic;

namespace WebApi.Entities
{
    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Sport Sport { get; set; }
        public bool IsLive { get; set; }
        public IList<Match> Matches { get; set; }
    }
}