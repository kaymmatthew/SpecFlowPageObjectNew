using Microsoft.Extensions.Logging;

namespace SpecFlowPageObjectNew.Support
{
    public static class Log
    {
        private static readonly ILoggerFactory _factory = LoggerFactory.Create(builder =>
        {
            builder
                .AddSimpleConsole(options =>
                {
                    options.SingleLine = true;
                    options.TimestampFormat = "yyyy-MM-dd HH:mm:ss ";
                })
                .SetMinimumLevel(LogLevel.Information);
        });

        public static ILogger CreateLogger<T>() => _factory.CreateLogger<T>();

        public static ILogger Logger => _factory.CreateLogger("SpecFlowTests");
    }
}
