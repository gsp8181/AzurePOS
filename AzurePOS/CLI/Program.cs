using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using CommandLine;
using CommandLine.Text;
using AzurePOS.Location;

namespace AzurePOS.CLI
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            string invokedVerb = "";
            object invokedVerbInstance = null;

            var options = new Options();
            if (!CommandLine.Parser.Default.ParseArguments(args, options,
              (verb, subOptions) =>
              {
                  // if parsing succeeds the verb name and correct instance
                  // will be passed to onVerbCommand delegate (string,object)
                  invokedVerb = verb;
                  invokedVerbInstance = subOptions;
              }))
            {
                Environment.Exit(CommandLine.Parser.DefaultExitCodeFail);
            }

            if (invokedVerb == "list")
            {
                ListSubOptions commitSubOptions = (ListSubOptions)invokedVerbInstance;
                if(commitSubOptions.type == database.customer)
                {
                    Console.WriteLine("List of customers will go here");
                } 
                else if (commitSubOptions.type == database.order)
                {
                    Console.WriteLine("List of orders will go here");
                }
            
            }
            if (invokedVerb == "register")
            {
                RegisterSubOptions commitSubOptions = (RegisterSubOptions)invokedVerbInstance;
            }
            if (invokedVerb == "order")
            {
                OrderSubOptions commitSubOptions = (OrderSubOptions)invokedVerbInstance;
            }
            if (invokedVerb == "location")
            {
                LocationSubOptions commitSubOptions = (LocationSubOptions)invokedVerbInstance;
                if (commitSubOptions.view)
                {
                    Console.WriteLine(Location.Location.ToString());
                }
                else
                {
                    if (String.IsNullOrEmpty(commitSubOptions.city) || String.IsNullOrEmpty(commitSubOptions.country))
                    {
                        Console.WriteLine(HelpText.AutoBuild(options, invokedVerb));
                    }
                    else 
                    {
                        Location.Location.City = commitSubOptions.city;
                        Location.Location.Country = commitSubOptions.country;
                        Console.WriteLine("New values saved");
                        Console.WriteLine(Location.Location.ToString());
                    }
                }
                    
                
            }
        }
    }
}
