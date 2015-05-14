using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Petroineos.Intraday.Lib;
using Microsoft.Practices.Unity;
using PetroineosIntradayTradesExtractorService;

namespace Petroineos.Intraday.IntegrationTests
{
    [TestClass]
    public class PowerIntraDayReportBuilderTests
    {
        private static IUnityContainer _container;
        private IPowerIntraDayReportBuilder _reportBuilder;
       
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

        #region Test Setup And TearDown
        [TestInitialize]
        public void MyTestInitialize()
        {
            log4net.Config.XmlConfigurator.Configure();
            _reportBuilder = _container.Resolve<IPowerIntraDayReportBuilder>();
        }
               
        [TestCleanup]
        public void MyTestCleanup()
        {
            _reportBuilder = null;
        }
        #endregion

        [TestMethod]
        public void TestBuildReport_ReturnSuccess()
        {
            var result = _reportBuilder.BuildIntradayPowerTradePositionReport(DateTime.Now);
            var reportFileName = result.Result.ReportFilename;

            Assert.IsTrue(File.Exists(reportFileName));
            Assert.IsTrue(File.ReadAllBytes(reportFileName).Length > 0);
        }

    }
}