using Arena.NET.Objects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Arena.NET.Repositories
{
    public class Repository
    {
        private ArenaAPI ArenaAPI { get; set; }

        private String _action;

        public String Action
        {
            get
            {
                return _action;
            }
            set
            {
                var baseAction = (value.Contains("?")) ? String.Format("{0}&api_session={1}", value, ArenaAPI.Session.SessionId.ToString()) : String.Format("{0}?api_session={1}", value, ArenaAPI.Session.SessionId.ToString());
                _action = String.Format("{0}&api_sig={1}", baseAction, GetAPISignature(String.Format("{0}_{1}", ArenaAPI.Credentials.ConvertToUnsecureString(ArenaAPI.Credentials.APISecretKey), baseAction)));
            }
        }

        public Repository(ArenaAPI arenaAPI)
        {
            ArenaAPI = arenaAPI;
        }

        public async Task<ArenaPostResult> ExecutePost(HttpRequestMessage request)
        {
            String exceptionResponse = String.Empty;

            HttpResponseMessage response = await ArenaAPI.Client.PostAsync(request.RequestUri, request.Content);
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                result = result.Replace("True", "true").Replace("False", "false");
                XmlSerializer xmls = new XmlSerializer(typeof(ArenaPostResult));

                //attempt to desearlize result
                try
                {
                    return (ArenaPostResult)xmls.Deserialize(new StringReader(result));
                }
                catch (Exception exception)
                {
                    exceptionResponse = exception.Message;
                }

            }
            
            ArenaPostResult postResult = new ArenaPostResult();
            postResult.WasSuccessful = false;
            postResult.Action = Action;
            postResult.ErrorMessage = String.Format("Response Status Code: {0}, Reason: {1}, Exception: {2}", response.StatusCode.ToString(), response.ReasonPhrase, exceptionResponse);

            return postResult;
            
        }

        public async Task<T> ExecuteGet<T>(HttpRequestMessage request)
        {
            String exceptionResponse = String.Empty;

            HttpResponseMessage response = await ArenaAPI.Client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                //XmlSerializer xmls = new XmlSerializer(typeof(T));

                //attempt to desearlize
                try
                {
                    return JsonConvert.DeserializeObject<T>(result);
                    //return (T)xmls.Deserialize(new StringReader(result));
                }
                catch (Exception exception)
                {
                    exceptionResponse = exception.Message;
                }
                
            }

            throw new Exception(GetResponseExceptionMessage(response));
        }

        private String GetResponseExceptionMessage(HttpResponseMessage response)
        {
            if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
            {
                return "Forbidden request";
            }

            return String.Empty;
        }

        private string GetAPISignature(String action)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(action.ToLower());
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }
    }
}
