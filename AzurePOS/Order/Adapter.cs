using AzurePOS.Offline;
using AzurePOS.Properties;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AzurePOS.Order
{
    /// <summary>
    /// Has methods for interacting with the Order azure queue and table
    /// </summary>
    static class Adapter
    {

        /// <summary>
        /// Gets a formatted list of the orders held in the Azure table
        /// </summary>
        /// <returns></returns>
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
                foreach (Order c in executedQuery)
                {
                    output += ("\n" + c.RowKey + ": customer " + c.PartitionKey + " " + c.sku + " " + c.dateTime.ToString("dd/MM/yyyy HH:mm") + " " + c.price.ToString("C"));
                }
                return output;
            }
            else
            {
                return ("No order records found");
            }
        }

        /// <summary>
        /// Sends a new order to the azure queue for procesing
        /// </summary>
        /// <param name="customerId">The associated ID of the customer</param>
        /// <param name="sku">The SKU of the item</param>
        /// <param name="dateTime">The date and time of the order</param>
        /// <param name="price">The price of the order</param>
        /// <returns></returns>
        public static string Register(string customerId, string sku, DateTime dateTime, double price)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse((string)Settings.Default["StorageConnectionString"]);

            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();

            CloudQueue queue = queueClient.GetQueueReference("order");

            OrderObject OrderObj = new OrderObject(customerId, sku, dateTime, price);

            string order = Serialiser.Serialiser.serialise(OrderObj);

            CloudQueueMessage cm = new CloudQueueMessage(order);

            try
            {
                queue.CreateIfNotExists();
                queue.AddMessage(cm);
            }
            catch (StorageException)
            {
                Resend.ResendMessage(order, "order");
            }

            string orderStr = order.ToString();
            orderStr = Regex.Replace(orderStr, @"\\/Date\(\d+\)\\/", dateTime.ToString("dd/MM/yyyy HH:mm"));
            return "Added " + orderStr;
        }
    }
}
