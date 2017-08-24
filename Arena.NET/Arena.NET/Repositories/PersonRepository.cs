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

        public async Task<ArenaPostResult> AddNote(int personId, PersonNote personNote)
        {
            Action = String.Format("person/{0}/note", personId);

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, Action);
            String serializedContent = personNote.Serialize();
            request.Content = new StringContent(serializedContent, Encoding.UTF8, "application/xml");

            return await ExecutePost(request);
        }

        public async Task<List<Person>> Get(PersonListOptions options)
        {
            String parameters = options.ToString();

            //The fields are not the same, and I have mapped all the information needed
            //String fields = "FirstName,LastName,Addresses,Emails,Phones,Birthdate,FamilyID,FamilyName,FamiliyMemberRoleValue,Gender,PersonGUID,PersonID,MemberStatusValue";
            if (String.IsNullOrWhiteSpace(parameters)) { throw new Exception("You must provide at least 1 option paramter to search by."); }
            
            //Action = String.Format("json/person/list?fields={0}{1}", fields, options);
            Action = String.Format("json/person/list?{0}", parameters.Remove(0, 1));
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, Action);

            PersonFromListCollection people = await ExecuteGet<PersonFromListCollection>(request);

            //map to person object for consitency
            List<Person> persons = new List<Person>();
            people.Persons.ForEach(delegate (PersonFromList person)
            {
                persons.Add(new Person(person));
            });

            return persons;
        }

        public async Task<List<Person>> GetFamily(int familyId)
        {
            //String fields = "FirstName,LastName,Addresses,Emails,Phones,Birthdate,FamilyID,FamilyName,FamiliyMemberRoleValue,Gender,PersonGUID,PersonID,MemberStatusValue";
            //Action = String.Format("json/person/{0}?fields={1}", personId, fields);
            Action = String.Format("json/family/{0}", familyId);
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, Action);

            FamilyMembersFromGet family = await ExecuteGet<FamilyMembersFromGet>(request);

            //map to person object for consitency
            List<Person> members = new List<Person>();
            family.FamilyMembers.ForEach(delegate (Person person)
            {
                person.FamilyId = family.FamilyID;
                person.FamilyName = family.FamilyName;
                members.Add(person);
            });

            return members;
        }

        public async Task<Person> Get(int personId)
        {
            //String fields = "FirstName,LastName,Addresses,Emails,Phones,Birthdate,FamilyID,FamilyName,FamiliyMemberRoleValue,Gender,PersonGUID,PersonID,MemberStatusValue";
            //Action = String.Format("json/person/{0}?fields={1}", personId, fields);
            Action = String.Format("json/person/{0}", personId);
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, Action);

            PersonFromGet person = await ExecuteGet<PersonFromGet>(request);

            return new Person(person);
        }
    }
}
