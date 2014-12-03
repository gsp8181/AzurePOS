using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzurePOS.Customer
{

    public class CustomerObject
    {
        public CustomerObject(string name, string country)
        {
            this.name = name;
            this.country = country;
        }
        public CustomerObject()
        {

        }
        public string name;
        public string country;
    }
}
