# Updating Resources

Updating existing resources is a complex topic with a [whole section in the API docs](https://api.akeneo.com/documentation/update.html) describing update behaviour based on the entity types etc. It is recommended to read up on how this works before using the endpoint.


## Add or change values

One straight forward strategy to update an resource is to retrieve it from the API, modify it and the update it in the API. This is how it can be done:

```csharp
var product = await Client.GetAsync<Product>("nike_air");
product.Enabled = false;
await Client.UpdateAsync(product);
```

This approach has two drawbacks:

1. It requires one extra round-trip to the API to retrieve the product before updating it
2. It can not be used to remove certain property values (like `string` properties on the product).


## Delete values

In order to be able to remove the value of a `string` property, it needs to be passed to the API with a `null` value.

```csharp
var response = await Client.UpdateAsync<Product>("nike_air", new UpdateModel
{
	{"variant_group", null}
});
```

This approach requires no extra round-trip, but gives the caller greater greater responsibility.

## Technical information

It is how the API interprets `null` that adds complexity to the update query. A string property value is removed by passing in `null` as a value for that property. However, `null` is the default value for reference types and it is difficult to differ an "explicit null" from a "implicit null" without introducing some sort of complexity to the solution.