using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arena.NET.Objects
{
    internal class PersonFromGet
    {
        public int ActiveMeter { get; set; }
        public List<Address> Addresses { get; set; }
        public int Age { get; set; }
        public DateTime AnniversaryDate { get; set; }
        public string AttributesLink { get; set; }
        public DateTime BirthDate { get; set; }
        public int BlobID { get; set; }
        public string BlobLink { get; set; }
        public int CampusID { get; set; }
        public string CampusName { get; set; }
        public bool ContributeIndividually { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public int DisplayNotesCount { get; set; }
        public bool EmailStatement { get; set; }
        public List<Email> Emails { get; set; }
        public int EnvelopeNumber { get; set; }
        public int FamilyID { get; set; }
        public string FamilyLink { get; set; }
        public int FamilyMemberRoleID { get; set; }
        public string FamilyMemberRoleValue { get; set; }
        public int FamilyMembersCount { get; set; }
        public string FamilyName { get; set; }
        public string FirstName { get; set; }
        public int ForeignKey { get; set; }
        public int ForeignKey2 { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public string GivingUnitID { get; set; }
        public int InactiveReasonID { get; set; }
        public string InactiveReasonValue { get; set; }
        public bool IncludeOnEnvelope { get; set; }
        public string LastName { get; set; }
        public int MaritalStatusID { get; set; }
        public string MaritalStatusValue { get; set; }
        public string MedicalInformation { get; set; }
        public int MemberStatusID { get; set; }
        public string MemberStatusValue { get; set; }
        public string MiddleName { get; set; }
        public string NickName { get; set; }
        public string Notes { get; set; }
        public string NotesLink { get; set; }
        public int OrganizationID { get; set; }
        public string PersonGUID { get; set; }
        public int PersonID { get; set; }
        public string PersonLink { get; set; }
        public List<Phone> Phones { get; set; }
        public bool PrintStatement { get; set; }
        public int RecordStatusID { get; set; }
        public string RecordStatusValue { get; set; }
        public string RegionName { get; set; }
        public int SuffixID { get; set; }
        public string SuffixValue { get; set; }
        public int TitleID { get; set; }
        public string TitleValue { get; set; }
    }
}
