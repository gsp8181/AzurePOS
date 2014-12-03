using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;

namespace SQLWorker
{
    public class Functions
    {
        // This function will get triggered/executed when a new message is written 
        // on an Azure Queue called queue.
        public static void ProcessCustomerQueueMessage([QueueTrigger("customer")] string message, TextWriter log)
        {
            CustomerObject c = Serialiser.deserialiseC(message);
            log.WriteLine(message);
        }

        // This function will get triggered/executed when a new message is written 
        // on an Azure Queue called queue.
        public static void ProcessOrderQueueMessage([QueueTrigger("order")] string message, TextWriter log)
        {
            OrderObject o = Serialiser.deserialiseO(message);
            log.WriteLine(message);
        }
    }
}
