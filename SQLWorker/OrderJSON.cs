using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLWorker
{
    /// <summary>
    /// Holds an order for serilaisation to JSON
    /// </summary>
    public class OrderObject
    {
        /// <summary>
        /// Creates a new order object
        /// </summary>
        /// <param name="customerId">The ID of the customer</param>
        /// <param name="sku">The SKU of the object</param>
        /// <param name="dateTime">The date and time of the order</param>
        /// <param name="price">The price of the order</param>
        public OrderObject(string customerId, string sku, DateTime dateTime, double price)
        {
            this.customerId = customerId;
            this.sku = sku;
            this.dateTime = dateTime;
            this.price = price;
        }
        public OrderObject()
        {
        }
        /// <summary>
        /// The ID of the customer
        /// </summary>
        public string customerId;
        /// <summary>
        /// The SKU of the object
        /// </summary>
        public string sku;
        /// <summary>
        /// The date and time of the order
        /// </summary>
        public DateTime dateTime;
        /// <summary>
        /// The price of the order
        /// </summary>
        public double price;
    }
}
