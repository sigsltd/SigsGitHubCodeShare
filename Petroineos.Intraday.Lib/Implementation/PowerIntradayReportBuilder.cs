using System;
using System.Collections.Generic;
using System.Reflection;
using log4net;
using Petroineos.Intraday.Lib.Model;

namespace Petroineos.Intraday.Lib.Implementation
{
    public class PowerIntraDayReportBuilder : IPowerIntraDayReportBuilder
    {
        private const string IntraDayReportFileNamePrefix = "PowerPosition";
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly ICsvFileWriter _csvWriter;
        private readonly IPowerIntraDayReportFileNameBuilder _fileNameBuilder;
        private readonly IPowerTradeAPI<IntraDayTradePosition> _serviceAPI;

        public PowerIntraDayReportBuilder(IPowerTradeAPI<IntraDayTradePosition> serviceAPI, ICsvFileWriter csvWriter,
            IPowerIntraDayReportFileNameBuilder fileNameBuilder)
        {
            if (serviceAPI == null) throw new ArgumentNullException("serviceAPI");
            if (csvWriter == null) throw new ArgumentNullException("csvWriter");
            if (fileNameBuilder == null) throw new ArgumentNullException("fileNameBuilder");
            _serviceAPI = serviceAPI;
            _csvWriter = csvWriter;
            _fileNameBuilder = fileNameBuilder;
        }

        public OperationResult<PowerIntraDayReport> BuildIntradayPowerTradePositionReport(DateTime asOfDate)
        {
            OperationResult<PowerIntraDayReport> reportResult;

            try
            {
                Log.Info("Begin BuildIntradayPowerTradePositionReport");
                var tradePositions = _serviceAPI.GetIntradayTrades(asOfDate);

                Log.Info("Construct CSV filename");
                var filename = _fileNameBuilder.GetFilename(IntraDayReportFileNamePrefix);

                Log.Info("Write  to csv");
                _csvWriter.Write(filename, tradePositions, CreateHeaderNames());

                Log.Info("End of BuildIntradayPowerTradePositionReport");

                reportResult = new OperationResult<PowerIntraDayReport>(new PowerIntraDayReport
                {
                    ReportFilename = filename
                }, true);
            }
            catch (Exception ex)
            {
                Log.Error(ex.StackTrace, ex);
                reportResult = new OperationResult<PowerIntraDayReport>(new PowerIntraDayReport(), false);
            }

            return reportResult;
        }

        private string[] CreateHeaderNames()
        {
            return new List<string> {"Local Time", "Volume"}.ToArray();
        }
    }
}