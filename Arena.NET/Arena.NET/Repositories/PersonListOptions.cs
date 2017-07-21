using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arena.NET.Repositories
{
    public class PersonListOptions
    {
        public String Email { get; set; }

        public DateTime BirthDate { get; set; }

        public int PersonId { get; set; }

        public String FirstName { get; set; }

        public String LastName { get; set; }

        public String PhoneNumber { get; set; }

        public int FamilyId { get; set; }


        //add the values with data to the search parameters
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            if(!String.IsNullOrWhiteSpace(Email)) { builder.Append(String.Format("&email={0}", Email)); }

            if (!String.IsNullOrWhiteSpace(FirstName)) { builder.Append(String.Format("&firstName={0}", FirstName)); }

            if (!String.IsNullOrWhiteSpace(LastName)) { builder.Append(String.Format("&lastName={0}", LastName)); }

            if (!String.IsNullOrWhiteSpace(PhoneNumber)) { builder.Append(String.Format("&phone={0}", PhoneNumber)); }

            if(PersonId != default(int)) { builder.Append(String.Format("&personid={0}", PersonId)); }

            if (FamilyId != default(int)) { builder.Append(String.Format("&familyid={0}", FamilyId)); }

            if (BirthDate != default(DateTime)) { builder.Append(String.Format("&birthdate={0}", BirthDate.ToShortDateString())); }

            return builder.ToString();
        }
    }
}
