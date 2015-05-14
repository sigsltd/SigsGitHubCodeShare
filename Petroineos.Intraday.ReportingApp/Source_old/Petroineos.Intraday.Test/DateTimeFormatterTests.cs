using Microsoft.VisualStudio.TestTools.UnitTesting;
using Petroineos.Intraday.Lib.Implementation;
using System.Linq;

namespace Petroineos.Intraday.Test
{
    [TestClass]
    public class DateTimeFormatterTests
    {

        [TestMethod]
        public void MapPeriodTwoToZeroHours()
        {
            // Assemble
            const int period = 2;
            // Act
            var result = _dateTimeFormatter.GetFormattedTime(period);
            // Assert
            Assert.AreEqual("00:00", result, "Should return 00:00 hour");
        }

        [TestMethod]
        public void MapPeriodOneToTwentyThreeHours()
        {
            // Assemble
            const int period = 1;
            // Act
            var result = _dateTimeFormatter.GetFormattedTime(period);
            // Assert
            Assert.AreEqual("23:00", result, "Should return 23:00 hour");
        }

        [TestMethod]
        public void MapPeriodThreeToOneHour()
        {
            // Assemble
            const int period = 3;
            // Act
            var result = _dateTimeFormatter.GetFormattedTime(period);
            // Assert
            Assert.AreEqual("01:00", result, "Should return 01:00 hour");
        }

        [TestMethod]
        public void MapPeriodTwentyFourToTwentyTwoHours()
        {
            // Assemble
            const int period = 24;
            // Act
            var result = _dateTimeFormatter.GetFormattedTime(period);
            // Assert
            Assert.AreEqual("22:00", result, "Should return 22:00 hours");
        }

        [TestMethod]
        public void TestGetFormattedTime_WhenUsingMockPeriod_ThenReturnCorrectFormattedTime()
        {
            int[] Periods = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24 };
            var modResults = Periods.Select(p => _dateTimeFormatter.GetFormattedTime(p)).ToList();
            Assert.AreEqual(modResults.Count(), 24);
        }
        #region Setup

        private DateTimeFormatter _dateTimeFormatter;

        [TestInitialize()]
        public void MyTestInitialize()
        {
            _dateTimeFormatter = new DateTimeFormatter();
        }

        [TestCleanup()]
        public void MyTestCleanup()
        {
            _dateTimeFormatter = null;
        }
        #endregion


    }
}
