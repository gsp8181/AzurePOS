using AzurePOS.Customer;
using AzurePOS.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace AzurePOS.Serialiser
{
    static class Serialiser
    {
        /*public static string deserialise(string input)
        {
            return "";
        }*/
        public static string serialise(CustomerObject input)
        {
            string json = new JavaScriptSerializer().Serialize(input);
            return json;
        }
        public static string serialise(OrderObject input)
        {
            string json = new JavaScriptSerializer().Serialize(input);
            return json;
        }
        public static CustomerObject deserialiseC(string input)
        {
            return new JavaScriptSerializer().Deserialize<CustomerObject>(input);
        }
        public static OrderObject deserialiseO(string input)
        {
            return new JavaScriptSerializer().Deserialize<OrderObject>(input);
        }
    }
}
