using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace MyWorkerService
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            using var hostBuilder = CreateHostBuilder(args).Build();

            var assemblyName = typeof(Program).Assembly.GetName().Name;
            hostBuilder.CreateSerilogLogger(assemblyName);
            Log.Information($"Serilog Logger created. Starting {assemblyName}...");

            await hostBuilder
                .RunAsync()
                .ConfigureAwait(false);
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .UseWindowsService()
                .ConfigureServices((hostContext, services) =>
                {
                    // LOG HERE WITH SERILOG
                    services.AddHostedService<Worker>();
                });
    }
}
