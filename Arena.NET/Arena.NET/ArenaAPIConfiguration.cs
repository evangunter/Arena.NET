using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Arena.NET
{
    public static class ArenaAPIConfiguration
    {
        public static Session Session { get; set; }
        public static String APIUrl { get; set; }

        public static HttpClient Client
        {
            get
            {
                if(String.IsNullOrWhiteSpace(APIUrl)) { throw new Exception("Cannot request client with specifiying the api uri."); }
                if (Client == null)
                {
                    HttpClient client = new HttpClient();
                    client.BaseAddress = new Uri(String.Format("{0}/{1}/", APIUrl, "api.svc"));
                    return new HttpClient();
                }

                return Client;
            }
        }


        /// <summary>
        /// Initialize and configure the arena the api.
        /// </summary>
        /// <param name="apiUrl">The base URL of your arena API. Example: https://yourchurch.myshelby.org</param>
        /// <param name="apiCredentials">Credentials object for username, password, and apiKey</param>
        public static void Configure(string apiUrl, Credentials apiCredentials)
        {
            if(String.IsNullOrWhiteSpace(apiUrl) || !apiCredentials.HasCredentials) { throw new Exception("Invalid api url or credentials."); }

            //assuming we have a good base URI and credentials, let's connect and obtain a session if there isn't already one
            APIUrl = apiUrl;
        }

        /// <summary>
        /// If for some reason you already have a session
        /// </summary>
        /// <param name="session"></param>
        public static void Configure(string apiUrl, Session session)
        {
            if (session != null || String.IsNullOrWhiteSpace(apiUrl)) { throw new Exception("Invalid api url or session."); }

            APIUrl = apiUrl;
            Session = session;
        }

        private static async Task<Session> GetSessionAsync()
        {
            if(Session != null && !Session.IsExpired) { return Session; }

            String action = "login";

            Client.DefaultRequestHeaders.Accept.Clear();

            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));//ACCEPT header

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, action);
            request.Content = new StringContent("{\"name\":\"John Doe\",\"age\":33}",
                                                Encoding.UTF8,
                                                "application/json");//CONTENT-TYPE header

            client.SendAsync(request)
                  .ContinueWith(responseTask =>
                  {
                      Console.WriteLine("Response: {0}", responseTask.Result);
                  });

            HttpResponseMessage response = await Client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                Session = await response.Content.ReadAsAsync<Session>();
            }

            return Session;
        }
    }
}
