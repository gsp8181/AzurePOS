using AzurePOS.Customer;
using AzurePOS.Properties;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzurePOS.Offline
{
    static class Resend
    {
        public static void ResendMessage()
        {
            while((bool)Settings.Default["Stored"])
            {
                Console.WriteLine("Attempting to resend saved message");

                CloudStorageAccount storageAccount = CloudStorageAccount.Parse((string)Settings.Default["StorageConnectionString"]);

                CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();

                CloudQueue queue = queueClient.GetQueueReference((string)Settings.Default["StoredQueue"]);

                string message = (string)Settings.Default["StoredMessage"];

                CloudQueueMessage cm = new CloudQueueMessage(message);

                try
                {
                    queue.CreateIfNotExists();
                    queue.AddMessage(cm);
                    Settings.Default["Stored"] = false;
                    Settings.Default["StoredQueue"] = "";
                    Settings.Default["StoredMessage"] = "";
                    Settings.Default.Save();
                    Console.WriteLine("Successfully sent saved message");
                }
                catch (StorageException) { }
            }
        }
        public static void ResendMessage(string o, string type)
        {
            Console.WriteLine("Internet connection is not available, message queued for sending when internet connectivity resumes");
            Settings.Default["Stored"] = true;
            Settings.Default["StoredQueue"] = type;
            Settings.Default["StoredMessage"] = o;
            Settings.Default.Save();
            ResendMessage();
        }
    }
}
