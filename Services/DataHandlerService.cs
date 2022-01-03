using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;
using AppContext = WebApi.Helpers.AppContext;

namespace WebApi.Services
{
    public class DataHandlerService : IHostedService
    {
        private Timer _timer = null!;

        public DataHandlerService()
        {
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
            //_eventService.ProcessXmlData();
        }
    }
}