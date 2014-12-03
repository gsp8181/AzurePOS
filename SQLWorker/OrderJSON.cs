﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLWorker
{
    public class OrderObject
    {
        public OrderObject(string customerId, string sku, DateTime dateTime, decimal price)
        {
            this.customerId = customerId;
            this.sku = sku;
            this.dateTime = dateTime;
            this.price = price;
        }
        public OrderObject()
        {
        }
        public string customerId;
        public string sku;
        public DateTime dateTime;
        public decimal price;
    }
}