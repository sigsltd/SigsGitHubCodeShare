using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Petroineos.Intraday.Lib;
using Petroineos.Intraday.Lib.Model;
using Microsoft.Practices.Unity;
using PetroineosIntradayTradesExtractorService;

namespace Petroineos.Intraday.IntegrationTests
{
    [TestClass]
    public class PowerTradeAPITests
    {
        private static IUnityContainer _container;
        private  IDateTimeFormatter _dateTimeFormatter;
        private  IPowerTradeAPI<IntraDayTradePosition> _serviceAPI;

        #region Setup

        [ClassInitialize]
        public static void MyClassInitialize(TestContext testContext)
        {
            _container = new UnityContainer();
            _container.AddNewExtension<IntraDayReportingConfiguration>();
        }

        [ClassCleanup]
        public static void MyClassCleanup()
        {
            _container.Dispose();
        }

        #endregion

        #region Setup And TearDown
        [TestInitialize]
        public void MyTestInitialize()
        {
            log4net.Config.XmlConfigurator.Configure();
            _serviceAPI = _container.Resolve<IPowerTradeAPI<IntraDayTradePosition>>();
            _dateTimeFormatter = _container.Resolve<IDateTimeFormatter>();
            
        }

        [TestCleanup]
        public void MyTestCleanup()
        {
            _serviceAPI = null;
            _dateTimeFormatter = null;
        }
        #endregion
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