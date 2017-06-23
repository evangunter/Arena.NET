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
            String apiURL = "XXXX";
            String username = "XXXX";
            String password = "XXXXX";
            String apiKey = "XXXX";

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
