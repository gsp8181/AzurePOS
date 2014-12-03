using AzurePOS.Properties;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
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
            return "order list";
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
