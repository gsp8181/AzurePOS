using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzurePOS.Order
{
    static class Adapter
    {
        public static string GetList()
        {
            return "order list";
        }

        public static string Register(string customerId, string sku, string dateTime, decimal price)
        {
            return "made new order!";
        }
    }
}
