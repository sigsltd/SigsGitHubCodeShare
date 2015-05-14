using System.Collections.Generic;
using Petroineos.Intraday.Lib.Model;
using Services;

namespace Petroineos.Intraday.Lib
{
    public interface ITradeVolumesToPositionsAggregator
    {
        IEnumerable<IntraDayTradePosition> Aggregate(IEnumerable<PowerTrade> intraDayPowerTrades);
    }
}