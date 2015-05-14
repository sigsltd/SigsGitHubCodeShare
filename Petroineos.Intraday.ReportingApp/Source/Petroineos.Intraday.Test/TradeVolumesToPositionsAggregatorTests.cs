using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Petroineos.Intraday.Lib;
using Petroineos.Intraday.Lib.Implementation;
using Services;

namespace Petroineos.Intraday.Test
{
    [TestClass]
    public class TradeVolumesToPositionsAggregatorTests
    {
        // TODO: address the issue that this is NOT POSSIBLE to unit test, since we can't set the VOLUME property
        [TestMethod]
        public void ShouldAggregatePowerTradeVolumes()
        {
            // Assemble
            var trade1 = PowerTrade.Create(new DateTime(2015, 01, 01), 2);
            var trade2 = PowerTrade.Create(new DateTime(2015, 01, 02), 10);
            var trade3 = PowerTrade.Create(new DateTime(2015, 01, 01), 2);

            // Act
            // Assert
            Assert.Inconclusive("Can't set Volume property on PowerTrade DTO");
        }

        #region Setup

        private Mock<IDateTimeFormatter> _dateTimeFormatterMock;
        private ITradeVolumesToPositionsAggregator _tradeVolumesToPositionsAggregator;

        [TestInitialize]
        public void MyTestInitialize()
        {
            log4net.Config.XmlConfigurator.Configure();
            _dateTimeFormatterMock = new Mock<IDateTimeFormatter>();
            _tradeVolumesToPositionsAggregator = new TradeVolumesToPositionsAggregator(_dateTimeFormatterMock.Object);
        }

        [TestCleanup]
        public void MyTestCleanup()
        {
            _dateTimeFormatterMock = null;
            _tradeVolumesToPositionsAggregator = null;
        }

        #endregion
    }
}