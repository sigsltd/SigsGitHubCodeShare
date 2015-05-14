namespace Petroineos.Intraday.Lib
{
    public interface IDateTimeFormatter
    {
        string GetFormattedTime(int period);
    }
}