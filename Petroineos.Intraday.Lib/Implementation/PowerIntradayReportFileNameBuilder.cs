using System;
using System.Text;

namespace Petroineos.Intraday.Lib.Implementation
{
    public class PowerIntraDayReportFileNameBuilder : IPowerIntraDayReportFileNameBuilder
    {
        private readonly IConfigurationProvider _configurationProvider;

        public PowerIntraDayReportFileNameBuilder(IConfigurationProvider configurationProvider)
        {
            if (configurationProvider == null) throw new ArgumentNullException("configurationProvider");
            _configurationProvider = configurationProvider;
        }

        public string GetFilename(string prefix)
        {
            return _configurationProvider.CsvFilePath + @"\" + BuildCsvFileName(prefix);
        }

        private string BuildCsvFileName(string prefix)
        {
            if (string.IsNullOrEmpty(prefix))
            {
                return DateTime.Now.ToString("yyyyMMdd_HH:mm") + ".csv";
            }
            return new StringBuilder(prefix).AppendFormat("_{0}", DateTime.Now.ToString("yyyyMMdd_HHmm")) + ".csv";
        }
    }
}