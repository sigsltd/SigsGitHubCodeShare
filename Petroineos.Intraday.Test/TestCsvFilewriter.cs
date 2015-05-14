using Microsoft.VisualStudio.TestTools.UnitTesting;
using Petroineos.Intraday.Lib;
using Petroineos.Intraday.Lib.Model;
using System;
using System.Collections.Generic;
using System.IO;
using Petroineos.Intraday.Lib.Implementation;
using Moq;

namespace Petroineos.Intraday.Test
{
    [TestClass]
    public class TestCsvFilewriter
    {
        #region Setup

        ICsvFileWriter _csvWriter;
        IConfigurationProvider _configurationProvider;
        IPowerIntraDayReportFileNameBuilder _fileNameBuilder;

        [TestInitialize]
        public void MyTestInitialize()
        {
            log4net.Config.XmlConfigurator.Configure();
            _csvWriter = new CsvFileWriter();
            _configurationProvider = new ConfigurationProvider();
            _fileNameBuilder = Mock.Of<IPowerIntraDayReportFileNameBuilder>();
        }

        [TestCleanup]
        public void MyTestCleanup()
        {
            _csvWriter = null;
            _configurationProvider = null;
            _fileNameBuilder = null;
        }
        #endregion
       
        [TestMethod]
        public void TestCsvWrite_WhenUsingMockIntradayTradePositionsWithoutHeaderRow_ThenCreateCsvFileReportSucessfully()
        {
            //Assemble
           IEnumerable<IntraDayTradePosition> IntraDayTrades = new List<IntraDayTradePosition>()
            {
                new  IntraDayPowerTradesPosition(1,"23:00", 60),
                new  IntraDayPowerTradesPosition(1,"00:00", 80),
                new  IntraDayPowerTradesPosition(1,"01:00", 180),
                new  IntraDayPowerTradesPosition(1,"02:00", 90),
                new  IntraDayPowerTradesPosition(1,"03:00", 120),
            };
            
            Mock.Get(_fileNameBuilder).Setup(f => f.GetFilename("PowerPosition")).Returns("PowerPosition_2015_1400.csv");
            //Prefer  not to mock the directory path, usually I would mock the config directory path too
            string fileName = _configurationProvider.CsvFilePath + @"\" + _fileNameBuilder.GetFilename("PowerPosition");

            //Act
            _csvWriter.Write(fileName, IntraDayTrades, null);

            //Assert
            Assert.IsTrue(File.Exists(fileName.ToString()));
            Assert.IsTrue(File.ReadAllBytes(fileName).Length > 0);
        }

        [TestMethod] 
        public void TestCsvWrite_WhenUsingMockIntradayTradePositionsWithHeaderRow_ThenCreateCsvFileReportSucessfully()
        {
            //Assemble
            IEnumerable<IntraDayTradePosition> IntraDayTrades = new List<IntraDayTradePosition>()
            {
                new  IntraDayPowerTradesPosition(1,"23:00", 60),
                new  IntraDayPowerTradesPosition(1,"00:00", 80),
                new  IntraDayPowerTradesPosition(1,"01:00", 180),
                new  IntraDayPowerTradesPosition(1,"02:00", 90),
                new  IntraDayPowerTradesPosition(1,"03:00", 120),
            };
            
            Mock.Get(_fileNameBuilder).Setup(f => f.GetFilename("PowerPosition")).Returns("PowerPosition_2015_1400.csv");
            //Prefer  not to mock the directory path, usually I would mock the config directory path too
            string fileName = _configurationProvider.CsvFilePath + @"\" + _fileNameBuilder.GetFilename("PowerPosition");

            //Act
            _csvWriter.Write(fileName,IntraDayTrades, new List<string>() { "Local Time", "Volume" }.ToArray());

            //Assert
             Assert.IsTrue(File.Exists(fileName.ToString()));
             Assert.IsTrue(File.ReadAllBytes(fileName).Length > 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCsvWrite_WhenProvideInvalidPath_ThenThrowException()
        {
            
            //Assemble
            IEnumerable<IntraDayTradePosition> IntraDayTrades = new List<IntraDayTradePosition>()
            {
                new  IntraDayPowerTradesPosition(1,"23:00", 60),
            };

            //Act
            _csvWriter.Write("", IntraDayTrades, null);


        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCsvWrite_WhenProvideNoTradesProvided_ThenThrowException()
        {
            //Assemble
            Mock.Get(_fileNameBuilder).Setup(f => f.GetFilename("PowerPosition")).Returns("PowerPosition_2015_1400.csv");
            IConfigurationProvider configurationProvider = new ConfigurationProvider();
            string fileName = configurationProvider.CsvFilePath + @"\" + _fileNameBuilder.GetFilename("PowerPosition");

            //Act
            _csvWriter.Write("", null, null);
        }



    }
}
