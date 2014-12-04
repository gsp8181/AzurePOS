AzurePOS
========
##To Build

1. Download NuGet.exe from http://nuget.org/nuget.exe and place in your PATH or in the root folder
2. Run `nuget.exe restore` in the root folder
3. Run `msbuild /t:Release` to build all the applications (found in Visual Studio Tools->Visual Studio Command Prompt)

###AzurePOS.exe
Command line applications for adding orders and customers to the Azure Queue and retrieving the list of them from tables storage.
Help included in application

Usage:`AzurePOS <command>` eg `AzurePOS help`

###GenRandom.exe
Makes 1000 customers and 10000 orders for those customers and adds them to the Azure Queue.

Usage:`GenRandom`

###Report.exe
Generates a report for the orders of a specific country

Usage:`Report <Country Code>` eg `Report GB`

###NoSQLWorker.exe
Azure Web Job that takes customers and orders from the queue and adds them to Tables Storage.

Can be deployed as part of an Azure Website or run locally

Usage:`NoSQLWorker`

###SQLWorker.exe
Azure Web Job that takes customers and orders from the queue and adds them to the SQL database.

Can be deployed as part of an Azure Website or run locally

Usage:`SQLWorker`
