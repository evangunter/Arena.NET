using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Arena.NET
{
    [XmlRoot(ElementName = "ApiSession")]
    public class Session
    {
        [XmlElement(ElementName = "SessionID")]
        public Guid SessionId { get; set; }

        [XmlElement(ElementName = "DateExpires")]
        public DateTime Expires { get; set; }

        public Session(Guid sessionId, DateTime expires)
        {
            SessionId = sessionId;
            Expires = expires.ToUniversalTime();
        }

        public Session()
        {

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
