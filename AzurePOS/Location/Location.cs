using AzurePOS.Properties;

namespace AzurePOS.Location
{
    /// <summary>
    /// Holds and retrieves properties for the city and country of the current instance
    /// </summary>
    static class Location
    {

        /// <summary>
        /// The current city
        /// </summary>
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

        /// <summary>
        /// The current country in two letter format
        /// </summary>
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

        /// <summary>
        /// True if both the city and the country are set
        /// </summary>
        internal static bool IsSet
        {
            get { return (City != "" && Country != "");}
        }

        /// <summary>
        /// Gets a string of the location information
        /// </summary>
        /// <returns>The formatted string</returns>
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
