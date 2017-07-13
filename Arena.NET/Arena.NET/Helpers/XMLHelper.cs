using Arena.NET.Objects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Arena.NET.Helpers
{
    public static class XMLHelper
    {
        public static String Serialize<T>(this T value)
        {
            if (value == null)
            {
                return string.Empty;
            }
            try
            {
                var xmlserializer = new XmlSerializer(typeof(T));
                var stringWriter = new StringWriter();
                using (var writer = XmlWriter.Create(stringWriter))
                {
                    xmlserializer.Serialize(writer, value);
                    return stringWriter.ToString();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred", ex);
            }
        }

        /// <summary>
        /// Searlizes the Person Object to the XML object Arena requires - which is for some reason 
        /// different than what the API returns. What the hell, arena?
        /// </summary>
        /// <typeparam name="Person"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static String Serialize(this Person person)
        {
            XmlDocument personDocument = new XmlDocument();

            //add person
            XmlElement personElement = (XmlElement)personDocument.AppendChild(personDocument.CreateElement("Person"));

            //add addresses - use searlizer for this
            if (person.Addresses != null && person.Addresses.Count > 0)
            {
                String xmlAddresses = person.Addresses.Serialize();
                xmlAddresses = xmlAddresses.Replace("ArrayOfAddress", "Addresses");
                XmlNode addresses = personDocument.ImportNode(GetElement(xmlAddresses), true);

                personElement.AppendChild(addresses);
            }


            //name
            personElement.AppendChild(personDocument.CreateElement("FirstName")).InnerText = person.FirstName;
            personElement.AppendChild(personDocument.CreateElement("LastName")).InnerText = person.LastName;

            //gender
            personElement.AppendChild(personDocument.CreateElement("Gender")).InnerText = person.Gender;

            //email
            if(person.Emails != null && person.Emails.Count > 0)
            {
                //personElement.AppendChild(personDocument.CreateElement("FirstActiveEmail")).InnerText = person.Emails.First().Address;

                //XmlElement emailsElement = (XmlElement)personElement.AppendChild(personDocument.CreateElement("Emails"));

                //foreach(var email in person.Emails)
                //{
                //    emailsElement.AppendChild(personDocument.CreateElement("Address")).InnerText = email.Address;
                //}

                String xmlEmails = person.Emails.Serialize();
                xmlEmails = xmlEmails.Replace("ArrayOfEmail", "Emails");
                XmlNode emails = personDocument.ImportNode(GetElement(xmlEmails), true);

                personElement.AppendChild(emails);
            }

            //birthdate
            if (person.BirthDate != default(DateTime)) { personElement.AppendChild(personDocument.CreateElement("BirthDate")).InnerText = person.BirthDate.ToString(); }

            if(person.Phones != null && person.Phones.Count > 0)
            {

                ////cell phone
                //var cell = person.Phones.FirstOrDefault(x => x.PhoneType.ToLower().Contains("cell"));
                //if (cell != null) { personElement.AppendChild(personDocument.CreateElement("CellPhone")).InnerText = cell.Number; }

                ////business phone
                //var business = person.Phones.FirstOrDefault(x => x.PhoneType.ToLower().Contains("business"));
                //if (business != null) { personElement.AppendChild(personDocument.CreateElement("BusinessPhone")).InnerText = business.Number; }

                ////home phone
                //var home = person.Phones.FirstOrDefault(x => x.PhoneType.ToLower().Contains("home"));
                //if (home != null) { personElement.AppendChild(personDocument.CreateElement("HomePhone")).InnerText = home.Number; }

                String xmlPhones = person.Phones.Serialize();
                xmlPhones = xmlPhones.Replace("ArrayOfPhone", "Phones");
                XmlNode phones = personDocument.ImportNode(GetElement(xmlPhones), true);

                personElement.AppendChild(phones);


            }

            //identifiers
            personElement.AppendChild(personDocument.CreateElement("PersonID")).InnerText = person.PersonId.ToString();
            personElement.AppendChild(personDocument.CreateElement("PersonGUID")).InnerText = person.PersonIdentifier.ToString();
            personElement.AppendChild(personDocument.CreateElement("FamilyID")).InnerText = person.FamilyId.ToString();
            personElement.AppendChild(personDocument.CreateElement("MemberStatusValue")).InnerText = person.MemberStatus;
            personElement.AppendChild(personDocument.CreateElement("FamilyMemberRoleValue")).InnerText = person.FamilyMemberRole;

            return personDocument.OuterXml;
        }

        private static XmlElement GetElement(string xml)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            return doc.DocumentElement;
        }
    }
}
