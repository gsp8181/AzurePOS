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
    /// <summary>
    /// Contains a series of methods that produce string formatted reports for specified country codes
    /// </summary>
    static class ReportObjects
    {
        /// <summary>
        /// Retrieves the total amount of orders from a specified country.
        /// Uses a deprecated JOIN method to demonstrate understanding
        /// </summary>
        /// <param name="country">The two letter country code</param>
        /// <returns>The resulting value</returns>
        public static double TotalOrdersFromCountry(string country)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse((string)Settings.Default["StorageConnectionString"]);

            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            CloudTable orderTable = tableClient.GetTableReference("order");

            TableQuery<Order> orderQuery = new TableQuery<Order>();

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

        /// <summary>
        /// Retrieves the mean amount of orders for a specified country. 
        /// Uses the new OrderCountry table for more efficient processing
        /// </summary>
        /// <param name="country">The two letter country code</param>
        /// <returns>The resulting value</returns>
        public static double MeanOrdersFromCountry(string country)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse((string)Settings.Default["StorageConnectionString"]);

            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            CloudTable orderCountryTable = tableClient.GetTableReference("ordercountry");

            TableQuery<Order> orderQuery = new TableQuery<Order>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, country));
            var orderQueryResult = orderCountryTable.ExecuteQuery(orderQuery);


            if (orderQueryResult.Count() > 0)
            {
                double ou = orderQueryResult.Average(qu => qu.price);

                return ou;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// Retrieves all of the orders made from the last seven days from a given country
        /// </summary>
        /// <param name="country">The two letter country code</param>
        /// <returns>A list of orders made in the past seven days from the given country</returns>
        public static List<Order> OrdersFromLastSevenDays(string country)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse((string)Settings.Default["StorageConnectionString"]);

            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            CloudTable orderCountryTable = tableClient.GetTableReference("ordercountry");

            TableQuery<Order> orderQuery = new TableQuery<Order>()
                .Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, country))
                .Where(TableQuery.GenerateFilterConditionForDate("dateTime", QueryComparisons.GreaterThanOrEqual, DateTime.Today.AddDays(-7)));


            var ou = orderCountryTable.ExecuteQuery(orderQuery);

            List<Order> completedList = ou.ToList<Order>();
            return completedList;
        }
    }
}
