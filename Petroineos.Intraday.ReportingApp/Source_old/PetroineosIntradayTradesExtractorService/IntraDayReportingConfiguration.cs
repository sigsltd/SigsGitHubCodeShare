using Microsoft.Practices.Unity;
using Petroineos.Intraday.Lib;
using Petroineos.Intraday.Lib.Implementation;
using Petroineos.Intraday.Lib.Model;

namespace PetroineosIntradayTradesExtractorService
{
    public class IntraDayReportingConfiguration : UnityContainerExtension
    {
        protected override void Initialize()
        {
            Container.RegisterType<IConfigurationProvider, ConfigurationProvider>(
                new ContainerControlledLifetimeManager());
            Container.RegisterType<ICsvFileWriter, CsvFileWriter>();
            Container.RegisterType<IDateTimeFormatter, DateTimeFormatter>();
            Container.RegisterType<IPowerIntraDayReportBuilder, PowerIntraDayReportBuilder>();
            Container.RegisterType<IPowerIntraDayReportFileNameBuilder, PowerIntraDayReportFileNameBuilder>();
            Container.RegisterType<IPowerTradeAPI<IntraDayTradePosition>, PowerTradeAPI>();
            Container.RegisterType<ITradeVolumesToPositionsAggregator, TradeVolumesToPositionsAggregator>();
        }
    }
}