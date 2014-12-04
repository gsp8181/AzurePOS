using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Report
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("Run this program with Reports.exe <Country Code>");
                Console.WriteLine(@"If deployed as an Azure OnDemand WebJob, invoke with POST https://<site name>.scm.azurewebsites.net/api/triggeredwebjobs/Report/run?arguments=<country code>");
                Environment.Exit(0);
            }
            string country = args[0];
            Console.WriteLine("Generating report for " + country);
            double total = ReportObjects.TotalOrdersFromCountry(country);
            double average = ReportObjects.MeanOrdersFromCountry(country);
            List<Order> orders = ReportObjects.OrdersFromLastSevenDays(country);

            Console.WriteLine("The total amount of orders for " + country + " came to " + total.ToString("C"));
            Console.WriteLine("The mean average order for " + country + " came to " + average.ToString("C"));
            Console.WriteLine("Orders from the last 7 days from " + country);
            foreach (Order o in orders)
            {
                Console.WriteLine("\n" + o.RowKey + ": customer " + o.PartitionKey + " " + o.sku + " " + o.dateTime.ToString("dd/MM/yyyy HH:mm") + " " + o.price.ToString("C"));
            }

        }
    }
}
