using System;

namespace GenRandom
{
    class Program
    {
        /// <summary>
        /// Generates 1000 random customers and 10000 random orders and feeds them to the AzurePOS program for addition to the queue
        /// </summary>
        /// <param name="args">Not used at this time</param>
        static void Main(string[] args)
        {
            Random random = new Random(Guid.NewGuid().GetHashCode());
            AzurePOS.CLI.Program.Main(new string[] { "location", "--city", "Newcastle upon Tyne", "--country", "GB" });
            for(int i=0;i<1000;i++)
            {

                string firstName = Names.firstNames[random.Next(Names.firstNames.Length)];
                string lastName = Names.lastNames[random.Next(Names.lastNames.Length)];
                string fullName = firstName + " " + lastName;
                AzurePOS.CLI.Program.Main(new string[] { "register", "-n", fullName }) ;
            }
            for(int i=0;i<10000;i++)
            {
                int customerid = random.Next(1,1000);
                string sku = "AZP-TEST-" + random.Next(10).ToString() + random.Next(10).ToString() + random.Next(10).ToString();// + random.Next(10).ToString();
                DateTime dt = DateTime.Today;
                dt = dt.AddMinutes(- random.Next(525949)); //525949 is the number of seconds in a year
                string p1 = random.Next(1000).ToString();
                string p2 = random.Next(100).ToString("00");
                string price = p1 + "." + p2;
                AzurePOS.CLI.Program.Main(new string[] { "order", "-i", customerid.ToString(), "-s", sku, "-d", dt.ToString("dd/MM/yyyy"), "-t", dt.ToString("HH:mm"), "-p", price });
            }
        }
    }
}
