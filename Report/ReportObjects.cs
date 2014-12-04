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

            CloudTable orderTable = tableClient.GetTableReference("order");

            TableQuery<Order> orderQuery = new TableQuery<Order>();
            //TableQuery<Order> query = new TableQuery<Order>().Sum(qu => qu.price);
                //.Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, country))
            IEnumerable<Order> orderQueryResult = orderTable.ExecuteQuery(orderQuery);
            List<Order> orderList = orderQueryResult.ToList();

            CloudTable customerTable = tableClient.GetTableReference("customer");

            TableQuery<Customer> customerQuery = new TableQuery<Customer>()
            .Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, country));

            IEnumerable<Customer> customerQueryResult = customerTable.ExecuteQuery(customerQuery);
            List<Customer> customerList = customerQueryResult.ToList();
            
            var query = from order in orderList
                        join customer in customerList on order.PartitionKey equals customer.RowKey
                        select order;

            double ou = query.Sum(qu => qu.price);
            
            return ou;
        }

        public static double meanOrdersFromCountry(string country)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse((string)Settings.Default["StorageConnectionString"]);

            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            CloudTable orderTable = tableClient.GetTableReference("order");

            TableQuery<Order> orderQuery = new TableQuery<Order>();
            //TableQuery<Order> query = new TableQuery<Order>().Sum(qu => qu.price);
            //.Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, country))
            IEnumerable<Order> orderQueryResult = orderTable.ExecuteQuery(orderQuery);
            List<Order> orderList = orderQueryResult.ToList();

            CloudTable customerTable = tableClient.GetTableReference("customer");

            TableQuery<Customer> customerQuery = new TableQuery<Customer>()
            .Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, country));

            IEnumerable<Customer> customerQueryResult = customerTable.ExecuteQuery(customerQuery);
            List<Customer> customerList = customerQueryResult.ToList();

            var query = from order in orderList
                        join customer in customerList on order.PartitionKey equals customer.RowKey
                        select order;

            double ou = query.Average(qu => qu.price);

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
