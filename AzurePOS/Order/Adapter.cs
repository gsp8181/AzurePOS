using AzurePOS.Properties;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzurePOS.Order
{
    static class Adapter
    {
        public static string GetList()
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse((string)Settings.Default["StorageConnectionString"]);

            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            CloudTable table = tableClient.GetTableReference("order");

            if (table.Exists())
            {
                TableQuery<Order> query = new TableQuery<Order>();
                string output = "Order Table";
                IEnumerable<Order> executedQuery = table.ExecuteQuery(query).OrderBy(k => int.Parse(k.RowKey));
                foreach(Order c in executedQuery)
                {
                   output += ("\n" + c.RowKey + ": customer " + c.PartitionKey + " " + c.sku + " " + c.dateTime.ToString("dd/MM/yyyy HH:mm") + " " + c.price.ToString("C"));
                }
                return output;
            } else
            {
                return ("No customer records found");
            }
        }

        public static string Register(string customerId, string sku, DateTime dateTime, double price)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse((string)Settings.Default["StorageConnectionString"]);

            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();

            CloudQueue queue = queueClient.GetQueueReference("order");

            queue.CreateIfNotExists();

            OrderObject OrderObj = new OrderObject(customerId,sku,dateTime, price);

            string order = Serialiser.Serialiser.serialise(OrderObj);

            CloudQueueMessage cm = new CloudQueueMessage(order);

            queue.AddMessage(cm);
            return "Added " + order;
        }
    }
}
