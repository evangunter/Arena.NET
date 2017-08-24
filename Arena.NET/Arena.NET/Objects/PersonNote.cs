using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Arena.NET.Objects
{
    public class PersonNote
    {
        [XmlElement(ElementName = "Private")]
        [JsonProperty(PropertyName = "Private")]
        public Boolean IsPrivate { get; set; }

        [XmlElement(ElementName = "SecurityTemplateID")]
        [JsonProperty(PropertyName = "SecurityTemplateID")]
        public int SecurityTemplateId { get; set; }

        [XmlElement(ElementName = "Text")]
        [JsonProperty(PropertyName = "Text")]
        public String Note { get; set; }
    }
}
