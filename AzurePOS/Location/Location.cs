using AzurePOS.Properties;

namespace AzurePOS.Location
{
    static class Location
    {

        internal static string City
        {
            get
            {
                return (string)Settings.Default["City"];
            }
            set
            {
                Settings.Default["City"] = value;
                Settings.Default.Save();
            }
        }

        internal static string Country
        {
            get
            {
                return (string)Settings.Default["Country"];
            }
            set
            {
                Settings.Default["Country"] = value;
                Settings.Default.Save();
            }
        }

        internal static bool IsSet
        {
            get { return (City != "" && Country != "");}
        }

#pragma warning disable 0114
        internal static string ToString()
        {
            string output = "";
            if (City != "" && Country != "")
            {
                output += ("Current Location Information\n");
                output += (City + ", " + Country);
            } else if (City != "" || Country != "")
                {
                    output += ("Current Location Information\n(Partially Set)\n");
                    output += (City + ", " + Country);
                }
                else
            {
                output += ("No associated location information");
            }
            return output;
        }

    }
}
