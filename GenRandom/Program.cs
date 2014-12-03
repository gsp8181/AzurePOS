﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GenRandom
{
    class Program
    {
        static void Main(string[] args)
        {
            AzurePOS.CLI.Program.Main(new string[] { "location", "--city", "Newcastle upon Tyne", "--country", "United Kingdom" });
            for(int i=0;i<1000;i++)
            {
                Console.WriteLine("added " + i);
                AzurePOS.CLI.Program.Main(new string[] { "register", "-n", "\"Geoffrey Prytherch\"" }) ;
            }
            for(int i=0;i<10000;i++)
            {
                int customerid = 1;
                string sku = "sku";
                DateTime dt = DateTime.Today;
                string price = "00.00";
                AzurePOS.CLI.Program.Main(new string[] { "order", "-i", customerid.ToString(), "-s", sku, "-d", dt.ToString("dd/MM/yyyy"), "-t", dt.ToString("HH:mm"), "-p", price });
            }
        }
    }
}
