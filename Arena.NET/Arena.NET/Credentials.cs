using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Arena.NET
{
    public class Credentials
    {
        public SecureString Username { get; set; }

        public SecureString Password { get; set; }

        public SecureString APIKey { get; set; }

        public SecureString APISecretKey { get; set; }


        public Credentials(String username, String password, String apiKey, String apiSecretKey)
        {
            if (String.IsNullOrWhiteSpace(username) || String.IsNullOrWhiteSpace(password) || String.IsNullOrWhiteSpace(apiKey) || String.IsNullOrWhiteSpace(apiSecretKey))
            {
                throw new Exception("Invalid credentials. Empty.");
            }

            //Convert ID to SecureString
            Username = ConvertToSecureString(username);
            Password = ConvertToSecureString(password);
            APIKey = ConvertToSecureString(apiKey);
            APISecretKey = ConvertToSecureString(apiSecretKey);

        }

        public Boolean HasCredentials
        {
            get
            {
                return (Username != null && Password != null && APIKey != null && Username.Length > 0 && Password.Length > 0 && APIKey.Length > 0 && APISecretKey.Length > 0);
            }
        }

        public SecureString ConvertToSecureString(String unsecurePassword)
        {
            SecureString thisString = new SecureString();
            foreach (var character in unsecurePassword.ToCharArray())
            {
                thisString.AppendChar(character);
            }

            return thisString;
        }

        public string ConvertToUnsecureString(SecureString securePassword)
        {
            if (securePassword == null)
                throw new ArgumentNullException("securePassword");

            IntPtr unmanagedString = IntPtr.Zero;
            try
            {
                unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(securePassword);
                return Marshal.PtrToStringUni(unmanagedString);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            }
        }
    }
}
