using System.Linq;
using System.Threading.Tasks;
using ConsoleTest.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging.Configuration;
using Microsoft.Extensions.Logging;
using System.IO;
using System;

namespace ConsoleTest
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            var cupsKeySettings = EnvironmentSettings.Create().ToList();
            string environmentName;
            string configPath;
            string configFile;

            SetManifestFilePath();

            var builder = new HostBuilder()
              .ConfigureAppConfiguration((hostingContext, config) =>
              {
                  var env = hostingContext.HostingEnvironment;
                  env.EnvironmentName = environmentName;

                  config.SetBasePath(configPath);
                  config.AddYamlFile(configFile, optional: true);
                  config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                  config.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);
                  config.AddEnvironmentVariables();
                  config.AddInMemoryCollection(cupsKeySettings);

                  if (args != null)
                  {
                      config.AddCommandLine(args);
                  }
              })
              .ConfigureServices((hostContext, services) =>
              {
                  services.AddOptions();
                  services.Configure<ToggleSettings>(hostContext.Configuration.GetSection("ToggleSettings"));
                  services.Configure<GdasServiceSettings>(hostContext.Configuration.GetSection("GdasServiceSettings"));
                  services.AddHostedService<Startup>();
                  //services.AddMvcCore().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
              })
              .ConfigureLogging((hostingContext, logging) =>
              {
                  logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                  logging.AddConsole();
              });

            //services.Configure<AppConfig>(hostContext.Configuration.GetSection("AppConfig"));
            //services.AddSingleton<IHostedService, PrintTextToConsoleService>();

            await builder.RunConsoleAsync();

            void SetManifestFilePath()
            {
                environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";
                configPath = Path.Combine(Environment.CurrentDirectory);
                configFile = "manifest.yml";
            }
        }
    }
}

