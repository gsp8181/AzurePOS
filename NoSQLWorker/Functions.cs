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
using System.Net;


namespace NoSQLWorker
{
    /// <summary>
    /// Holds functions for async recieving of messages from the azure queues to be processed and sent to the relavent tables
    /// </summary>
    public class Functions
    {

        /// <summary>
        /// This function will get triggered/executed when a new message is written on an Azure Queue called customer. 
        /// Processes the message and adds the new customer to the customer table
        /// </summary>
        /// <param name="message">The JSON string of the new customer</param>
        /// <param name="log">The log for writing information messages</param>
        public static void ProcessCustomerQueueMessage([QueueTrigger("customer")] string message, TextWriter log)
        {
            CustomerObject co = Serialiser.deserialiseC(message);


            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConfigurationManager.ConnectionStrings["AzureWebJobsStorage"].ConnectionString);
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
            CloudTable table = tableClient.GetTableReference("customer");
            bool createdTable = table.CreateIfNotExists();
            Customer ce = new Customer(genId.generate("customer").ToString(), co.country);
            ce.name = co.name;

            TableOperation insertOperation = TableOperation.Insert(ce);

            try
            {
                table.Execute(insertOperation);
            }
            catch (WebException)
            {
                table.Execute(insertOperation);
            }

            //log.WriteLine(message);
        }

        /// <summary>
        /// This function will get triggered/executed when a new message is written on an Azure Queue called order. 
        /// Processes the message and adds the new order to the orders table.
        /// Also adds the order to a special link table called OrderCountry for reporting purposes
        /// </summary>
        /// <param name="message">The JSON string of the new order</param>
        /// <param name="log">The log for writing information messages</param>
        public static void ProcessOrderQueueMessage([QueueTrigger("order")] string message, TextWriter log)
        {
            OrderObject oo = Serialiser.deserialiseO(message);

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConfigurationManager.ConnectionStrings["AzureWebJobsStorage"].ConnectionString);
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
            CloudTable table = tableClient.GetTableReference("order");
            table.CreateIfNotExists();
            string id = genId.generate("order").ToString();
            Order oe = new Order(id, oo.customerId.ToString());
            oe.sku = oo.sku;
            oe.dateTime = oo.dateTime;
            oe.price = oo.price;

            TableOperation insertOperation = TableOperation.Insert(oe);

            table.Execute(insertOperation);

            CloudTable ocTable = tableClient.GetTableReference("ordercountry");
            ocTable.CreateIfNotExists();

            OrderCountry oc = new OrderCountry(id, "GB") { customerId = oo.customerId.ToString(), sku = oo.sku, dateTime = oo.dateTime, price = oo.price }; //TODO: get country

            TableOperation ocInsertOperation = TableOperation.Insert(oc);

            ocTable.Execute(ocInsertOperation);

            //log.WriteLine(message);
        }
    }
}
