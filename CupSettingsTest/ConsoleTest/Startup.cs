using System;
using System.Threading;
using System.Threading.Tasks;
using ConsoleTest.Infrastructure;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace ConsoleTest
{
    public class Startup : IHostedService
    {
        private readonly ToggleSettings _toggleSettings;
        private readonly GdasServiceSettings _gdasServiceSettings;

        public Startup(IOptions<ToggleSettings> toggleSettings, IOptions<GdasServiceSettings> gdasServiceSettings)
        {
            _toggleSettings = toggleSettings.Value;
            _gdasServiceSettings = gdasServiceSettings.Value;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine($"Toggle:{_toggleSettings.PODupCheckForEmeaApj}, gdas:{_gdasServiceSettings.PODupCheckUri}");
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Close the console app, Press any key to close the window..");
            return Task.CompletedTask;
        }
    }
}
