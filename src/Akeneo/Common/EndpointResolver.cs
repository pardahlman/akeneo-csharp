using System;
using System.Collections.Concurrent;
using System.Reflection;
using Akeneo.Model;
using Akeneo.Model.Attributes;

namespace Akeneo.Common
{
	public class EndpointResolver
	{
		private static readonly Type AttributeBase = typeof(AttributeBase);
		private static readonly Type AttributeOption = typeof(AttributeOption);
		private static readonly Type Family = typeof(Family);
		private static readonly Type Category = typeof(Category);
		private static readonly Type Product = typeof(Product);
		private static readonly Type MediaFile = typeof(MediaFile);
		private static readonly Type Locale = typeof(Locale);
		private static readonly Type ProductModel = typeof(ProductModel);
		private static readonly Type FamilyVariant = typeof(FamilyVariant);

		private readonly ConcurrentDictionary<Type, string> _typeToEndpointCache;

		public EndpointResolver()
		{
			_typeToEndpointCache = new ConcurrentDictionary<Type, string>();
		}

		public string ForResourceType<TModel>(string parentCode = null) where TModel : ModelBase
		{
			var isOption = AttributeOption.GetTypeInfo().IsAssignableFrom(typeof(TModel));
			return isOption
				? $"{Endpoints.Attributes}/{parentCode}/options"
				: GetResourceEndpoint(typeof(TModel));
		}

		public string ForResource<TModel>(TModel existing) where TModel : ModelBase
		{
			var baseUri = GetResourceEndpoint(typeof(TModel));
			var product = existing as Product;
			if (product != null)
			{
				return $"{baseUri}/{product.Identifier}";
			}
			var attribute = existing as AttributeBase;
			if (attribute != null)
			{
				return $"{baseUri}/{attribute.Code}";
			}
			var option = existing as AttributeOption;
			if (option != null)
			{
				return $"{baseUri}/{option.Attribute}/option/{option.Code}";
			}
			var family = existing as Family;
			if (family != null)
			{
				return $"{baseUri}/{family.Code}";
			}
			var category= existing as Category;
			if (category != null)
			{
				return $"{baseUri}/{category.Code}";
			}
			return null;
		}

		public string ForResource<TModel>(params string[] code) where TModel : ModelBase
		{
			var formatterStr = GetResourceFormatString(typeof(TModel));
			return string.Format(formatterStr, code);
		}

		protected virtual string GetResourceFormatString(Type modelType)
		{
			var endpoint = GetResourceTypeFormatString(modelType);
			return AttributeOption.GetTypeInfo().IsAssignableFrom(modelType)
				? $"{endpoint}/{{1}}"
				: $"{endpoint}/{{0}}";
		}

		protected virtual string GetResourceTypeFormatString(Type modelType)
		{
			var endpoint = GetResourceEndpoint(modelType);
			return AttributeOption.GetTypeInfo().IsAssignableFrom(modelType)
				? $"{endpoint}/{{0}}/options"
				: $"{endpoint}";
		}

		protected virtual string GetResourceEndpoint(Type modelType)
		{
			return _typeToEndpointCache.GetOrAdd(modelType, type =>
			{
				if (Product.GetTypeInfo().IsAssignableFrom(type))
				{
					return Endpoints.Products;
				}
				if (AttributeBase.GetTypeInfo().IsAssignableFrom(type))
				{
					return Endpoints.Attributes;
				}
				if (Family.GetTypeInfo().IsAssignableFrom(type))
				{
					return Endpoints.Families;
				}
				if (Category.GetTypeInfo().IsAssignableFrom(type))
				{
					return Endpoints.Categories;
				}
				if (AttributeOption.GetTypeInfo().IsAssignableFrom(type))
				{
					return Endpoints.Attributes;
				}
				if (MediaFile.GetTypeInfo().IsAssignableFrom(type))
				{
					return Endpoints.MediaFiles;
				}
				if (Locale.GetTypeInfo().IsAssignableFrom(type))
				{
					return Endpoints.Locale;
				}
				if (ProductModel.GetTypeInfo().IsAssignableFrom(type))
				{
					return Endpoints.ProductModel;
				}
				if (FamilyVariant.GetTypeInfo().IsAssignableFrom(type))
				{
					return Endpoints.FamilyVariant;
				}
				throw new NotSupportedException($"Unable to find API endpoint for type {modelType.FullName}");
			});
		}

		public string ForPagination<TModel>(int page = 1, int limit = 10, bool withCount = false) where TModel : ModelBase
		{
			var baseUrl = ForResourceType<TModel>();
			return $"{baseUrl}?page={page}&limit={limit}&with_count={withCount.ToString().ToLower()}";
		}

		public string ForPagination<TModel>(string parentCode, int page = 1, int limit = 10, bool withCount = false) where TModel : ModelBase
		{
			var baseUrl = ForResourceType<TModel>(parentCode);
			return $"{baseUrl}?page={page}&limit={limit}&with_count={withCount.ToString().ToLower()}";
		}

		public string WithSearchAfter<TModel>(int limit = 100, string searchAfter = null) where TModel : ModelBase
		{
			var baseUrl = ForResourceType<TModel>();
		    return searchAfter == null
		        ? $"{baseUrl}?pagination_type=search_after&limit={limit}"
		        : $"{baseUrl}?pagination_type=search_after&limit={limit}&search_after={searchAfter}";
		}
	}
}
