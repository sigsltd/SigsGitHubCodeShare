using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.ServiceProcess;
using System.Threading.Tasks;

namespace PetroineosIntradayTradesExtractorService
{
    [RunInstaller(true)]
    public partial class TradePositionExtractorServiceInstaller : System.Configuration.Install.Installer
    {
        ServiceProcessInstaller _serviceProcessInstaller = new ServiceProcessInstaller();
        ServiceInstaller _serviceInstaller = new ServiceInstaller();
        public TradePositionExtractorServiceInstaller()
        {
            InitializeComponent();
            
            _serviceProcessInstaller.Account = ServiceAccount.LocalSystem;
            _serviceProcessInstaller.Username = null;
            _serviceProcessInstaller.Password = null;
            _serviceInstaller.DisplayName = "PertroinoesIntraDayTradeExtractorService";
            _serviceInstaller.StartType = ServiceStartMode.Automatic;
            _serviceInstaller.ServiceName = "TradePositionExtractorService";

            this.Installers.AddRange(new Installer[] {
            _serviceProcessInstaller,_serviceInstaller});
            
        }
    }
}
