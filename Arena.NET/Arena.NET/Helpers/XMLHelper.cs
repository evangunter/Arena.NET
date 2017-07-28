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

        public static String Serialize(this GroupMember groupMember)
        {
            XmlDocument groupMemberDocument = new XmlDocument();

            //add group member
            XmlElement groupMemberElement = (XmlElement)groupMemberDocument.AppendChild(groupMemberDocument.CreateElement("GroupMember"));

            //Active
            groupMemberElement.AppendChild(groupMemberDocument.CreateElement("Active")).InnerText = (groupMember.IsActive) ? "true" : "false";

            groupMemberElement.AppendChild(groupMemberDocument.CreateElement("MemberNotes")).InnerText = groupMember.MemberNotes;

            if(groupMember.RoleId != default(int)) { groupMemberElement.AppendChild(groupMemberDocument.CreateElement("RoleID")).InnerText = groupMember.RoleId.ToString(); }

            if (groupMember.UniformNumber != default(int)) { groupMemberElement.AppendChild(groupMemberDocument.CreateElement("UniformNumber")).InnerText = groupMember.UniformNumber.ToString(); }

            return groupMemberDocument.OuterXml;
        }

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

            //birthdate
            if (person.BirthDate != default(DateTime)) { personElement.AppendChild(personDocument.CreateElement("BirthDate")).InnerText = person.BirthDate.ToString("s"); }

            //campusId
            if (person.CampusId != default(int)) { personElement.AppendChild(personDocument.CreateElement("CampusID")).InnerText = person.CampusId.ToString(); }

            //email
            if (person.Emails != null && person.Emails.Count > 0)
            {

                String xmlEmails = person.Emails.Serialize();
                xmlEmails = xmlEmails.Replace("ArrayOfEmail", "Emails");
                XmlNode emails = personDocument.ImportNode(GetElement(xmlEmails), true);

                personElement.AppendChild(emails);
            }

            //familyId
            if (person.FamilyId != default(int)) { personElement.AppendChild(personDocument.CreateElement("FamilyID")).InnerText = person.FamilyId.ToString(); }

            //familyMemberRoleId
            if (person.FamilyMemberRoleId != default(int)) { personElement.AppendChild(personDocument.CreateElement("FamilyMemberRoleID")).InnerText = person.FamilyMemberRoleId.ToString(); }

            //name
            personElement.AppendChild(personDocument.CreateElement("FirstName")).InnerText = person.FirstName;

            //gender
            personElement.AppendChild(personDocument.CreateElement("Gender")).InnerText = person.Gender;

            //last name
            personElement.AppendChild(personDocument.CreateElement("LastName")).InnerText = person.LastName;

            //medical information
            personElement.AppendChild(personDocument.CreateElement("MedicalInformation")).InnerText = person.MedicalInformation;

            //member status id
            if (person.MemberStatusId != default(int)) { personElement.AppendChild(personDocument.CreateElement("MemberStatusID")).InnerText = person.MemberStatusId.ToString(); }

            //identifiers
            personElement.AppendChild(personDocument.CreateElement("PersonGUID")).InnerText = person.PersonIdentifier.ToString();
            personElement.AppendChild(personDocument.CreateElement("PersonID")).InnerText = person.PersonId.ToString();

            if (person.Phones != null && person.Phones.Count > 0)
            {
                String xmlPhones = person.Phones.Serialize();
                xmlPhones = xmlPhones.Replace("ArrayOfPhone", "Phones");
                XmlNode phones = personDocument.ImportNode(GetElement(xmlPhones), true);

                personElement.AppendChild(phones);
            }

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
