using Microsoft.VisualStudio.TestTools.UnitTesting;
using Petroineos.Intraday.Lib;
using Petroineos.Intraday.Lib.Model;
using System.Collections.Generic;
using System.IO;
using Microsoft.Practices.Unity;
using PetroineosIntradayTradesExtractorService;

namespace Petroineos.Intraday.IntegrationTests
{
    
    [TestClass]
    public class TestCsvFileWriterTests
    {
        private static IUnityContainer _container;
        private ICsvFileWriter _csvFileWriter;
        private IPowerTradeAPI<IntraDayTradePosition> _serviceAPI;
        private  IPowerIntraDayReportFileNameBuilder _fileNameBuilder;


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
            _csvFileWriter = _container.Resolve<ICsvFileWriter>();
            _fileNameBuilder = _container.Resolve<IPowerIntraDayReportFileNameBuilder>();
            
        }

        [TestCleanup]
        public void MyTestCleanup()
        {
            _csvFileWriter = null;
            _fileNameBuilder = null;
           
        }
        #endregion

       
        [TestMethod]
        public void TestCsvWrite_WhenUsingMockIntradayTradePositions_ThenCreateCsvFileReportSucessfully()
        {

            IEnumerable<IntraDayTradePosition> IntraDayTrades = new List<IntraDayTradePosition>()
            {
                new  IntraDayPowerTradesPosition(1,"23:00", 60),
                new  IntraDayPowerTradesPosition(1,"00:00", 80),
                new  IntraDayPowerTradesPosition(1,"01:00", 180),
                new  IntraDayPowerTradesPosition(1,"02:00", 90),
                new  IntraDayPowerTradesPosition(1,"03:00", 120),
            };

            string  filename = _fileNameBuilder.GetFilename("PowerPosition");
            _csvFileWriter.Write(filename, IntraDayTrades, new List<string>() { "Local Time", "Volume" }.ToArray());

            Assert.IsTrue(File.Exists(filename));
            Assert.IsTrue(File.ReadAllBytes(filename).Length > 0);
        }

    
    }
}
