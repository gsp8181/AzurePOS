using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzurePOS.Customer
{
    /// <summary>
    /// Class for holding customers to be serialised for transmission to the queue
    /// </summary>
    public class CustomerObject
    {
        /// <summary>
        /// Creates a new customer object
        /// </summary>
        /// <param name="name">The name of the customer</param>
        /// <param name="country">The two letter country code of the customer</param>
        public CustomerObject(string name, string country)
        {
            this.name = name;
            this.country = country;
        }
        public CustomerObject()
        {

        }
        /// <summary>
        /// The name of the customer
        /// </summary>
        public string name;
        /// <summary>
        /// The two letter country code of the customer
        /// </summary>
        public string country;
    }
}
