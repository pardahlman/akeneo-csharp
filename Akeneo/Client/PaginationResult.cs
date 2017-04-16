using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Newtonsoft.Json;

namespace Akeneo.Client
{
	public class PaginationResult<TContent>
	{
		[JsonProperty("_links")]
		public Dictionary<string, PaginationLink> Links { get; set; }

		public int CurrentPage { get; set; }

		[JsonProperty("_embedded")]
		public EmbeddedItems<TContent> Embedded { get; set; }

		public static PaginationResult<TContent> Empty => new PaginationResult<TContent>
		{
			CurrentPage = -1,
			Embedded = new EmbeddedItems<TContent>(),
			Links = new Dictionary<string, PaginationLink>()
		};
	}

	public static class PaginationToListExtension
	{
		public static List<TItem> GetItems<TItem>(this PaginationResult<TItem> result)
		{
			return result?.Embedded?.Items.Select(item => item).ToList();
		}
	}

	public static class PaginationDictionaryExtensions
	{
		private static readonly string Next = "next";

		public static PaginationLink GetNext(this IDictionary<string, PaginationLink> links)
		{
			return links.ContainsKey(Next) ? links[Next] : null;
		}
	}


	[DebuggerDisplay("{Href}")]
	public class PaginationLink
	{
		public string Href { get; set; }
	}

	public class EmbeddedItems<TContent>
	{
		public List<TContent> Items { get; set; }
	}
}
