using System.Collections.Generic;
using System.IO;
using Akeneo.Model;
using Akeneo.Model.ProductValues;
using Akeneo.Serialization;
using Newtonsoft.Json;
using Xunit;

namespace Akeneo.Tests.Serialization
{
	public class ProductValueDataConverterTests
	{
		[Fact]
		public void Should_Deserialize_String_Values()
		{
			/* Setup */
			const string value = "Tshirt long sleeves";
			var jsonStr = $"\"{value}\"";
			var reader = new JsonTextReader(new StringReader(jsonStr));
			reader.Read();
			var serializer = new JsonSerializer();
			var converter = new ProductValueDataConverter();

			/* Test */
			var result = converter.ReadJson(reader, typeof(object), null, serializer);

			/* Assert */
			Assert.IsType<string>(result);
			Assert.Equal(value, result);
		}

		[Fact]
		public void Should_Deserialize_String_With_Number_To_Float()
		{
			/* Setup */
			const float value = 89.897f;
			var jsonStr = $"\"{value}\"";
			var reader = new JsonTextReader(new StringReader(jsonStr));
			reader.Read();
			var serializer = new JsonSerializer();
			var converter = new ProductValueDataConverter();

			/* Test */
			var result = converter.ReadJson(reader, typeof(object), null, serializer);

			/* Assert */
			Assert.IsType<float>(result);
			Assert.Equal(value, result);
		}

		[Fact]
		public void Should_Deserialize_Bool()
		{
			/* Setup */
			const bool value = true;
			var jsonStr = value.ToString().ToLower();
			var reader = new JsonTextReader(new StringReader(jsonStr));
			reader.Read();
			var serializer = new JsonSerializer();
			var converter = new ProductValueDataConverter();

			/* Test */
			var result = converter.ReadJson(reader, typeof(object), null, serializer);

			/* Assert */
			Assert.IsType<bool>(result);
			Assert.Equal(value, result);
		}

		[Fact]
		public void Should_Deserialize_Metric_Values_With_Decimals_Allowed()
		{
			/* Setup */
			const string value = "{\"amount\":\"-12.78\",\"unit\":\"KILOWATT\"}";
			var reader = new JsonTextReader(new StringReader(value));
			reader.Read();
			var serializer = new JsonSerializer();
			var converter = new ProductValueDataConverter();

			/* Test */
			var result = converter.ReadJson(reader, typeof(object), null, serializer);

			/* Assert */
			Assert.IsType<MetricProductValue>(result);
			Assert.Equal(((MetricProductValue)result).Amount, -12.78f);
			Assert.Equal(((MetricProductValue)result).Unit, PowerUnits.Kilowatt);
		}

		[Fact]
		public void Should_Deserialize_Metric_Values_With_Decimals_Not_Allowed()
		{
			/* Setup */
			const string value = "{\"amount\":13,\"unit\":\"KILOWATT\"}";
			var reader = new JsonTextReader(new StringReader(value));
			reader.Read();
			var serializer = new JsonSerializer();
			var converter = new ProductValueDataConverter();

			/* Test */
			var result = converter.ReadJson(reader, typeof(object), null, serializer);

			/* Assert */
			Assert.IsType<MetricProductValue>(result);
			Assert.Equal(((MetricProductValue)result).Amount, 13f);
			Assert.Equal(((MetricProductValue)result).Unit, PowerUnits.Kilowatt);
		}

		[Fact]
		public void Should_Deserialize_List_Of_Strings()
		{
			/* Setup */
			const string array = "[\"red\", \"black\", \"grey\"]";
			var reader = new JsonTextReader(new StringReader(array));
			reader.Read();
			var serializer = new JsonSerializer();
			var converter = new ProductValueDataConverter();

			/* Test */
			var result = converter.ReadJson(reader, typeof(object), null, serializer);

			/* Assert */
			if (result is List<string> deserliazed)
			{
				Assert.Equal(deserliazed[0], "red");
				Assert.Equal(deserliazed[1], "black");
				Assert.Equal(deserliazed[2], "grey");
			}
			else
			{
				Assert.True(false, $"Expected List<string> got {result.GetType()}");
			}
		}

		[Fact]
		public void Should_Deserialize_Array_Of_Price_Values()
		{
			/* Setup */
			const string array = "[{\"amount\":45,\"currency\":\"USD\"}, {\"amount\":56,\"currency\":\"EUR\"}]";
			var reader = new JsonTextReader(new StringReader(array));
			reader.Read();
			var serializer = new JsonSerializer();
			var converter = new ProductValueDataConverter();

			/* Test */
			var result = converter.ReadJson(reader, typeof(object), null, serializer);

			/* Assert */
			if (result is List<PriceProductValue> deserliazed)
			{
				Assert.Equal(deserliazed[0].Amount, 45d);
				Assert.Equal(deserliazed[0].Currency, Currency.USD);
				Assert.Equal(deserliazed[1].Amount, 56);
				Assert.Equal(deserliazed[1].Currency, Currency.EUR);
			}
			else
			{
				Assert.True(false, $"Expected List<PriceProductValue> got {result.GetType()}");
			}
		}
	}
}
