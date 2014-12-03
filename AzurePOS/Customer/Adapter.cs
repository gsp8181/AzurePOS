using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Queue;
using Microsoft.WindowsAzure;
using AzurePOS.Properties;

namespace AzurePOS.Customer
{
    static class Adapter
    {
        public static string GetList()
        {
            return "customer list";
        }

        public static string Register(string name, string country)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse((string)Settings.Default["StorageConnectionString"]);

            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();

            CloudQueue queue = queueClient.GetQueueReference("customer");

            queue.CreateIfNotExists();

            CustomerObject CustomerObj = new CustomerObject(name, country);

            string customer = Serialiser.Serialiser.serialise(CustomerObj);

            CloudQueueMessage cm = new CloudQueueMessage(customer);

            queue.AddMessage(cm);
            return "Added " + customer;
        }

        public static string Register(string name)
        {
            string country = Location.Location.Country;
            return Register(name, country);
        }
    }
}
