using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arena.NET.Objects
{
    internal class FamilyMembersFromGet
    {
        public int FamilyID { get; set; }
        public string FamilyName { get; set; }
        public List<Person> FamilyMembers { get; set; }
    }
}
