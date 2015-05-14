using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Petroineos.Intraday.Lib;
using Petroineos.Intraday.Lib.Implementation;
using Petroineos.Intraday.Lib.Model;
using Services;

namespace Petroineos.Intraday.Test
{
    [TestClass]
    public class TestPowerTradeAPI
    {
        private  IDateTimeFormatter _dateTimeFormatter;
        private IPowerTradeAPI<IntraDayTradePosition> _serviceAPI;

        [TestInitialize]
        public void MyTestInitialize()
        {
            log4net.Config.XmlConfigurator.Configure();
            _serviceAPI = new PowerTradeAPI(new PowerService(), new TradeVolumesToPositionsAggregator(new DateTimeFormatter()), new ConfigurationProvider());
            _dateTimeFormatter = new DateTimeFormatter();
        }

        [TestCleanup]
        public void MyTestCleanup()
        {
            _dateTimeFormatter = null;
            _serviceAPI = null;
            
        }

        public TestPowerTradeAPI()
        {
            
            _serviceAPI = new PowerTradeAPI(new PowerService(), new TradeVolumesToPositionsAggregator(new DateTimeFormatter()), new ConfigurationProvider());
            _dateTimeFormatter = new DateTimeFormatter();
        }

        
        [TestMethod]
        public void TestGetIntradayTradePositions_WhenValidDate_ThenReturnSuccess()
        {
            var postions = _serviceAPI.GetIntradayTrades(DateTime.Now);
            Assert.IsTrue(postions.Count() > 0);
        }

        [TestMethod]
        public void TestGetIntradayTradePositions_WhenRunMultipleTimes_ThenReturnIntraDayTradePositonsWithSuccess()
        {
            for (var i = 0; i < 10; i++)
            {
                var postions = _serviceAPI.GetIntradayTrades(DateTime.Now);
                Assert.IsTrue(postions.Count() > 0);
            }
        }
    }
}