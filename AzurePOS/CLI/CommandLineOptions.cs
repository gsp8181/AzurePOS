using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLine;
using CommandLine.Text;

namespace AzurePOS.CLI
{
    class Options
    {

        public Options()
        {
           // ListVerb = new ListSubOptions { Patch = true };
        }
        [VerbOption("list", HelpText = "List a specified database (customer|order).")]
        public ListSubOptions ListVerb { get; set; }

        [VerbOption("register", HelpText = "Register a new customer.")]
        public RegisterSubOptions RegisterVerb { get; set; }

        [VerbOption("order", HelpText = "Place a new order.")]
        public OrderSubOptions OrderVerb { get; set; }

        [VerbOption("location", HelpText = "View (or set) the location of this instance.")]
        public LocationSubOptions LocationCountryVerb { get; set; }

        [HelpVerbOption]
        public string GetUsage(string verb)
        {
            return HelpText.AutoBuild(this, verb);
        }

    }

    class ListSubOptions
    {

        public ListSubOptions()
        {

        }

        [VerbOption("customer", HelpText = "View the customer database")]
        public bool customer { get; set; }

        [VerbOption("order", HelpText = "View the order database")]
        public bool order { get; set; }

    }

    class RegisterSubOptions
    {
        [Option('n',"name", HelpText = "Name of customer", Required=true)]
        public string name { get; set; }

        //[Option('c',"country", HelpText = "Country of customer", Required=true)]
        //public string country { get; set; } //TODO: should this not just be got from Location?
    }

    class OrderSubOptions
    {
        [Option('i',"id", HelpText = "Customer ID", Required = true)]
        public string customerId { get; set; }

        [Option('s',"SKU", HelpText = "Stock Keeping Unit (SKU) code", Required = true)]
        public string sku { get; set; }

        [Option('d', "date", HelpText = "Date and Time of the order (dd/MM/yyyy). A time is also required if date is set")]
        public string date { get; set; }

        [Option('t', "time", HelpText = "Time of the order (HH:mm)")]
        public string time { get; set; } 

        [Option('p', "price", HelpText = "Price (omitting currency code)", Required = true)] //TODO: really omit currency code?
        public decimal price { get; set; }
    }

    class LocationSubOptions
    {
        [Option("city", HelpText = "Enter a new city", MutuallyExclusiveSet="set")]
        public string city { get; set; }

        [Option("country", HelpText = "Enter a new country code (eg GB)", MutuallyExclusiveSet = "set")]
        public string country { get; set; }

        [Option('v',"view",HelpText="View the associated city and country of the instance",MutuallyExclusiveSet="get")]
        public bool view { get; set; }
    }

}
