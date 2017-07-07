using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Arena.NET.Objects
{
    public class Person
    {
        [XmlElement(ElementName = "PersonID")]
        [JsonProperty(PropertyName = "PersonID")]
        public int PersonId { get; set; }

        [XmlElement(ElementName = "PersonGUID")]
        [JsonProperty(PropertyName = "PersonGUID")]
        public Guid PersonIdentifier { get; set; }

        public DateTime BirthDate { get; set; }

        public String FirstName { get; set; }

        public String LastName { get; set; }

        [XmlElement(ElementName = "MemberStatusValue")]
        [JsonProperty(PropertyName = "MemberStatusValue")]
        public String MemberStatus { get; set; }

        public String Gender { get; set; }

        public List<Email> Emails { get; set; }

        public List<Phone> Phones { get; set; }

        [XmlElement(ElementName = "FamilyID")]
        [JsonProperty(PropertyName = "FamilyID")]
        public int FamilyId { get; set; }

        public string FamilyName { get; set; }

        [XmlElement(ElementName = "FamilyMemberRoleValue")]
        [JsonProperty(PropertyName = "FamilyMemberRoleValue")]
        public String FamilyMemberRole { get; set; }

        public List<Address> Addresses { get; set; }
    }

    //for desearlizing json
    internal class PersonCollection
    {
        public List<Person> Persons { get; set; }
    }
}
