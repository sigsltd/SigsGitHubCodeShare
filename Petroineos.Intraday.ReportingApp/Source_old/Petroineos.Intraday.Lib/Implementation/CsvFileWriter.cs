using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using log4net;
using Petroineos.Intraday.Lib.Model;

namespace Petroineos.Intraday.Lib.Implementation
{
    public class CsvFileWriter : ICsvFileWriter
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
      
        public void Write(string csvFilePath, IEnumerable<IntraDayTradePosition> intraDayTrades, string[] headerNames = null)
        {
            Log.Info("In CsvFileWriter (Write)");
            if (string.IsNullOrEmpty(csvFilePath)) throw new ArgumentNullException();
            if (intraDayTrades == null) throw new ArgumentNullException();
            if (intraDayTrades.Count() == 0) throw new Exception("No trades  to process");
            if (!Directory.Exists(Path.GetDirectoryName(csvFilePath))) throw new DirectoryNotFoundException();
            
            try
            {
                if (!File.Exists(csvFilePath))
                {
                    File.Create(csvFilePath).Close();
                    Log.Info(String.Format("{0} created", csvFilePath));
                }

                var intraDayReport = new StringBuilder();
                if (headerNames != null)
                {
                    Log.Info("In CsvFileWriter build headers");
                    //Build Header Row if available
                    foreach (var header in headerNames)
                        intraDayReport.AppendFormat("{0},", header);
                    intraDayReport.Append(Environment.NewLine);
                }

                Log.Info("In CsvFileWriter begining to build Power Intraday trades string to report");

                foreach (var trade in intraDayTrades)
                    intraDayReport.AppendFormat("{0},{1}{2}", trade.TradeTime, trade.TradePosition, Environment.NewLine);


                File.AppendAllText(csvFilePath, intraDayReport.ToString());
                Log.Info("Power Intraday Report written successfully");
            }
            catch (Exception ex)
            {
                Log.Error(ex.StackTrace, ex);
            }
        }
    }
}