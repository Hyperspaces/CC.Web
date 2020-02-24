using log4net;
using log4net.Config;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace CC.Web.Test.Core.log4
{
    public class log4Test
    {
        private ILog log;

        [SetUp]
        public void Setup()
        {
            var repository = LogManager.CreateRepository("logTestRepository");
            BasicConfigurator.Configure(repository);
            log = LogManager.GetLogger(repository.Name,"NetCoreLogTestLogger");
            //XmlConfigurator.Configure()
        }

        [Test]
        public void BaseConfigLogTest()
        {
            log.Debug("Base Config Log Debug Test");
        }
    }
}
