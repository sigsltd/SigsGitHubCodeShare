namespace Petroineos.Intraday.Lib.Model
{
    public class IntraDayPowerTradesPosition : IntraDayTradePosition
    {
        public IntraDayPowerTradesPosition(int period, string time, double position)
        {
            TradePeriod = period;
            TradePosition = position;
            TradeTime = time;
        }

        public override CommodityType TradeType
        {
            get { return CommodityType.Power; }
        }
    }
}