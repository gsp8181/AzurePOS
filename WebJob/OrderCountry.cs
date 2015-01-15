using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebJob
{
    class OrderCountry : TableEntity
    {
        public OrderCountry(string id, string country)
        {
            this.RowKey = id;
            this.PartitionKey = country;
        }
        public OrderCountry()
        {

        }
        public string customerId { get; set; }
        public string sku { get; set; }
        public DateTime dateTime { get; set; }
        public double price { get; set; }
    }
}
