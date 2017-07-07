using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Arena.NET.Objects
{
    [XmlRoot(ElementName = "ModifyResult")]
    public class ArenaPostResult
    {
        public String ErrorMessage { get; set; }

        [XmlElement(ElementName = "Link")]
        [JsonProperty(PropertyName = "Link")]
        public String Action { get; set; }

        [XmlElement(ElementName = "ObjectID")]
        [JsonProperty(PropertyName = "ObjectID")]
        public int ObjectId { get; set; }

        [XmlElement(ElementName = "Successful")]
        [JsonProperty(PropertyName = "Successful")]
        public Boolean WasSuccessful { get; set; }

        [XmlElement(ElementName = "ValidationResults")]
        public ValidationResults ValidationResults { get; set; }
    }

    [XmlRoot(ElementName = "ModifyValidationResult")]
    public class Error
    {
        [XmlElement(ElementName = "Key")]
        [JsonProperty(PropertyName = "Key")]
        public String Key { get; set; }

        [XmlElement(ElementName = "Message")]
        [JsonProperty(PropertyName = "Message")]
        public String Message { get; set; }
    }

    [XmlRoot(ElementName = "ValidationResults")]
    public class ValidationResults
    {
        [XmlElement(ElementName = "ModifyValidationResult", Namespace = "http://schemas.datacontract.org/2004/07/Arena.Services.Contracts")]
        public List<Error> Error { get; set; }
        [XmlAttribute(AttributeName = "a", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string A { get; set; }
    }
}
