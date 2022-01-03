using System.Collections.Generic;
using WebApi.Models;

namespace WebApi.Entities
{
    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Sport Sport { get; set; }
        public bool IsLive { get; set; }
        public IList<Match> Matches { get; set; }
        public int OldId { get; set; }

        public Event() { }
        public Event(EventProxy proxy)
        {
            Id = 0;
            OldId = proxy.Id;
            Name = proxy.Name;
            IsLive = proxy.IsLive;
        }
    }
}