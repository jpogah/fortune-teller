using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Steeltoe.Extensions.Logging;
using Steeltoe.Extensions.Configuration.CloudFoundry;
using System.Threading.Tasks;
using FortuneTeller.Service.Models;
using Steeltoe.Extensions.Configuration.ConfigServer;

namespace FortuneTeller.Service
{
    public class Program
    {
        public static void  Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {

            var builder = WebHost.CreateDefaultBuilder(args)
                 .UseDefaultServiceProvider(configure => configure.ValidateScopes = false)
                //.UseCloudFoundryHosting() //Enable listening on a Env provided port
                 .AddCloudFoundry() //Add cloudfoundry environment variables as a configuration source
                 .ConfigureAppConfiguration((hostingContext, config) =>
                 {
                     var env = hostingContext.HostingEnvironment;

                     config
                         .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                         .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                         .AddConfigServer()
                         .AddEnvironmentVariables();

                     if (args != null)
                     {
                         config.AddCommandLine(args);
                     }
                 })
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
