namespace MessageQueue.WebApi
{
    using System;
    using System.IO;
    using MessageQueue.Common.Extensions;
    using Microsoft.Extensions.Configuration;
    using Serilog;
    using Serilog.Core;

    public static class LogInitializer
    {
        private const string AppSettings = "appsettings";
        private const string AspNetCoreEnv = "ASPNETCORE_ENVIRONMENT";
        private const string ProductionName = "Production";
        private const string JsonExtension = "json";

        public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(AppSettings.ToFileName(JsonExtension), optional: false, reloadOnChange: true)
            .AddJsonFile(AppSettings.FormatFileName(ExtendedFileNameSection, JsonExtension), optional: true)
            .AddEnvironmentVariables()
            .Build();

        public static Logger GetLogger() =>
            new LoggerConfiguration()
                .ReadFrom.Configuration(Configuration)
                .Enrich.FromLogContext()
                .WriteTo.Debug()
                .WriteTo.Console(
                    outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj} {Properties:j}{NewLine}{Exception}")
                .CreateLogger();

        private static string ExtendedFileNameSection => $"{Environment.GetEnvironmentVariable(AspNetCoreEnv) ?? ProductionName}";
}
}
