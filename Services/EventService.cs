using AppContext = WebApi.Helpers.AppContext;

namespace WebApi.Services
{
    public interface IEventService
    {
    }

    public class EventService : IEventService
    {
        private readonly AppContext _context;

        public EventService(AppContext context)
        {
            _context = context;
        }
    }
}