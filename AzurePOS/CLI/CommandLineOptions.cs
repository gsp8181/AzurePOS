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
        //[Option('r', "read", Required = true,
        //  HelpText = "Input file to be processed.")]
        //public string InputFile { get; set; }

        // omitting long name, default --verbose
        /*[Option('g', "gui",
          HelpText = "Displays the GUI associated with the program", MutuallyExclusiveSet = "gui")]
        public bool gui { get; set; }

        [Option('t', "type", HelpText = "Database to perform operations on (customer/order)")]
        public Type type { get; set; }

        [ParserState]
        public IParserState LastParserState { get; set; }

        [HelpOption]
        public string GetUsage()
        {
            return HelpText.AutoBuild(this,
              (HelpText current) => HelpText.DefaultParsingErrorsHandler(this, current));
        }*/

        public Options()
        {
           // ListVerb = new ListSubOptions { Patch = true };
        }
        [VerbOption("list", HelpText = "List a specified database (customer|order).")]
        public ListSubOptions ListVerb { get; set; }

        [VerbOption("register", HelpText = "Register a new customer.")]
        public RegisterSubOptions RegisterVerb { get; set; }

        [VerbOption("order", HelpText = "Register a new order.")]
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
        [Option('t', "type", HelpText = "Database to perform operations on (customer/order)", Required=true)]
        public database type { get; set; }


    }

    class RegisterSubOptions
    {
        // Remainder omitted
    }

    class OrderSubOptions
    {
        // Remainder omitted 
    }

    class LocationSubOptions
    {
        [Option("city", HelpText = "Enter a new city", MutuallyExclusiveSet="set")]
        public string city { get; set; }

        [Option("country", HelpText = "Enter a new country", MutuallyExclusiveSet = "set")]
        public string country { get; set; }

        [Option('v',"view",HelpText="View the associated city and country of the instance",MutuallyExclusiveSet="get")]
        public bool view { get; set; }
    }

    enum database
    {
        customer,
        order
    }

}
