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

        [XmlElement(ElementName = "CampusName")]
        [JsonProperty(PropertyName = "CampusName")]
        [JsonIgnore]
        public String CampusName { get; set; }

        [XmlElement(ElementName = "RecordStatusValue")]
        [JsonProperty(PropertyName = "RecordStatusValue")]
        [JsonIgnore]
        public String RecordStatus { get; set; }

        [XmlElement(ElementName = "FamilyMemberRoleValue")]
        [JsonProperty(PropertyName = "FamilyMemberRoleValue")]
        public String FamilyMemberRole { get; set; }

        [XmlElement(ElementName = "MedicalInformation")]
        [JsonProperty(PropertyName = "MedicalInformation")]
        public String MedicalInformation { get; set; }

        [XmlElement(ElementName = "FamilyMemberRoleID")]
        [JsonProperty(PropertyName = "FamilyMemberRoleID")]
        public int FamilyMemberRoleId { get; set; }

        [XmlArray("Addresses")]
        [XmlArrayItem("Address")]
        public List<Address> Addresses { get; set; }

        public int FamilyMembers { get; set; }

        public Person()
        {

        }

        internal Person(PersonFromGet personFromGet)
        {
            PersonId = personFromGet.PersonID;
            PersonIdentifier = Guid.Parse(personFromGet.PersonGUID);
            BirthDate = personFromGet.BirthDate;
            FirstName = personFromGet.FirstName;
            LastName = personFromGet.LastName;
            MemberStatus = personFromGet.MemberStatusValue;
            MemberStatusId = personFromGet.MemberStatusID;
            Gender = personFromGet.Gender;
            Emails = personFromGet.Emails;
            Phones = personFromGet.Phones;
            FamilyId = personFromGet.FamilyID;
            FamilyName = personFromGet.FamilyName;
            CampusId = personFromGet.CampusID;
            CampusName = personFromGet.CampusName;
            RecordStatus = personFromGet.RecordStatusValue;
            FamilyMemberRole = personFromGet.FamilyMemberRoleValue;
            MedicalInformation = personFromGet.MedicalInformation;
            FamilyMemberRoleId = personFromGet.FamilyMemberRoleID;
            Addresses = personFromGet.Addresses;
            FamilyMembers = personFromGet.FamilyMembersCount;

        }

        internal Person(PersonFromList personFromList)
        {
            PersonId = personFromList.PersonID;
            PersonIdentifier = Guid.Parse(personFromList.PersonGUID);
            BirthDate = personFromList.BirthDate;
            FirstName = personFromList.FirstName;
            LastName = personFromList.LastName;
            MemberStatus = personFromList.MemberStatusValue;
            MemberStatusId = personFromList.MemberStatusID;
            Gender = personFromList.Gender;
            FamilyId = personFromList.FamilyID;
            //FamilyName = personFromList.;
            CampusId = personFromList.CampusID;
            CampusName = personFromList.CampusName;
            RecordStatus = personFromList.RecordStatusValue;
            FamilyMemberRole = personFromList.FamilyMemberRoleValue;
            FamilyMemberRoleId = personFromList.FamilyMemberRoleID;
            Addresses = personFromList.Addresses;

            Emails = new List<Email> { new Email { Address = personFromList.FirstActiveEmail } };

            List<Phone> phones = new List<Phone>();
            if (!String.IsNullOrWhiteSpace(personFromList.HomePhone)) { phones.Add(new Phone { Number = personFromList.HomePhone, PhoneType = "Home" }); };
            if (!String.IsNullOrWhiteSpace(personFromList.CellPhone)) { phones.Add(new Phone { Number = personFromList.HomePhone, PhoneType = "Cell" }); };

            Phones = phones;
        }
    }

    //for desearlizing json
    internal class PersonCollection
    {
        public List<Person> Persons { get; set; }
    }
}
