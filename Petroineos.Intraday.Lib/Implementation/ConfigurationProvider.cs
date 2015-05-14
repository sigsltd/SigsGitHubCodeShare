using System;
using System.Configuration;

namespace Petroineos.Intraday.Lib.Implementation
{
    public class ConfigurationProvider : IConfigurationProvider
    {
        private const int DefaultPollFrequencyInMinutes = 1;
        private const int DefaultAttempsToGetTrades = 5;
        private const int DefaultIntraDayTradesRetryIntervalInSeconds = 15;

        private int _attempsToGetTrades;
        private string _csvFilePath;
        private bool _isLoaded;
        private int _pollFrequencyInMinutes;
        private int _intraDayTradesRetryIntervalInSeconds;

        public int AttempsToGetTrades
        {
            get
            {
                CheckIfSettingsAreLoaded();
                return _attempsToGetTrades;
            }
        }

        public int IntraDayTradesRetryIntervalInSeconds
        {
            get
            {
                CheckIfSettingsAreLoaded();
                return _intraDayTradesRetryIntervalInSeconds;
            }
        }

        public string CsvFilePath
        {
            get
            {
                CheckIfSettingsAreLoaded();
                return _csvFilePath;
            }
        }

        public int PollFrequencyInMinutes
        {
            get
            {
                CheckIfSettingsAreLoaded();
                return _pollFrequencyInMinutes;
            }
        }

        private void CheckIfSettingsAreLoaded()
        {
            if (_isLoaded == false)
            {
                LoadSettings();
            }
        }

        private void LoadSettings()
        {
            _csvFilePath = ConfigurationManager.AppSettings["CsvFilePath"];
            var pollFrequenceIsValid = Int32.TryParse(ConfigurationManager.AppSettings["PollFrequencyInMinutes"],out _pollFrequencyInMinutes);
            if (pollFrequenceIsValid == false)
            {
                _pollFrequencyInMinutes = DefaultPollFrequencyInMinutes;
            }

            var attempsToGetTradesIsValid = Int32.TryParse(ConfigurationManager.AppSettings["AttempsToGetTrades"],out _attempsToGetTrades);
            if (attempsToGetTradesIsValid == false)
            {
                _attempsToGetTrades = DefaultAttempsToGetTrades;
            }
            

            var intraDayTradesRetryIntervalInSecondsIsValid = Int32.TryParse(
                ConfigurationManager.AppSettings["IntraDayTradesRetryIntervalInSeconds"],out _intraDayTradesRetryIntervalInSeconds);
            if (intraDayTradesRetryIntervalInSecondsIsValid == false)
            {
                _intraDayTradesRetryIntervalInSeconds = DefaultIntraDayTradesRetryIntervalInSeconds;
            }

            _isLoaded = true;
        }
    }
}