using System.Collections.Generic;

namespace Petroineos.Intraday.Lib.Model
{
    public class IntradayPositionsReport<T> where T : IntraDayTradePosition
    {
        private IEnumerable<T> _intraDayReport= new List<T>();
        protected  IEnumerable<T> IntraDayReport
        {
            get {return _intraDayReport; }
            set { _intraDayReport = value; }
            
        }
    }
}
