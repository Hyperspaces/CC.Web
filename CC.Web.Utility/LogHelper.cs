using log4net;
using log4net.Config;
using System;
using System.IO;

namespace CC.Web.Utility
{
    public static class LogHelper
    {
        private static ILog Logger { get; set; }

        static LogHelper()
        {
            var repository = LogManager.CreateRepository("logTestRepository");
            var configPath = Path.Combine(Directory.GetCurrentDirectory(), "log4net.config");
            var config = new FileInfo(configPath);
            XmlConfigurator.Configure(repository, config);
            Logger = LogManager.GetLogger(repository.Name, "NetCoreLogTestLogger");
            Logger.Debug("Base Config Log Debug start");
        }

        public static void DebugLog(string message)
        {
            Logger.Debug(message);
        }
    }
}
