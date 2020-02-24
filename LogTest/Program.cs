using log4net;
using log4net.Config;
using System;
using System.IO;

namespace LogTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var repository = LogManager.CreateRepository("logTestRepository");
            var config = new FileInfo("log4net.config");
            XmlConfigurator.Configure(repository,config);
            var log = LogManager.GetLogger(repository.Name, "NetCoreLogTestLogger");
            log.Debug("Base Config Log Debug Test");
            Console.WriteLine("Hello World!");
            Console.ReadKey();
        }
    }
}
