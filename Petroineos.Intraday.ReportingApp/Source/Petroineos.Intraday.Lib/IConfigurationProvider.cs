namespace Petroineos.Intraday.Lib
{
    public interface IConfigurationProvider
    {
        int AttempsToGetTrades { get; }
        int IntraDayTradesRetryIntervalInSeconds { get; }
        string CsvFilePath { get; }
        int PollFrequencyInMinutes { get; }
    }
}