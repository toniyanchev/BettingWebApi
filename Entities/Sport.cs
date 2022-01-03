using System.Collections.Generic;
using WebApi.Models;

namespace WebApi.Entities
{
    public class Sport
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<Event> Events { get; set; }

        public Sport() { }
        public Sport(SportProxy proxy)
        {
            Id = proxy.Id;
            Name = proxy.Name;
        }
    }
}