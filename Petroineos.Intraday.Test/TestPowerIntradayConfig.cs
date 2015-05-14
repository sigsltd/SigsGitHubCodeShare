using System;
using System.IO;
using System.Timers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Petroineos.Intraday.Lib.Implementation;

namespace Petroineos.Intraday.Test
{
    [TestClass]
    public class TestPowerIntradayConfig
    {
        [TestMethod]
        public void Test_GetConfigData_ReturnSuccess()
        {
            log4net.Config.XmlConfigurator.Configure();
            var timer = new Timer();
            var configurationProvider = new ConfigurationProvider();
            timer.Interval = TimeSpan.FromMinutes(configurationProvider.PollFrequencyInMinutes).TotalMilliseconds;
            Assert.IsTrue(configurationProvider.PollFrequencyInMinutes > 0);
            Assert.IsTrue(Directory.Exists(configurationProvider.CsvFilePath));
        }
    }
}