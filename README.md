# Arena.NET
This is a C# based wrapper for the Arena by Shelby Systems API endpoints. 

### Introduction / Support
This wrapper was built and intended to speed up development when consuming the Arena API. Hopefully, other C# developers will begin to add more repositories that correspond to different endpoints. I have tried to abstract in the best way I know, but I encourage developers to look over my solutions and make them better. 

You will notice my frustrations with Arena API throughout the code. My main concern is that in most cases when dealing with a specific repository, the XML returned when getting information does not correspond to the XML POSTed with attempting to update or add information. This makes maintaining object consistency a bit a pain. For the time being, I am writing custom Serializers for specific objects. Again, feel free to make adjustments.

### Getting Started
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
await repository.Get(id);
```


