using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoSQLWorker
{
    /// <summary>
    /// Table entity for holding orders for sending/recieving from Azure Tables
    /// </summary>
    class Order : TableEntity
    {
        /// <summary>
        /// Creates a new order entity
        /// </summary>
        /// <param name="id">The ID of the order</param>
        /// <param name="customerId">The ID of the customer</param>
        public Order(string id, string customerId)
        {
            this.RowKey = id;
            this.PartitionKey = customerId;
        }
        public Order()
        {

        }
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
