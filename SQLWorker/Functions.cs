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
    public class Functions
    {
        // This function will get triggered/executed when a new message is written 
        // on an Azure Queue called order.
        public static void ProcessOrderQueueMessage([QueueTrigger("order")] string message, TextWriter log)
        {
            log.WriteLine(message);
        }

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
