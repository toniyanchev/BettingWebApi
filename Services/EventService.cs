using AppContext = WebApi.Helpers.AppContext;
using System.Linq;
using System;

namespace WebApi.Services
{
    public interface IEventService
    {
        dynamic GetDailyMatches();
        dynamic GetMatchById(int id);
        dynamic GetMatchesByType(int typeId);
    }

    public class EventService : IEventService
    {
        private readonly AppContext _context;

        public EventService(AppContext context)
        {
            _context = context;
        }

        public dynamic GetDailyMatches()
        {
            return _context.Matches
                .Where(x => x.Date > DateTime.Now.AddDays(-1))
                .ToList();
        }

        public dynamic GetMatchById(int id)
        {
            return _context.Matches
                .Where(x => x.Id == id)
                .SingleOrDefault();
        }

        public dynamic GetMatchesByType(int typeId)
        {
            return _context.Matches
                .Where(x => x.Type.Id == typeId)
                .ToList();
        }
    }
}