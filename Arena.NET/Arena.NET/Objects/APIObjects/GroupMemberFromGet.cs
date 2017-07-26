using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arena.NET.Objects.APIObjects
{
    internal class GroupMemberFromGet
    {
        public bool Active { get; set; }
        public String DateInactive { get; set; }
        public DateTime DateJoined { get; set; }
        public int GroupID { get; set; }
        public string MemberNotes { get; set; }
        public int PersonID { get; set; }
        public Person PersonInformation { get; set; }
        public string PersonLink { get; set; }
        public int RoleID { get; set; }
        public string RoleValue { get; set; }
        public int UniformNumber { get; set; }
    }

    internal class GroupMemberFromGetCollection
    {
        public List<GroupMemberFromGet> Members { get; set; }
    }
}
