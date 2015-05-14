using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Petroineos.Intraday.Lib;
using Petroineos.Intraday.Lib.Implementation;
using Petroineos.Intraday.Lib.Model;
using Services;

namespace Petroineos.Intraday.Test
{
    [TestClass]
    public class TestPowerIntraDayReportBuilder
    {
        private ConfigurationProvider _configurationProvider;
        private ICsvFileWriter _csvFileWriter;
        private IPowerIntraDayReportFileNameBuilder _reportFileNameBuilder;
        private IPowerIntraDayReportBuilder _reportBuilder;
        private IPowerTradeAPI<IntraDayTradePosition> _serviceAPI;


        [TestMethod]
        public void TestBuildReport_WhenPassingInvalidDate_ReturnNoFilesCreated()
        {
            var result = _reportBuilder.BuildIntradayPowerTradePositionReport(new DateTime());
            var reportFileName = result.Result.ReportFilename;
            Assert.IsFalse(File.Exists(reportFileName));
        }

        [TestMethod]
        public void TestBuildReport_ReturnSuccess()
        {
            var result = _reportBuilder.BuildIntradayPowerTradePositionReport(DateTime.Now);
            var reportFileName = result.Result.ReportFilename;

            Assert.IsTrue(File.Exists(reportFileName));
            Assert.IsTrue(File.ReadAllBytes(reportFileName).Length > 0);
        }

        [TestInitialize]
        public void MyTestInitialize()
        {
            log4net.Config.XmlConfigurator.Configure();
            _configurationProvider = new ConfigurationProvider();
            _reportFileNameBuilder = new PowerIntraDayReportFileNameBuilder(_configurationProvider);
            _serviceAPI = new PowerTradeAPI(new PowerService(), new TradeVolumesToPositionsAggregator(new DateTimeFormatter()), new ConfigurationProvider() );
            _csvFileWriter = new CsvFileWriter();
            _reportBuilder = new PowerIntraDayReportBuilder(_serviceAPI, _csvFileWriter, _reportFileNameBuilder);
        }

        [TestCleanup]
        public void MyTestCleanup()
        {
            _configurationProvider = null;
            _serviceAPI = null;
            _csvFileWriter = null;
            _reportBuilder = null;
            _reportFileNameBuilder = null;
        }
    }
}