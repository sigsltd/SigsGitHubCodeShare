

using System;
using Petroineos.Intraday.Lib.Model;

namespace Petroineos.Intraday.Lib
{
    public interface IPowerIntraDayReportBuilder
    {
        OperationResult<PowerIntraDayReport> BuildIntradayPowerTradePositionReport(DateTime asOfDate);
    }
}
