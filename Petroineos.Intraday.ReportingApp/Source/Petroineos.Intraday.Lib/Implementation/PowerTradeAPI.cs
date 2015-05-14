using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using log4net;
using Petroineos.Intraday.Lib.Model;
using Services;

namespace Petroineos.Intraday.Lib.Implementation
{
    public class PowerTradeAPI : IPowerTradeAPI<IntraDayTradePosition>
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly IConfigurationProvider _configurationProvider;
        private readonly PowerService _powerService;
        private readonly ITradeVolumesToPositionsAggregator _tradeVolumesToPositionsAggregator;

        public PowerTradeAPI(PowerService powerService,
            ITradeVolumesToPositionsAggregator tradeVolumesToPositionsAggregator,
            IConfigurationProvider configurationProvider)
        {
            if (powerService == null) throw new ArgumentNullException("powerService");
            if (tradeVolumesToPositionsAggregator == null)
                throw new ArgumentNullException("tradeVolumesToPositionsAggregator");
            if (configurationProvider == null) throw new ArgumentNullException("configurationProvider");
            _powerService = powerService;
            _tradeVolumesToPositionsAggregator = tradeVolumesToPositionsAggregator;
            _configurationProvider = configurationProvider;
        }

        public IEnumerable<IntraDayTradePosition> GetIntradayTrades(DateTime date)
        {
            if (_powerService == null) throw new NullReferenceException("Powerservice is null");
            if (date > DateTime.Now) throw new Exception("Invalid Report DateTime");

            IEnumerable<IntraDayTradePosition> intradayPositions = new List<IntraDayTradePosition>();

            Log.Info(String.Format("In GetIntraDayTrades for {0}", date));

            for (var attempts = 0; attempts < _configurationProvider.AttempsToGetTrades; attempts++)
            {
                var result = TryGetIntraDayTrades(date);
                if (result.Success)
                {
                    intradayPositions = result.Result;
                    Log.Info("We have successfully retrieved IntraDayTrades.");
                    break;
                }

                Log.Warn(String.Format(
                    "Failed to retreive IntraDayTrades for {0}. Sleeping for {1} secs and retrying.", date,
                    _configurationProvider.IntraDayTradesRetryIntervalInSeconds));

                Thread.Sleep(_configurationProvider.IntraDayTradesRetryIntervalInSeconds);
            }

            return intradayPositions;
        }

        private OperationResult<IEnumerable<IntraDayTradePosition>> TryGetIntraDayTrades(DateTime date)
        {
            IEnumerable<IntraDayTradePosition> intradayPositions = null;
            var success = false;

            try
            {
                var intraDayTrades = _powerService.GetTrades(date).ToList();
                if (intraDayTrades.Count() > 0)
                {
                    intradayPositions = _tradeVolumesToPositionsAggregator.Aggregate(intraDayTrades);
                    Log.Info("GetIntradayTrades Succeeded");
                    success = true;
                }
            }
            catch (PowerServiceException pex)
            {
                Log.Error(pex.StackTrace, pex);
            }
            catch (Exception ex)
            {
                Log.Error(ex.StackTrace, ex);
            }
            return new OperationResult<IEnumerable<IntraDayTradePosition>>(intradayPositions, success);
        }
    }
}