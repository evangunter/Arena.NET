using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Arena.NET.Objects
{
    public class Address
    {
        [XmlElement(ElementName = "AddressID")]
        [JsonProperty(PropertyName = "AddressID")]
        public int AddressId { get; set; }

        [XmlElement(ElementName = "AddressTypeID")]
        [JsonProperty(PropertyName = "AddressTypeID")]
        public int AddressTypeId { get; set; }

        public String AddressTypeValue { get; set; }

        public String City { get; set; }

        public String Country { get; set; }

        public String PostalCode { get; set; }

        [XmlElement(ElementName = "Primary")]
        [JsonProperty(PropertyName = "Primary")]
        public Boolean IsPrimary { get; set; }

        public String State { get; set; }

        public Double Latitude { get; set; }

        public Double Longitude { get; set; }

        [XmlElement(ElementName = "StreetLine1")]
        [JsonProperty(PropertyName = "StreetLine1")]
        public String Address1 { get; set; }

        [XmlElement(ElementName = "StreetLine2")]
        [JsonProperty(PropertyName = "StreetLine2")]
        public String Address2 { get; set; }

        //add the values with data to the search parameters
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            if (!String.IsNullOrWhiteSpace(Address1)) { builder.Append(String.Format("{0} ", Address1)); }

            if (!String.IsNullOrWhiteSpace(Address2)) { builder.Append(String.Format("{0} ", Address2)); }

            if (!String.IsNullOrWhiteSpace(City) && !String.IsNullOrWhiteSpace(State)) { builder.Append(String.Format("{0}, {1} ", City, State)); }

            if (!String.IsNullOrWhiteSpace(PostalCode)) { builder.Append(String.Format("{0}", PostalCode)); }

            return builder.ToString();
        }
    }
}
