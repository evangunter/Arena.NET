using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Arena.NET
{
    public class ArenaAPI
    {
        public Session Session { get; set; }
        public String APIUrl { get; set; }
        public Credentials Credentials { get; set; }

        private HttpClient _client;

        public HttpClient Client
        {
            get
            {
                if(String.IsNullOrWhiteSpace(APIUrl)) { throw new Exception("Cannot request client with specifiying the api uri."); }
                if (_client == null)
                {
                    HttpClient client = new HttpClient();
                    client.BaseAddress = new Uri(String.Format("{0}/{1}/", APIUrl, "api.svc"));
                    _client = client;
                }

                return _client;
            }
        }


        /// <summary>
        /// Initialize and configure the arena the api.
        /// </summary>
        /// <param name="apiUrl">The base URL of your arena API. Example: https://yourchurch.myshelby.org</param>
        /// <param name="apiCredentials">Credentials object for username, password, and apiKey</param>
        public ArenaAPI(string apiUrl, Credentials apiCredentials)
        {
            if(String.IsNullOrWhiteSpace(apiUrl) || !apiCredentials.HasCredentials) { throw new Exception("Invalid api url or credentials."); }

            //assuming we have a good base URI and credentials, let's connect and obtain a session if there isn't already one
            APIUrl = apiUrl;
            Credentials = apiCredentials;
            
        }

        /// <summary>
        /// If for some reason you already have a session
        /// </summary>
        /// <param name="session"></param>
        public ArenaAPI(string apiUrl, Session session)
        {
            if (session != null || String.IsNullOrWhiteSpace(apiUrl)) { throw new Exception("Invalid api url or session."); }

            APIUrl = apiUrl;
            Session = session;
        }

        public async Task GetSessionAsync()
        {
            //already have active session
            if(Session != null && !Session.IsExpired) { return; }

            //no credentials
            if(Credentials == null || !Credentials.HasCredentials) { return; }

            String action = "login";

            Client.DefaultRequestHeaders.Accept.Clear();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, action);

            //data to post
            var keyValuePairs = new List<KeyValuePair<string, string>>();
            keyValuePairs.Add(new KeyValuePair<string, string>("username", Credentials.ConvertToUnsecureString(Credentials.Username)));
            keyValuePairs.Add(new KeyValuePair<string, string>("password", Credentials.ConvertToUnsecureString(Credentials.Password)));
            keyValuePairs.Add(new KeyValuePair<string, string>("api_key", Credentials.ConvertToUnsecureString(Credentials.APIKey)));

            request.Content = new FormUrlEncodedContent(keyValuePairs);
            
            HttpResponseMessage response = await Client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var session = response.Content.ReadAsStringAsync().Result;
                XmlSerializer xmls = new XmlSerializer(typeof(Session));
                Session = (Session)xmls.Deserialize(new StringReader(session));
            }
        }
    }
}
