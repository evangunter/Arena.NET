using Arena.NET.Objects.APIObjects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Arena.NET.Objects
{

    public class GroupMember
    {
        [XmlElement(ElementName = "Active")]
        [JsonProperty(PropertyName = "Active")]
        public Boolean IsActive { get; set; }

        [XmlIgnore]
        [JsonIgnore]
        public DateTime DateInactive { get; set; }

        [XmlIgnore]
        [JsonIgnore]
        public DateTime DateJoined { get; set; }

        [XmlElement(ElementName = "GroupID")]
        [JsonProperty(PropertyName = "GroupID")]
        [XmlIgnore]
        [JsonIgnore]
        public int GroupId { get; set; }

        public String MemberNotes { get; set; }

        [XmlElement(ElementName = "PersonID")]
        [JsonProperty(PropertyName = "PersonID")]
        [XmlIgnore]
        [JsonIgnore]
        public int PersonId { get; set; }

        [XmlElement(ElementName = "PersonInformation")]
        [JsonProperty(PropertyName = "PersonInformation")]
        [XmlIgnore]
        [JsonIgnore]
        public Person Person { get; set; }

        [XmlIgnore]
        [JsonIgnore]
        public String PersonLink { get; set; }

        [XmlElement(ElementName = "RoleID")]
        [JsonProperty(PropertyName = "RoleID")]
        public int RoleId { get; set; }

        [XmlElement(ElementName = "RoleValue")]
        [JsonProperty(PropertyName = "RoleValue")]
        public String Role { get; set; }

        [XmlIgnore]
        [JsonIgnore]
        public int UniformNumber { get; set; }

        public GroupMember()
        {

        }

        internal GroupMember(GroupMemberFromGet groupMemberFromGet)
        {
            PersonId = groupMemberFromGet.PersonID;
            IsActive = groupMemberFromGet.Active;

            DateTime date;
            if(DateTime.TryParse(groupMemberFromGet.DateInactive, out date)) { DateInactive = date; }

            GroupId = groupMemberFromGet.GroupID;
            MemberNotes = groupMemberFromGet.MemberNotes;
            Person = groupMemberFromGet.PersonInformation;
            PersonLink = groupMemberFromGet.PersonLink;
            RoleId = groupMemberFromGet.RoleID;
            Role = groupMemberFromGet.RoleValue;
            UniformNumber = groupMemberFromGet.UniformNumber;
        }

        internal class GroupMemberCollection
        {
            public List<GroupMember> Members { get; set; }
        }
    }
}
