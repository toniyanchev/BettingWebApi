using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;
using AppContext = WebApi.Helpers.AppContext;

namespace WebApi.Services
{
    public interface IDataHandlerService
    {
    }

    public class DataHandlerService : IHostedService, IDataHandlerService
    {
        private readonly EventService _eventService;
        private readonly AppContext _context;
        private Timer _timer = null!;

        public DataHandlerService()
        {
            _eventService = new EventService(_context);
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            // timer repeates call to RemoveScheduledAccounts every 24 hours.
            _timer = new Timer(
                HandleData,
                null,
                TimeSpan.Zero,
                TimeSpan.FromMinutes(1)
            );

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        private void HandleData(object state)
        {
            _eventService.ProcessXmlData();
        }
    }
}