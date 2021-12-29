using System.Collections.Generic;

namespace WebApi.Entities
{
    public class Sport
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<Event> Events { get; set; }
    }
}