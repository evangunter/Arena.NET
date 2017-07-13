using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Arena.NET.Objects
{
    public class Phone
    {
        public String Extension { get; set; }

        public String Number { get; set; }

        [XmlElement(ElementName = "PhoneTypeID")]
        [JsonProperty(PropertyName = "PhoneTypeID")]
        public int PhoneTypeId { get; set; }

        [XmlElement(ElementName = "PhoneTypeValue")]
        [JsonProperty(PropertyName = "PhoneTypeValue")]
        [XmlIgnore]
        public String PhoneType { get; set; }

        public Boolean SMSEnabled { get; set; }

        public Boolean Unlisted { get; set; }
    }
}
