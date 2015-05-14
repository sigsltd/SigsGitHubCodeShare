using System;
using System.Collections.Generic;
using Petroineos.Intraday.Lib.Model;

namespace Petroineos.Intraday.Lib
{
    public interface IPowerTradeAPI<T> where T : IntraDayTradePosition
    {
        IEnumerable<IntraDayTradePosition> GetIntradayTrades(DateTime date);
    }
}
