using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzurePOS.Order
{
    class Order : TableEntity
    {
        public Order(string id, string customerId)
        {
            this.RowKey = id;
            this.PartitionKey = customerId;
        }
        public Order()
        {

        }
        public string sku { get; set; }
        public DateTime dateTime { get; set; }
        public double price { get; set; }
    }
}
