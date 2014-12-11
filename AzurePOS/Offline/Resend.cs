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
    /// <summary>
    /// Handles offline sending of queue messages
    /// </summary>
    static class Resend
    {
        /// <summary>
        /// Attempts to resend the message in the queue for sending
        /// </summary>
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
        /// <summary>
        /// Takes a message to be added to the queue for resend
        /// </summary>
        /// <param name="o">The message to be stored</param>
        /// <param name="type">The type of message (customer or order)</param>
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
