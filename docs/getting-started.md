# Getting Started

## Install Akeneo PIM

It is assumed that Akeneo PIM is installed. To install Akeneo PIM, follow the [official installation instructions](https://docs.akeneo.com/latest/developer_guide/installation/index.html) or use [this `docker-compose.yml`](https://github.com/pardahlman/docker-akeneo/blob/master/docker-compose.yml) file to get started on [Docker](https://www.docker.com/) with one simple command.

### Creating OAuth credentials

The calls to the API is authenticated via oauth. To create credentials follow [the official instructions](https://api.akeneo.com/documentation/security.html#authentication). Note, that if the `pardahlman/akene` docker image is used, a client is created during startup. Look through the containers logs and locate an entry similar to this

```bash
A new client has been added.
client_id: 1_27xlkd53wou8ogggwwwksk48s0sgsoogwkowws8wko88gcs0os
secret: 65rfqpc5a3okws0w4k0kgcwswwg0ggwg48wc40gcckso88sk44
  - Done.
```

## Install Akeneo.NET client

The .NET client is published on [NuGet](https://www.nuget.org/packages/Akeneo.NET/). To install it, search for `Akeneo.NET` in the NuGet user interface, or open the Package Manager Console and enter

```
PM> Install-Package Akeneo.NET
```

### Create client

The client is created with an `AkeneoOptions` options object. All fields are required in order to successfully connect to the PIM:

* `ApiEndpoint` is the URL to Akeneo PIM without any trailing slash
* `ClientId` is the OAuth client id (generated)
* `ClientSecret` is the OAuth client secret (generated)
* `UserName` is the name of a user in the PIM
* `Password` is the corresponding password the the user in the PIM

Below is an example of a complete options object.

```csharp
var options = new AkeneoOptions
{
	ApiEndpoint = new Uri("http://localhost:8080"),
	ClientId = "1_27xlkd53wou8ogggwwwksk48s0sgsoogwkowws8wko88gcs0os",
	ClientSecret = "65rfqpc5a3okws0w4k0kgcwswwg0ggwg48wc40gcckso88sk44",
	UserName = "admin",
	Password = "admin"
}
```

That's it! Create an instance of the client with the options object

```csharp
var client = new AkeneoClient(options);
```

## Using the API

### Asynchronious calls

All calls to the API is made from the `AkeneoClient`. The client implements asynchronious methods, that should be called using the `async`/`await` pattern. Each method has an [Cancellation Token](https://msdn.microsoft.com/en-us/library/system.threading.cancellationtoken(v=vs.110).aspx) as an optional argument. It can be provided to cancel an ongoing operation.

### Generic arguments

The API is uniform over most of it resources. This allows the client to use the same methods to query different resources. The desired resource type is provided as a generic argument to the calls. Below are some example calls using the same method but querying different resources.

```csharp
var argument = await Client.GetAsync<NumberAttribute>("shoe_sie");
var family = await Client.GetAsync<Family>("sports_shoe");
var category = await Client.GetAsync<Category>("women");
var product = await Client.GetAsync<NumberAttribute>("nike_air");
```

**Important!** Despite the fact that the API is uniform, it is not complete. As of the most current version (1.7.4) the only resource that can be deleted is `Product`. Trying to delete other resources will end up with a `405 Method Not Allowed`.

### Error handling

The client passes on any error messages from the API. Many of the methods returns either an `AkeneoResponse`, `AkeneoBatchResponse` or `PaginationResult`, each having a message property as well as property for the HTTP status code.

```csharp
var response = await Client.DeleteAsync<Product>("nike_air");
if (response.Code == HttpStatusCode.NoContent)
{
	Console.WriteLine("Successfully deleted product.");
}
else
{
	Console.WriteLine($"Unsuccessfully deleted product. Message: {response.Message}");
}
```