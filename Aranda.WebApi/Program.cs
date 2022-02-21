using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Aranda.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                                            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                                            .AddUserSecrets(typeof(Startup).GetTypeInfo().Assembly, true)
                                            .AddEnvironmentVariables()
                                            .AddCommandLine(args).Build();
            Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(configuration).MinimumLevel.Is(minimumLevel: Serilog.Events.LogEventLevel.Warning).CreateLogger();
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, config) =>
                { }).ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>().UseSerilog(); });
    }
}
