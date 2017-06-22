using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arena.NET
{
    public class Session
    {
        public Guid SessionId { get; set; }
        public DateTime Expires { get; set; }

        public Session(Guid sessionId, DateTime expires)
        {
            SessionId = sessionId;
            Expires = expires.ToUniversalTime();
        }

        public Boolean IsExpired
        {
            get
            {
                return (Expires < DateTime.UtcNow);
            }
        }
    }
}
