# Akeneo .NET Client

[![Build Status](https://img.shields.io/appveyor/ci/pardahlman/akeneo-csharp.svg?style=flat-square)](https://ci.appveyor.com/project/pardahlman/akeneo-csharp) [![NuGet](https://img.shields.io/nuget/v/Akeneo.NET.svg?style=flat-square)](https://www.nuget.org/packages/Akeneo.NET) [![GitHub release](https://img.shields.io/github/release/pardahlman/akeneo-csharp.svg?style=flat-square)](https://github.com/pardahlman/akeneo-csharp/releases/latest) [![Documentation Status](https://readthedocs.org/projects/akeneonet/badge/?version=latest&style=flat-square)](http://akeneonet.readthedocs.io)

> .NET Client to consume [Akeneo PIM](https://www.akeneo.com/)'s [RESTful API](https://api.akeneo.com/).

## Overview

* Runs on .NET Core and .NET Framework
* Create, update and delete resources from the API
* Upload and download media files
* Search and filter products
* Strongly typed attributes, search criterias and more
* Convenience classes for creating product values

 :blue_book: Full documentation at [`akeneonet.readthedocs.io`](http://akeneonet.readthedocs.io/en/latest/).

## Getting started

### Install NuGet package

[This package](https://www.nuget.org/packages/Akeneo.NET/) is available as a release on nuget.org.

```nuget
PM> Install-Package Akeneo.NET 
```

### Create OAuth Client and Secret

Follow [the official instructions](https://api.akeneo.com/documentation/security.html#create-an-oauth-client) to create client id and client secret

```bash
php app/console pim:oauth-server:create-client \
        --grant_type="password" \
        --grant_type="refresh_token"
```

### Create .NET client

Create an instance of the client by providing the URL to Akeneo PIM together with client id/secret and user name and password. The client will request access token and refresh token when needed.

```csharp
var client = new AkeneoClient(new ClientOptions
{
    ApiEndpoint = new Uri("http://localhost:8080"),
    ClientId = "1_3qwnpneuey80o080g0gco84ow4gsoo88skc880ssckgcg0okkg",
    ClientSecret = "3aw5l2xnvugwg0kc800g4k8s4coo80kkkc8ccs0so08gg08oc8",
    UserName = "admin",
    Password = "admdin"
});
```

### Create, delete and update

That's it! Use the client's generic methods to create, get, update and remove [Attributes](https://api.akeneo.com/api-reference.html#Attributes), [Attribute Options](https://api.akeneo.com/api-reference.html#Attributeoptions), [Families](https://api.akeneo.com/api-reference.html#Families), [Categories](https://api.akeneo.com/api-reference.html#Categories) and [Products](https://api.akeneo.com/api-reference.html#Products).

**Note:** There are some endpoints that are not implemented in the current version (1.7.3) of Akeneo PIM. For example, only products can be removed.

Programmatically define a product and specify its categories, values etc

```csharp
var product = new Product
{
    Identifier = "nike_air",
    Categories = new List<string>
    {
        Category.Shoes,
        Category.Sport,
        Category.Fashion
    },
    Family = Family.Shoes,
    Values = new Dictionary<string, List<ProductValue>>
    {
        {
            "shoe_size", new List<ProductValue>
            {
                new ProductValue {Locale = Locales.EnglishUs, Data = "10"},
                new ProductValue {Locale = Locales.SwedenSwedish, Data = "42"},
            }
        },
        {
            "name", new List<ProductValue>
            {
                new ProductValue {Data = "Nike Air"}
            }
        }
    }
};
```

Add it to the PIM

```csharp
var response = await Client.CreateAsync(product);
if (response.Code != HttpStatusCode.Created)
{
    _logger.Information(
        "Endpoint returned {statusCode}. Message: {message}, Errors: {@errors}",
        response.Code, response.Message, response.Errors
    );
})
```

Update it

```csharp
product.Enabled = true;
var response = await Client.UpdateAsync(product);
```

Remove it 

```csharp
var response = await Client.DeleteAsync<Product>(product.Identifier);
```
