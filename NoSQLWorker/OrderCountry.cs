using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoSQLWorker
{
    /// <summary>
    /// Table entity for holding orders with countries for sending/recieving from the special Azure table built for reporting purposes
    /// </summary>
    class OrderCountry : TableEntity
    {
        /// <summary>
        /// Creates a new order entity
        /// </summary>
        /// <param name="id">The ID of the order</param>
        /// <param name="customerId">The country of the order</param>
        public OrderCountry(string id, string country)
        {
            this.RowKey = id;
            this.PartitionKey = country;
        }
        public OrderCountry()
        {

        }
        /// <summary>
        /// The ID of the customer
        /// </summary>
        public string customerId { get; set; }
        /// <summary>
        /// The SKU of the object
        /// </summary>
        public string sku { get; set; }
        /// <summary>
        /// The date and time of the order
        /// </summary>
        public DateTime dateTime { get; set; }
        /// <summary>
        /// The price of the order
        /// </summary>
        public double price { get; set; }
    }
}
