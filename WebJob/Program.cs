using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebJob.Properties;


namespace WebJob
{
    class Program
    {
        static void Main(string[] args)
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

            TableQuery<Customer> customerQuery = new TableQuery<Customer>();
            //.Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, country));

            IEnumerable<Customer> customerQueryResult = customerTable.ExecuteQuery(customerQuery);
            List<Customer> customerList = customerQueryResult.ToList();

            var query = from order in orderList
                        join customer in customerList on order.PartitionKey equals customer.RowKey
                        select new {
                            ID = order.RowKey,
                            CustomerID = customer.RowKey,
                            Country = customer.PartitionKey,
                            SKU = order.sku,
                            dateTime = order.dateTime,
                            price = order.price
                        };

            //double ou = query.Sum(qu => qu.price);


            // Create the table if it doesn't exist.
            CloudTable ocTable = tableClient.GetTableReference("ordercountry");
            ocTable.CreateIfNotExists();


            foreach(var obj in query)
            {
                OrderCountry oc = new OrderCountry(obj.ID, obj.Country) {
                    customerId = obj.CustomerID,
                    sku = obj.SKU,
                    dateTime = obj.dateTime,
                    price = obj.price };
                TableOperation insertOperation = TableOperation.Insert(oc);
                ocTable.Execute(insertOperation);
            }
        }
    }
}
