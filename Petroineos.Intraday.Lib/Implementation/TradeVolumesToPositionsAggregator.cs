using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using log4net;
using Petroineos.Intraday.Lib.Model;
using Services;

namespace Petroineos.Intraday.Lib.Implementation
{
    public class TradeVolumesToPositionsAggregator : ITradeVolumesToPositionsAggregator
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly IDateTimeFormatter _dateTimeFormatter;

        public TradeVolumesToPositionsAggregator(IDateTimeFormatter dateTimeFormatter)
        {
            if (dateTimeFormatter == null) throw new ArgumentNullException("dateTimeFormatter");
            _dateTimeFormatter = dateTimeFormatter;
        }

        public IEnumerable<IntraDayTradePosition> Aggregate(IEnumerable<PowerTrade> intraDayPowerTrades)
        {
            var powerTradePeriods = ExtractIntraDayPowerTradePeriods(intraDayPowerTrades);
            var intradayPositions = BuildIntradayPowerTradeReportData(powerTradePeriods);
            return intradayPositions;
        }

        private IEnumerable<PowerPeriod> ExtractIntraDayPowerTradePeriods(IEnumerable<PowerTrade> intraDayPowerTrades)
        {
            Log.Info("ExtractIntraDayPowerTradePositions for trades");
            var powerTradePeriods = intraDayPowerTrades.Select(p => p.Periods).SelectMany(pp => pp.ToList());
            Log.Info("ExtractIntraDayPowerTradePositions succeeded");
            return powerTradePeriods;
        }

        private IEnumerable<IntraDayTradePosition> BuildIntradayPowerTradeReportData(
            IEnumerable<PowerPeriod> intraDayPowerPeriod)
        {
            Log.Info("BuildIntradayPowerTradeReportData for trades");

            var intraDayTradePositions = (from trade in intraDayPowerPeriod
                group trade.Volume by new {trade.Period}
                into g
                select
                    new IntraDayPowerTradesPosition(g.Key.Period, _dateTimeFormatter.GetFormattedTime(g.Key.Period),g.Sum())).ToList();

            Log.Info("BuildIntradayPowerTradeReportData succeeded");
            return intraDayTradePositions;
        }
    }
}