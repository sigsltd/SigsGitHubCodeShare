using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Petroineos.Intraday.Lib;
using Petroineos.Intraday.Lib.Implementation;
using Petroineos.Intraday.Lib.Model;
using PetroineosIntradayTradesExtractorService;

namespace Petroineos.Intraday.IntegrationTests
{
    [TestClass]
    public class ConfigurationTests
    {
        private static IUnityContainer _container;

        [TestMethod]
        public void ShouldResolveIConfigurationProvider()
        {
            //Assemble
            //Act
            var result = _container.Resolve<IConfigurationProvider>();
            //Assert
            Assert.IsInstanceOfType(result, typeof (ConfigurationProvider));
        }

        [TestMethod]
        public void ShouldResolveIConfigurationProviderAsSingleton()
        {
            //Assemble
            //Act
            var instance1 = _container.Resolve<IConfigurationProvider>();
            var instance2 = _container.Resolve<IConfigurationProvider>();
            //Assert
            Assert.AreSame(instance1, instance2,
                "IConfigurationProvider should be resolved as Singleton. Both results should point to same service instance.");
        }

        [TestMethod]
        public void ShouldResolveICsvFileWriter()
        {
            //Assemble
            //Act
            var result = _container.Resolve<ICsvFileWriter>();
            //Assert
            Assert.IsInstanceOfType(result, typeof (CsvFileWriter));
        }

        [TestMethod]
        public void ShouldResolveIDateTimeFormatter()
        {
            //Assemble
            //Act
            var result = _container.Resolve<IDateTimeFormatter>();
            //Assert
            Assert.IsInstanceOfType(result, typeof (DateTimeFormatter));
        }

        [TestMethod]
        public void ShouldResolveIPowerIntraDayReportBuilder()
        {
            //Assemble
            //Act
            var result = _container.Resolve<IPowerIntraDayReportBuilder>();
            //Assert
            Assert.IsInstanceOfType(result, typeof (PowerIntraDayReportBuilder));
        }

        [TestMethod]
        public void ShouldResolveIPowerIntraDayReportFileNameBuilder()
        {
            //Assemble
            //Act
            var result = _container.Resolve<IPowerIntraDayReportFileNameBuilder>();
            //Assert
            Assert.IsInstanceOfType(result, typeof (PowerIntraDayReportFileNameBuilder));
        }

        [TestMethod]
        public void ShouldResolveIPowerTradeAPIForIntraDayTradePosition()
        {
            //Assemble
            //Act
            var result = _container.Resolve<IPowerTradeAPI<IntraDayTradePosition>>();
            //Assert
            Assert.IsInstanceOfType(result, typeof (PowerTradeAPI));
        }

        [TestMethod]
        public void ShouldResolveITradeVolumesToPositionsAggregator()
        {
            //Assemble
            //Act
            var result = _container.Resolve<ITradeVolumesToPositionsAggregator>();
            //Assert
            Assert.IsInstanceOfType(result, typeof (TradeVolumesToPositionsAggregator));
        }

        #region Setup

        [ClassInitialize]
        public static void MyClassInitialize(TestContext testContext)
        {
            log4net.Config.XmlConfigurator.Configure();
            _container = new UnityContainer();
            _container.AddNewExtension<IntraDayReportingConfiguration>();
        }

        [ClassCleanup]
        public static void MyClassCleanup()
        {
            _container.Dispose();
        }

        #endregion
    }
}