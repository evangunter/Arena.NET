using Arena.NET.Helpers;
using Arena.NET.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Arena.NET.Repositories
{
    public class PersonRepository : Repository
    {

        public PersonRepository(ArenaAPI api) : base(api)
        {
            
        }

        public async Task<ArenaPostResult> InsertOrUpdate(Person person)
        {
            if (person.PersonId != default(int))
            {
                Action = String.Format("person/{0}/update", person.PersonId);
            }
            else
            {
                Action = "person/add";
            }

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, Action);
            String serializedContent = person.Serialize();
            request.Content = new StringContent(serializedContent, Encoding.UTF8, "application/xml");

            return await ExecutePost(request);
        }

        public async Task<List<Person>> Get(PersonListOptions options)
        {
            String parameters = options.ToString();
            if(String.IsNullOrWhiteSpace(parameters)) { throw new Exception("You must provide at least 1 option paramter to search by."); }
            
            Action = String.Format("json/person/list?fields=FirstName,LastName,Addresses,Emails,Phones,Birthdate,FamiliyID,FamilyName,FamiliyMemberRoleValue,Gender,PersonGUID,PersonID,MemberStatusValue{0}", options);
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, Action);

            PersonCollection people = await ExecuteGet<PersonCollection>(request);
            return people.Persons;
        }

        public async Task<Person> Get(int personId)
        {
            Action = String.Format("json/person/{0}?fields=FirstName,LastName,Addresses,Emails,Phones,Birthdate,FamiliyID,FamilyName,FamiliyMemberRoleValue,Gender,PersonGUID,PersonID,MemberStatusValue", personId);
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, Action);

            return await ExecuteGet<Person>(request);
        }
    }
}
