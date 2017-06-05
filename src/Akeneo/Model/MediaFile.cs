using System;
using System.Collections.Generic;
using Akeneo.Client;
using Newtonsoft.Json;

namespace Akeneo.Model
{
	public class MediaFile : ModelBase
	{
		/// <summary>
		/// Media file code
		/// </summary>
		public string Code { get; set; }

		/// <summary>
		/// Original filename of the media file
		/// </summary>
		public string OriginalFilename { get; set; }

		/// <summary>
		/// Mime type of the media file
		/// </summary>
		public string MimeType { get; set; }

		/// <summary>
		/// Size of the media file
		/// </summary>
		public int Size { get; set; }

		/// <summary>
		/// Extension of the media file
		/// </summary>
		public string Extension { get; set; }

		[JsonProperty("_links")]
		public Dictionary<string, PaginationLink> Links { get; set; }
	}

	public static class MediaFileExtension
	{
		private const string DownloadKey = "download";

		public static Uri GetDownloadUri(this MediaFile file)
		{
			var link = file.Links.ContainsKey(DownloadKey)
				? file.Links[DownloadKey]
				: throw new KeyNotFoundException($"Expected link collection to contain {DownloadKey}.");
			return new Uri(link.Href);
		}
	}
}
