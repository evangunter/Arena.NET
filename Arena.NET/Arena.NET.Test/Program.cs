using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arena.NET.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            String apiURL = "https://necc.myshelby.org";
            String username = "egunter";
            String password = "01644#Anecc3";
            String apiKey = "9f4be48b-d02e-4b48-a1dc-5f25dd52a776";

            var credentials = new Credentials(username, password, apiKey);

            //configure our API settings
            ArenaAPIConfiguration.Configure(apiURL, credentials);

            //start a session
            Task.WaitAll(StartSession());

            Console.WriteLine(ArenaAPIConfiguration.Session.SessionId + " - " + ArenaAPIConfiguration.Session.Expires);
            
        }

        private static async Task StartSession()
        {
            await ArenaAPIConfiguration.GetSessionAsync();
        }
    }
}
