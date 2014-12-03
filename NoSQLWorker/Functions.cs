using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System.Configuration;


namespace NoSQLWorker
{
    public class Functions
    {
        // This function will get triggered/executed when a new message is written 
        // on an Azure Queue called queue.
        public static void ProcessCustomerQueueMessage([QueueTrigger("customer")] string message, TextWriter log)
        {
            CustomerObject co = Serialiser.deserialiseC(message);


            //CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("AzureWebJobsStorage"));
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConfigurationManager.ConnectionStrings["AzureWebJobsStorage"].ConnectionString);
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
            CloudTable table = tableClient.GetTableReference("customer");
            bool createdTable = table.CreateIfNotExists();
            Customer ce = new Customer(Guid.NewGuid().ToString(), co.country);
            ce.name = co.name;

            TableOperation insertOperation = TableOperation.Insert(ce);

            table.Execute(insertOperation);

            log.WriteLine(message);
        }

        // This function will get triggered/executed when a new message is written 
        // on an Azure Queue called queue.
        public static void ProcessOrderQueueMessage([QueueTrigger("order")] string message, TextWriter log)
        {
            OrderObject oo = Serialiser.deserialiseO(message);

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConfigurationManager.ConnectionStrings["AzureWebJobsStorage"].ConnectionString);
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
            CloudTable table = tableClient.GetTableReference("order");
            bool createdTable = table.CreateIfNotExists();
            Order oe = new Order(Guid.NewGuid().ToString(), oo.customerId.ToString());
            oe.sku = oo.sku;
            oe.dateTime = oo.dateTime;
            oe.price = oo.price;
            //ce.name = oe.name;

            TableOperation insertOperation = TableOperation.Insert(oe);

            table.Execute(insertOperation);

            log.WriteLine(message);
        }
    }
}
