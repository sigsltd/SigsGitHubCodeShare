using Petroineos.Intraday.Lib.Model;
using System.Collections.Generic;


namespace Petroineos.Intraday.Lib
{
    public interface ICsvFileWriter
    {
        void Write(string csvFilePath, IEnumerable<IntraDayTradePosition> intraDayTrades, string[] headerNames = null);

    }
}
