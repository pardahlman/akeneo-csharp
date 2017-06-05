# Products

The entity `Product` has some properties that references other resources. This documentation does not go into detail on the constraints of these properties. For more information, see the [API documentation](https://api.akeneo.com/api-reference.html#post_products).Below are some client specific information that can be useful when creating a product.

## Creating product values

Product values are created in a dictionary keyed with attribute code (`string`).

```csharp
var values = new Dictionary<string, object>
{
	{"shoe_size", new List<ProductValue>{ new ProductValue
	{
		Locale = Locales.EnglishUs,
		Scope = AkeneoDefaults.Channel,
		Data = 9
	}}}
};
```

Different attribute expects different data. For example, `Scope` and `Locale` should not be provided if the attribute does not have unique values for locales or channels. Depending on the attribute type, the `Data` payload is expected to be of certain types.

## Product values from attribute

It can be hairy to provide the correct product values. In order to make it easier, Akeneo.NET has a set of extension methods that can be used to create a product value based on an attribute. The extension methods are specific for each type, which can be helpful if unsure what kind of data to expect.

Below is an example where the product values are created from a set of attributes.

```csharp
// load known attributes
var price = await Client.GetAsync<PriceAttribute>("list_price");
var campaignPrice = await Client.GetAsync<PriceAttribute>("campaign_price");
var startOfSales = await Client.GetAsync<DateAttribute>("campaign_start_date");

// use extension method to create product values
var product = new Product
{
	Identifier = "nike_air",
	Family = "sports_shoe",
	Enabled = true,
	Categories = {"shoe", "sport", "women"},
	Values = DictionaryFactory.Create(
		price.CreateValue(99, Currency.USD),
		campaignPrice.CreateValue(79, Currency.USD),
		startOfSales.CreateValue(DateTime.Today.AddDays(7))
	)
};
```

The `DictionaryFactory` is a convenience class that creates a dictionary from the `KeyValuePairs` returned from the `CreateValue` calls.