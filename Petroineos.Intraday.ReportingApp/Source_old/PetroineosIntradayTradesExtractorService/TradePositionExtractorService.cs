using System;
using System.Reflection;
using System.ServiceProcess;
using System.Timers;
using log4net;
using Microsoft.Practices.Unity;
using Petroineos.Intraday.Lib;

namespace PetroineosIntradayTradesExtractorService
{
    public partial class TradePositionExtractorService : ServiceBase
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private IConfigurationProvider _configurationProvider;
        private IPowerIntraDayReportBuilder _reportBuilder;
        private readonly IUnityContainer _container;
        private readonly Timer _timer = new Timer();

        public TradePositionExtractorService()
        {
            InitializeComponent();
            _container = ConfigureContainer();
        }

        protected override void OnStart(string[] args)
        {
            log4net.Config.XmlConfigurator.Configure();
            _configurationProvider = _container.Resolve<IConfigurationProvider>();
            _reportBuilder = _container.Resolve<IPowerIntraDayReportBuilder>();


            Log.Info("Starting  TradePositionExtractorService");
            //handle Elapsed event
            _timer.Elapsed += OnElapsedTime;
            Log.Info("Set Timer");
            _timer.Interval =
                TimeSpan.FromMinutes(_configurationProvider.PollFrequencyInMinutes).TotalMilliseconds;
            _timer.Start();
            _timer.Enabled = true;
            Log.Info("ExtractIntradayTradePositionReport On Service Start");
            ExtractIntradayTradePositionReport();
        }

        protected override void OnStop()
        {
            if (_container != null)
            {
                _container.Dispose();
            }
            _configurationProvider = null;
            _reportBuilder = null;
        }

        private void OnElapsedTime(object source, ElapsedEventArgs e)
        {
            ExtractIntradayTradePositionReport();
            
        }

        private void ExtractIntradayTradePositionReport()
        {
            Log.Info("Timer elapsed in  ExtractIntradayTradePositionReport");
            _reportBuilder.BuildIntradayPowerTradePositionReport(DateTime.Now);
        }

        private IUnityContainer ConfigureContainer()
        {
            var container = new UnityContainer();
            container.AddNewExtension<IntraDayReportingConfiguration>();
            return container;
        }
    }
}