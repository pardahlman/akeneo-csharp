# Filter and Search

Akeneo PIM provides endpoints to filter and search through products and locales. In the official documentation, it is refered to simply as [Filter](https://api.akeneo.com/documentation/filter.html), but the Akeneo.NET client differentiates between the search query (with its own syntax) and the general filtering.

## Search Criterias

A search is performed by supplying one or more `Criteria`. There are [different types of criterias](https://api.akeneo.com/documentation/filter.html#filter-on-product-properties), and they are all represented by a derived class. For example, search criterias for a category is created by the `Category` criteria (found in `Akeneo.Search` namespace). Each criteria has an operator (`string`) and a value (`object`). The class `Operators` contains a list of all known operators and can be used to supply values there.

```csharp
var categoryCriteria = new Category
{
	Operator = Operators.NotIn,
	Value = new[] { "summer_sale" }
};
```

Depending on what kind of criteria that is used, different operations are valid. Each criteria has static members that represent valid operations, and expected value types.

```csharp
var response = await Client.SearchAsync<Product>(new List<Criteria>
{
	Category.NotIn("summer_sale"),
	Family.In("sports_shoe"),
	Status.Disabled(),
	Created.Between(DateTime.Today.AddDays(-7), DateTime.Today)
});
```

The response is a `PaginationResult`.

### Multiple criterias of same type

It is possible to use the same type of criteria multiple multiple times to search for products. For example: to  find shoes that are not on summer sale, the following search criterias can be used

```csharp
var response = await Client.SearchAsync<Product>(new List<Criteria>
{
	Category.NotIn("summer_sale"),
	Category.In("shoes")
});
```

This results in the following search query

```
?search={"categories":[{"operator":"NOT IN","value":["summer_sale"]},{"operator":"IN","value":["shoes"]}]}
```

## Filter products

The client also provides a no-magic method for filtering products. `FilterAsync` is invoked with a query string argument (`string`) that is appended on the resource endpoint (derived from the provided generic type). To filter products via `description` attribute (similar to [this example](https://api.akeneo.com/documentation/filter.html#via-attributes)) simply perform the call.

```csharp
var response = await Client.FilterAsync<Product>("?attributes=description");
```

In fact, `SearchAsync` internally calls this end point to perform the searchd with a query parameter created from the provided criterias.