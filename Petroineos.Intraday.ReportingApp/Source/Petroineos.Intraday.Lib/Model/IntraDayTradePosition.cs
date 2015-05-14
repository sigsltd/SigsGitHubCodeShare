using System;

namespace Petroineos.Intraday.Lib.Model
{

    public enum CommodityType { Power, Coal, PetroChem, Metals };
    public  abstract class IntraDayTradePosition 
    {
        public int TradePeriod { get; set; }
        public string TradeTime { get; set; }
        public double TradePosition { get; set; }
                
        public abstract CommodityType TradeType { get; }
   
    }
}
