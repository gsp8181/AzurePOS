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
using Microsoft.WindowsAzure.Storage.Table;
using AzurePOS.Offline;

namespace AzurePOS.Customer
{
    static class Adapter
    {
        public static string GetList()
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse((string)Settings.Default["StorageConnectionString"]);

            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            CloudTable table = tableClient.GetTableReference("customer");

            if (table.Exists())
            {
                TableQuery<Customer> query = new TableQuery<Customer>();
                string output = "Customer Table";
                IEnumerable<Customer> executedQuery = table.ExecuteQuery(query).OrderBy(k => int.Parse(k.RowKey));
                foreach (Customer c in executedQuery)
                {
                    output += ("\n" + c.RowKey + ": " + c.name + " (" + c.PartitionKey + ")");
                }
                return output;
            }
            else
            {
                return ("No order records found");
            }
        }

        public static string Register(string name, string country)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse((string)Settings.Default["StorageConnectionString"]);

            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();

            CloudQueue queue = queueClient.GetQueueReference("customer");

            CustomerObject CustomerObj = new CustomerObject(name, country);

            string customer = Serialiser.Serialiser.serialise(CustomerObj);

            CloudQueueMessage cm = new CloudQueueMessage(customer);

            try
            {
                queue.CreateIfNotExists();
                queue.AddMessage(cm);
            }
            catch (StorageException)
            {
                Resend.ResendMessage(customer, "customer");
            }
            return "Added " + customer;
        }

        public static string Register(string name)
        {
            string country = Location.Location.Country;
            return Register(name, country);
        }
    }
}
