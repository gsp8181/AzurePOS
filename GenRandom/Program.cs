using System;

namespace GenRandom
{
    class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random(Guid.NewGuid().GetHashCode());
            AzurePOS.CLI.Program.Main(new string[] { "location", "--city", "Newcastle upon Tyne", "--country", "United Kingdom" });
            for(int i=0;i<1000;i++)
            {

                string firstName = Names.firstNames[random.Next(Names.firstNames.Length)];
                string lastName = Names.lastNames[random.Next(Names.lastNames.Length)];
                string fullName = firstName + " " + lastName;
                AzurePOS.CLI.Program.Main(new string[] { "register", "-n", fullName }) ;
            }
            for(int i=0;i<10000;i++)
            {
                int customerid = 1;
                string sku = "AZP-TEST-" + random.Next(10).ToString() + random.Next(10).ToString() + random.Next(10).ToString() + random.Next(10).ToString();
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
