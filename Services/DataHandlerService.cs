using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;
using AppContext = WebApi.Helpers.AppContext;

namespace WebApi.Services
{
    public class DataHandlerService : IHostedService
    {
        private readonly IEventService _eventService;
        private Timer _timer = null!;

        public DataHandlerService(IServiceProvider serviceProvider)
        {
            _eventService = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<IEventService>();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
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