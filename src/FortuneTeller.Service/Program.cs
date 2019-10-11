using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Steeltoe.Extensions.Logging;
using Steeltoe.Extensions.Configuration.CloudFoundry;
using System.IO;
using System.Threading.Tasks;
using FortuneTeller.Service.Models;

namespace FortuneTeller.Service
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = await Task.FromResult(CreateWebHostBuilder(args).Build());

            using (var scope = host.Services.CreateScope())
            {
                await SampleData.InitializeFortunesAsync(scope.ServiceProvider);
            }

            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {

            var builder = WebHost.CreateDefaultBuilder(args)
                 .UseDefaultServiceProvider(configure => configure.ValidateScopes = false)
                 .UseCloudFoundryHosting() //Enable listening on a Env provided port
                 .AddCloudFoundry() //Add cloudfoundry environment variables as a configuration source
                 .UseStartup<Startup>();
            builder.ConfigureLogging((hostingContext, loggingBuilder) =>
            {
                loggingBuilder.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                loggingBuilder.AddDynamicConsole();
            });
            return builder;
        }
    }
}
