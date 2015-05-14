Code Description
Assumptions: 
1) This is a simple api approach to GetTrades rather than the Async approach, ideally I would have liked to provide both options just like Power.dll 
2) Currently this library is used within Windows Service dign the sole function of trade extraction so don't see the need to make it multithreaded
In scenarios where this library will be used with WPF or a WCF application, async api call would be a better approach.


Caveats:

1) Unable to run integration test for ShouldAggregatePowerTradeVolumes in TradeVolumesToPositionsAggregatorTests as the PowerTrade class does not provide a provision 
to  set Volume



Code Solution Folder:
1) Petroineos.Intraday.Lib
	This is the main code which calls the external assembly PowerTrade (PowerTradeAPI) to extract trades. 
	The CSVWriter code constructs the file name as per requirements and will write out to a CSV file
	The main reportbuilder (PowerIntradayReportBuilder) is provided which extracts trades and builds csvfilewriter 
	The config class (ConfiguratioProvider)  provides the ability to read configuration from the app.config file
	A DTO class (TradeVolumesToPositionsAggregator) converts data  model from external data model to internal data model

2) PetroineosIntradayTradesExtractorService
   This is the main reportign service which uses Unity for DI and configuration. This will generate csv files at regular intervals

3) Petroineos.Intraday.IntegrationTests
   Provides interation test cases for the tool

4) Petroineos.Intraday.Test
	Provides unit test cases

5)  I use log4Net to log messages and the configuration can be a bit difficult. Please change the path for the log file with correct permissions and this shoudl work

Output
I have attached a sample log output and CSv report in teh output subfolder of the project

Steps to Configure and Install

Configuration Steps
	1) Configure App.Config file in the folders below based on reuirements
		PetroineosIntradayTradesExtractorService (Windows Service) 
		Petroineos.Intraday.Test (For unit Test)
		Petroineos.Intraday.IntegrationTests (For Integration Test)
	
	2) Ensure that the CsvFilePath is cirrectly set up with write permissions 
		<add key="CsvFilePath" value="\\LENOVO-PC\PowerIntradayReports"/>

	3) The Log file Path with correct permissions need to be set up in the log4net appender section. 
		<file value="\\LENOVO-PC\PowerLog\PowerPosition.log"/> 
	4) Can configure Log file size as per requirements on the entry as below in the app.config file
		<maximumFileSize value="5MB"/>

Install Steps
	1) Run Command as Admin
	2) cd C:\Windows\Microsoft.NET\Framework\v4.0.30319
	3) To install
		INSTALL /i service.exe path eg:
		installutil /i "D:\Workarea\Petroineos\Petroineos.Intraday.ReportingApp\Source\PetroineosIntradayTradesExtractorService\bin\Release\PetroineosIntradayTradesExtractorService.exe"
	3) To uninstall
		INSTALL /u service.exe path eg:
		installutil /u "D:\Workarea\Petroineos\Petroineos.Intraday.ReportingApp\Source\PetroineosIntradayTradesExtractorService\bin\Release\PetroineosIntradayTradesExtractorService.exe"
	4) Start the windows service
