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

        [XmlElement(ElementName = "MemberStatusID")]
        [JsonProperty(PropertyName = "MemberStatusID")]
        public int MemberStatusId { get; set; }

        public String Gender { get; set; }

        [XmlArray("Emails")]
        [XmlArrayItem("Email")]
        public List<Email> Emails { get; set; }

        [XmlArray("Phones")]
        [XmlArrayItem("Phone")]
        public List<Phone> Phones { get; set; }

        [XmlElement(ElementName = "FamilyID")]
        [JsonProperty(PropertyName = "FamilyID")]
        public int FamilyId { get; set; }

        public string FamilyName { get; set; }

        [XmlElement(ElementName = "CampusID")]
        [JsonProperty(PropertyName = "CampusID")]
        public int CampusId { get; set; }

        [XmlElement(ElementName = "FamilyMemberRoleValue")]
        [JsonProperty(PropertyName = "FamilyMemberRoleValue")]
        public String FamilyMemberRole { get; set; }

        [XmlArray("Addresses")]
        [XmlArrayItem("Address")]
        public List<Address> Addresses { get; set; }
    }

    //for desearlizing json
    internal class PersonCollection
    {
        public List<Person> Persons { get; set; }
    }
}
