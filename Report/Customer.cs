using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Report
{
    /// <summary>
    /// Table Entity to hold customers for recieving from Azure Tables
    /// </summary>
    class Customer : TableEntity
    {
        /// <summary>
        /// Creates a new customer table entity
        /// </summary>
        /// <param name="id">The customer ID</param>
        /// <param name="country">The two letter country code</param>
        public Customer(string id, string country)
        {
            this.RowKey = id;
            this.PartitionKey = country;
        }
        public Customer()
        {

        }
        /// <summary>
        /// The name of the customer
        /// </summary>
        public string name { get; set; }
    }
}
