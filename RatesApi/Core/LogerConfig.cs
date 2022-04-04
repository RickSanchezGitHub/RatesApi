using NLog;
using NLog.Config;

namespace RatesApi.Core
{
    public class LogerConfig
    {       
        public static void ConfigureNlog()
        {
            //var path = Environment.GetEnvironmentVariable("TEST");
            //var config = new LoggingConfiguration();
            //// Targets where to log to: File and Console
            //var logfile = new NLog.Targets.FileTarget(path) { FileName = "file.txt" };
            //var logconsole = new NLog.Targets.ConsoleTarget("logconsole");

            //// Rules for mapping loggers to targets            
            //config.AddRule(LogLevel.Debug, LogLevel.Fatal, logconsole);
            //config.AddRule(LogLevel.Debug, LogLevel.Fatal, logfile);

            //// Apply config           
            //LogManager.Configuration = config;
        }
    }
}
