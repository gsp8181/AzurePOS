using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using CommandLine;

namespace AzurePOS
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

                } 
                else if (commitSubOptions.type == database.order)
                {

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
            }
        }

        private static void RunGUI()
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());
        }
    }
}
