using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace SQLWorker
{
    /// <summary>
    /// Holds functions for async recieving of messages from the azure queues to be processed and sent to the relavent SQL tables
    /// </summary>
    public class Functions
    {
        /// <summary>
        /// This function will get triggered/executed when a new message is written on an Azure Queue called order. 
        /// Processes the message and adds the new customer to the order SQL table
        /// </summary>
        /// <param name="message">The JSON string of the new order</param>
        /// <param name="log">The log for writing information messages</param>
        public static void ProcessOrderQueueMessage([QueueTrigger("order")] string message, TextWriter log)
        {
            SqlConnectionStringBuilder csBuilder;
            csBuilder = new SqlConnectionStringBuilder(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

            SqlConnection conn = new SqlConnection(csBuilder.ToString());
            conn.Open();

            OrderObject o = Serialiser.deserialiseO(message);

            SqlParameter CustomerId = new SqlParameter("@CustomerID", SqlDbType.NVarChar, 50) { Value = o.customerId };
            SqlParameter SKU = new SqlParameter("@SKU", SqlDbType.NVarChar, 25) { Value = o.sku };
            SqlParameter OrderDateTime = new SqlParameter("@OrderDateTime", SqlDbType.DateTime2, 7) { Value = o.dateTime };
            SqlParameter Price = new SqlParameter("@Price", SqlDbType.Money) { Value = o.price };

            SqlCommand insertCommand = new SqlCommand("INSERT INTO OrderT (CustomerID, SKU, OrderDateTime, Price) VALUES (@CustomerID, @SKU, @OrderDateTime, @Price)", conn);
            insertCommand.Parameters.Add(CustomerId);
            insertCommand.Parameters.Add(SKU);
            insertCommand.Parameters.Add(OrderDateTime);
            insertCommand.Parameters.Add(Price);

            insertCommand.ExecuteNonQuery();

            conn.Close();
        }

        /// <summary>
        /// This function will get triggered/executed when a new message is written on an Azure Queue called customer. 
        /// Processes the message and adds the new customer to the customer SQL table
        /// </summary>
        /// <param name="message">The JSON string of the new customer</param>
        /// <param name="log">The log for writing information messages</param>
        public static void ProcessCustomerQueueMessage([QueueTrigger("customer")] string message, TextWriter log)
        {
            SqlConnectionStringBuilder csBuilder;
            csBuilder = new SqlConnectionStringBuilder(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

            SqlConnection conn = new SqlConnection(csBuilder.ToString());
            conn.Open();

            CustomerObject c = Serialiser.deserialiseC(message);

            SqlParameter name = new SqlParameter("@name", SqlDbType.NVarChar, 50) { Value = c.name };
            SqlParameter country = new SqlParameter("@country", SqlDbType.NVarChar, 5) { Value = c.country };

            SqlCommand insertCommand = new SqlCommand("INSERT INTO Customer (Name, Country) VALUES (@name, @country)", conn);
            insertCommand.Parameters.Add(name);
            insertCommand.Parameters.Add(country);

            insertCommand.ExecuteNonQuery();

            conn.Close();
        }
    }
}
