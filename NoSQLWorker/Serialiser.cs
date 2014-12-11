using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace NoSQLWorker
{
    /// <summary>
    /// Performs serialisation on the Customer and Order objects to transmit them successfully in string format to the queue for processing and retrieving them from the queue later on.
    /// </summary>
    static class Serialiser
    {
        /// <summary>
        /// Serialises a customer
        /// </summary>
        /// <param name="input">The customer object</param>
        /// <returns>The serialised JSON</returns>
        public static string serialise(CustomerObject input)
        {
            string json = new JavaScriptSerializer().Serialize(input);
            return json;
        }
        /// <summary>
        /// Serialises an order
        /// </summary>
        /// <param name="input">The order object</param>
        /// <returns>The serialised JSON</returns>
        public static string serialise(OrderObject input)
        {
            string json = new JavaScriptSerializer().Serialize(input);
            return json;
        }
        /// <summary>
        /// Deserialises a customer JSON string
        /// </summary>
        /// <param name="input">The serialised Customer JSON</param>
        /// <returns>The customer object</returns>
        public static CustomerObject deserialiseC(string input)
        {
            return new JavaScriptSerializer().Deserialize<CustomerObject>(input);
        }
        /// <summary>
        /// Deserialises an order JSON string
        /// </summary>
        /// <param name="input">The serialised Order JSON</param>
        /// <returns>The order object</returns>
        public static OrderObject deserialiseO(string input)
        {
            return new JavaScriptSerializer().Deserialize<OrderObject>(input);
        }
    }
}
