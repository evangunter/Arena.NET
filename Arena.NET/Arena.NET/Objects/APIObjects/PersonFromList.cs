using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arena.NET.Objects
{
    internal class PersonFromList
    {
        public List<Address> Addresses { get; set; }
        public int Age { get; set; }
        public string AreaName { get; set; }
        public DateTime BirthDate { get; set; }
        public string BlobLink { get; set; }
        public int CampusID { get; set; }
        public string CampusName { get; set; }
        public string CellPhone { get; set; }
        public int FamilyID { get; set; }
        public int FamilyMemberRoleID { get; set; }
        public string FamilyMemberRoleValue { get; set; }
        public string FirstActiveEmail { get; set; }
        public string FirstName { get; set; }
        public string Gender { get; set; }
        public string HomePhone { get; set; }
        public string LastName { get; set; }
        public int MemberStatusID { get; set; }
        public string MemberStatusValue { get; set; }
        public string NickName { get; set; }
        public string PersonGUID { get; set; }
        public int PersonID { get; set; }
        public string PersonLink { get; set; }
        public string RecordStatusValue { get; set; }
    }

    internal class PersonFromListCollection
    {
        public List<PersonFromList> Persons { get; set; }
    }
}
