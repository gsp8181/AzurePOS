using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Table;
using Report.Properties;

namespace Report
{
    static class ReportObjects
    {
        public static double totalOrdersFromCountry(string country)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse((string)Settings.Default["StorageConnectionString"]);

            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            CloudTable table = tableClient.GetTableReference("order");

            TableQuery<Order> query = new TableQuery<Order>();
            //TableQuery<Order> query = new TableQuery<Order>().Sum(qu => qu.price);
                //.Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, country))
            double ou = table.ExecuteQuery(query).Sum(qu => qu.price);

            return ou;
        }

        public static double meanOrdersFromCountry(string country)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse((string)Settings.Default["StorageConnectionString"]);

            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            CloudTable table = tableClient.GetTableReference("order");

            TableQuery<Order> query = new TableQuery<Order>();

            IEnumerable<Order> oex = table.ExecuteQuery(query);

            double ou = table.ExecuteQuery(query).Average(qu => qu.price);

            return ou;
        }


        public static List<Order> OrdersFromLastSevenDays(string country)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse((string)Settings.Default["StorageConnectionString"]);

            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            CloudTable table = tableClient.GetTableReference("order");

            TableQuery<Order> query = new TableQuery<Order>().Where(TableQuery.GenerateFilterConditionForDate("dateTime", QueryComparisons.GreaterThanOrEqual, DateTime.Today.AddDays(-7)));

            IEnumerable<Order> oex = table.ExecuteQuery(query);

            //double ou = table.ExecuteQuery(query);

            List<Order> completedList = oex.ToList<Order>();
            return completedList;
        }
    }
}
