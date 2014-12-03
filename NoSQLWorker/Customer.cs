using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoSQLWorker
{
    class Customer : TableEntity
    {
        public Customer(string id, string country)
        {
            this.RowKey = id;
            this.PartitionKey = country;
        }
        public Customer()
        {

        }
        public string name { get; set; }
    }
}
