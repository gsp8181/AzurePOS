using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzurePOS.Customer
{
    static class Adapter
    {
        public static string GetList()
        {
            return "customer list";
        }

        public static string Register(string name, string country)
        {
            return "added a customer";
        }

        public static string Register(string name)
        {
            string country = Location.Location.Country;
            return Register(name, country);
        }
    }
}
