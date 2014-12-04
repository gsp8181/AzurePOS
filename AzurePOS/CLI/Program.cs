using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using CommandLine;
using CommandLine.Text;
using System.Globalization;
using AzurePOS.Offline;

namespace AzurePOS.CLI
{
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main(string[] args)
        {
            Resend.ResendMessage();
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

                if (commitSubOptions.customer)
                {
                    Console.WriteLine(Customer.Adapter.GetList());
                }
                else if (commitSubOptions.order)
                {
                    Console.WriteLine(Order.Adapter.GetList());
                }
                else
                {
                    Console.WriteLine(HelpText.AutoBuild(options, invokedVerb)); //TODO: put this away 
                }

            }
            else if (invokedVerb == "location")
            {
                LocationSubOptions commitSubOptions = (LocationSubOptions)invokedVerbInstance;
                if (commitSubOptions.view)
                {
                    Console.WriteLine(Location.Location.ToString());
                }
                else
                {
                    if (!String.IsNullOrEmpty(commitSubOptions.city) && !String.IsNullOrEmpty(commitSubOptions.country))
                    {
                        Location.Location.City = commitSubOptions.city;
                        Location.Location.Country = commitSubOptions.country;
                        Console.WriteLine("New values saved");
                        Console.WriteLine(Location.Location.ToString());
                    }
                    else if (!String.IsNullOrEmpty(commitSubOptions.city))
                    {
                        Location.Location.City = commitSubOptions.city;
                        Console.WriteLine("New values saved");
                        Console.WriteLine(Location.Location.ToString());
                    }
                    else if (!String.IsNullOrEmpty(commitSubOptions.country))
                    {
                        Location.Location.Country = commitSubOptions.country;
                        Console.WriteLine("New values saved");
                        Console.WriteLine(Location.Location.ToString());
                    }
                    else
                    {
                        Console.WriteLine(HelpText.AutoBuild(options, invokedVerb)); //TODO: put this away
                    }
                }


            }
            else if (!Location.Location.IsSet)
            {
                Console.WriteLine("Location is not currently set\nPlease set location using the location verb commands");
            }
            else if (invokedVerb == "register")
            {
                RegisterSubOptions commitSubOptions = (RegisterSubOptions)invokedVerbInstance;
                string result = Customer.Adapter.Register(commitSubOptions.name, Location.Location.Country); //TODO: country necessary or from settings
                Console.WriteLine(result);
            }
            else if (invokedVerb == "order")
            {
                OrderSubOptions commitSubOptions = (OrderSubOptions)invokedVerbInstance;

                string dateStr = commitSubOptions.date;
                string timeStr = commitSubOptions.time;
                DateTime dt = new DateTime();
                bool write = false;
                if (String.IsNullOrEmpty(dateStr) && String.IsNullOrEmpty(timeStr))
                {
                    dt = DateTime.Now;
                    write = true;
                }
                else if (String.IsNullOrEmpty(dateStr))
                {
                    write = DateTime.TryParseExact(timeStr, "HH:mm", CultureInfo.CurrentCulture, DateTimeStyles.AssumeLocal, out dt);
                }
                else if (!String.IsNullOrEmpty(dateStr) && !String.IsNullOrEmpty(timeStr))
                {
                    write = DateTime.TryParseExact(dateStr + " " + timeStr, "dd/MM/yyyy HH:mm", CultureInfo.CurrentCulture, DateTimeStyles.AssumeLocal, out dt);
                }

                if (write)
                {
                    string result = Order.Adapter.Register(commitSubOptions.customerId, commitSubOptions.sku, dt, commitSubOptions.price); //TODO: country necessary or from settings
                    Console.WriteLine(result);
                }
                else
                {
                    Console.WriteLine(HelpText.AutoBuild(options, invokedVerb)); //TODO: put this away
                }

            }
        }
    }
}
