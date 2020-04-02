namespace MessageQueue.WebApi
{
    using System;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Hosting;
    using Serilog;

    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = LogInitializer.GetLogger();

            try
            {
                Log.Information("Initializing...");

                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly.");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                    .ConfigureKestrel(serverOptions =>
                    {
                        serverOptions.Limits.MaxConcurrentConnections = 100;
                        serverOptions.Limits.KeepAliveTimeout = TimeSpan.FromMilliseconds(60000);
                    })
                    .UseUrls("http://+:5000")
                    .UseStartup<Startup>();
                })
                .UseSerilog();
    }
}
