# Attributes

This section contains information on how to work with the different types of attributes available.

## Derived attribute types

There are multiple types of attributes in Akeneo PIM, each with a slightly different set of properties. in the Akeneo.NET client, each attribute type is represented by a specialized class, deriving from the abstract class `AttribteBase`. This simplifies things, as all valid properties and their corresponding types are present for each attribute type.

### Attribute types

A complete list of valid attributes can be found at [Akeneo's documentation](https://docs.akeneo.com/1.7/cookbook/rule/general_information_on_rule_format.html#attribute-types). All the attribute classes are located in the namespace `Akeneo.Model.Attributes`. In the same namespace is also `AttributeType` contains a list [that hold the list of attribute types](https://github.com/pardahlman/akeneo-csharp/blob/master/Akeneo/Model/Attributes/AttributeType.cs).

### Example

For example, the `NumberAttribute` has `NumberMin`, `NumberMax`, `DecimalsAllowed` and `NegativeAllowed` properties that is specific to it.

```csharp
var shoeSize = new NumberAttribute
{
	Code = "shoe_size",
	DecimalsAllowed = true,
	NegativeAllowed = false,
	NumberMin = 6,
	NumberMax = 15,
	AvailableLocales = new List<string> { Locales.EnglishUs }
}
```

## Attribute features

### Metric Attribute

The metric attribute expects a _Metric Family_ and _Metric Unit_ to be supplied. In order to make this easier, classes for `MetricFamily` and corresponding units can be used

```csharp
var weigth = new MetricAttribute
{
	Code = "weigth",
	MetricFamily = MetricFamilies.Weight,
	DefaultMetricUnit = WeightUnits.Gram
};
```

For each metric family, there is a family specific units class. In the example above, the `Weight` family has a `WeightUnits` units class. In the same way, the `Temperature` family has a `TemperatureUnits` class etc.

## Retrieving attributes

### Get one attribute
Attributes are queried for the API by it's unique identifier. In order to retrieve a `NumberAttribute` with the identifier `shoe_size`, the following request can be made

```csharp
var shoeSize = await Client.GetAsync<NumberAttribute>("shoe_size");
```

The generic argument tells the client what kind of attribute it is, and the client uses that class when trying to deserialize it. If the type of the attribute is unknown, the `GenericAttribute` can be used. It holds all know attribute properties, and thus will serialize all properties from the API.

### Get multiple attributes

Often, the PIM has multiple attributes of different typs. In those scenarios, it is not feasable to query after multiple attributes using only one target type. By using the attribute base class `AttributeBase` the client will look at the retrieved attribute's `type` property and serialize it to corresponding type.

```csharp
var paginatedResult = await Client.GetManyAsync<AttributeBase>();
var attributes = paginatedResult.Embedded.Items;
var numberAttributes = attributes.OfType<NumberAttribute>();
var shoeSize = numberAttributes.FirstOrDefault(a => a.Code == "shoe_size");
```