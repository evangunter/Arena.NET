# Arena.NET
This is a C# based wrapper for the Arena API by Shelby Systems. 

**Arena.NET Services**

[Introduction](#introduction--support)  
[Getting Started](#getting-started)
[Group](#group)  
[Person](#person)  

### Introduction / Support
This wrapper was built and intended to speed up development when consuming the Arena API. Hopefully, other C# developers will begin to add more repositories that correspond to different endpoints. I have tried to abstract in the best way I know, but I encourage developers to look over my solutions and make them better. 

You will notice my frustrations with Arena API throughout the code. My main concern is that in most cases when dealing with a specific repository, the XML returned when getting information does not correspond to the XML POSTed with attempting to update or add information. Not only are the properties different, but in most cases the general format is different. Throughout this process I have ran into numerous issues due to the porr API documentation and in some instances the documentation just being wrong - which resulted in countless wasted hours. Overall, this makes maintaining object consistency a bit a pain. For the time being, I am writing custom Serializers for specific objects. Again, feel free to make adjustments.


Getting Started
--------
*There is currently no nuget package as this version is not stable*

Once you have created your API credentials within Arena, you will need to provide those keys and a few other things to get started:


Provide Arena.net with your api key - there are a couple of ways to do this:

a) In your application initialization, declare your credentials and create an instance of the Arena API:

```csharp
//get settings
String apiURL = ConfigurationManager.AppSettings["Arena_APIUrl"];
String username = ConfigurationManager.AppSettings["Arena_Username"];
String password = ConfigurationManager.AppSettings["Arena_Password"];
String apiKey = ConfigurationManager.AppSettings["Arena_APIKey"];
String apiSecretKey = ConfigurationManager.AppSettings["Arena_APISecret"];

var credentials = new Credentials(username, password, apiKey, apiSecretKey);

//configure our API settings
ArenaAPI api = new ArenaAPI(apiURL, credentials);
```

b) Create a session with API:

```csharp
await api.GetSessionAsync();
```

c) Use the repositories to create, retrieve or update objects:

```csharp
int id = "<someValidPersonId>";
PersonRepository repository = new PersonRepository(api);
Person person = await repository.Get(id);
```
Person
--------
### Retrieving a Person

Once you have started an session, you can begin using the repositories. The repositories consist of both GET and POST requests on the backend. When executing a POST request, the repository will always return an ArenaPostResult notifying the user of a successful request or not. If the latter, the ArenaPostResult object will tell you the error message.

To get a single person when knowing the personId (int)

```csharp
int id = "<someValidPersonId>";
PersonRepository repository = new PersonRepository(api);
Person person = await repository.Get(id);
```
*Currently, as get request return an object and throw an exception when the request fails, it would probably be a good idea to wrap this in a try catch. In the long run, I'd ideally like to change how this works.*

### Retrieving Multiple People

If you need to query/search for people in your database, you MUST search using at least 1 of 7 parameters define in the object PersonListOptions. Simply create a new instance of this object, with at least one property set, and pass that object into the overloaded Get method on the PersonRepository.

```csharp
PersonListOptions options = new PersonListOptions { Email = "evan.gunter@necchurch.org" };
PersonRepository repository = new PersonRepository(api);
List<Person> persons = await repository.Get(options);
```

### Creating a Person

Creating a person uses the same PersonRepository, but is a POST request so it will always return an ArenaPostResult object. Below is outlined all current possible fields you can attach to a person and save in Arena. Finding specific formats was a bit of a pain and this may have been the first time I've encountered an API where object properties had to be in a specific order to save properly. These are currently all properties I've tested and will save. 
*NOTE: I'm faily certain most ID's are unique to your own system. It's likely the entire POST request will fail if you attempt to add a record with an ID that doesn't exist in the lookups portion of Arena*

```csharp
//PhoneTypeId is required in order for the phone to save properly
//I can't remember if PostalCode requires a 9 digit code or a 5 digit will work fine
Phone cell = new Phone { Number = "1234567890", PhoneTypeId = 276 };
Address address = new Address { Address1 = "1234 some street", City = "Louisville", State = "KY", PostalCode = "402291485" };
Email mainEmail = new Email { Address = "myfakeEmail@gmail.com" };

Person newPerson = new Person();
newPerson.Emails = new List<Email> { mainEmail }; // assign emails
newPerson.Addresses = new List<Address> { address }; // assign addresses
newPerson.Phones = new List<Phone> { cell }; //assign phones
newPerson.FamilyId = 25812; //if 0, will create a new family
newPerson.MedicalInformation = "I have an allergy";
newPerson.MemberStatusId = 11961; //if 0 or not specified, will result in unknown
newPerson.FamilyMemberRoleId = 29; //if 0 or not specified, will result in unknown
newPerson.CampusId = 1; //if 0 or not specified, will result in null I think 
newPerson.FirstName = "John";
newPerson.LastName = "Gunter";
newPerson.Gender = "Male"; //I haven't tested all variations of Gender (M, F, Male, Female, Unkown, etc...), but Male for sure works
newPerson.BirthDate = DateTime.Now.AddYears(-11);
newPerson.PersonIdentifier = Guid.NewGuid(); // I'm honestly not sure if this saves, or how this would be useful because the API using Ids (ints)

ArenaPostResult result = await repository.InsertOrUpdate(newPerson);
```
### Updating a Person

Fundamentally, this should work the exact same way as updating a person, except you need to specify PersonId. However, this is untested.

```csharp
newPerson.PersonId = 27 // some valid id
ArenaPostResult result = await repository.InsertOrUpdate(newPerson);;
```

Group
--------
### Getting Group Members

Getting group members assumes you know and have a valid group id (int).

```csharp
int id = "<someValidGroupId>";
GroupRepository repository = new GroupRepository(api);
List<GroupMember> members = await repository.Get(id);
```
### Adding Group Members

Adding a group member not only requires the group id (int) and the person id (int) you want to add to the group, but you must also supply the api with new GroupMember object requiring a roleId (int) and an IsActive (boolean). There are other properties on this object you may supply, but these are required. This is a POST method so the return object will always be an ArenaPostResult.

```csharp
int groupId = "<someValidGroupId>";
int personId = "<someValidPersonId>";
GroupRepository repository = new GroupRepository(api);
ArenaPostResult result = await repository.Insert(groupId, personId, new GroupMember { IsActive = true, RoleId = 24 });
```
### Updating Group Members

Updating group members works essentially the same as adding a group member. However, the person needs to already exist in the group and the only thing you can update is properties on the GroupMember object - in this case, I'm inactivating a this person from the group.

```csharp
int groupId = "<someValidGroupId>";
int personId = "<someValidPersonId>";
GroupRepository repository = new GroupRepository(api);
ArenaPostResult result = await repository.Update(groupId, personId, new GroupMember { IsActive = false, RoleId = 24 });
```



