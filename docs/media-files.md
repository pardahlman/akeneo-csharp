# Media Files

Media files are files associated with a specific attribute on a specific product. They can be uploaded and downloaded through Akeneo.NET

## Upload file

In order to upload a file, use the `FileUpload` class. In order for a successful upload, all properties must be set:

* Product Identifier: the identifier of the product
* Product Attribute: the attribute code for which the file should be associated with. The attribute should be a `FileAttribute` attribute
* File Path: The path to the image.

Below is an example of of a correctly created `FileUpload`

```csharp
var fileUpload = new MediaUpload
{
	Product =
	{
		Identifier = "nike_air",
		Attribute = "product_image_large"
	},
	FilePath = "C:\\images\\nike_air.png"
};
```

Upload the image by calling `UploadAsync`

```csharp
var response = await Client.UploadAsync(fileUpload);
```

The call returns an `AkeneoResponse` that hold information about the outcome of the operation.

## Download file

A file is downloaded through by supplying it's `mediaCode`. If the media file is recently uploaded, the media code can be retrieved from the links collection in the `AkeneoResponse`.

```csharp
var response = await Client.UploadAsync(fileUpload);
var mediaCode = response.Links[PaginationLinks.Location].Href;
```

Otherwise, it can be retrieved from the product values.

```csharp
var product = await Client.GetAsync<Product>("tyfon-tv-6m-0m-company");
var logoValue = product.Values["product_image_large"];
var mediaCode = logoValue.FirstOrDefault()?.Data as string;
```

Once the code is retrieved, it can be used as argument when calling `DownloadAsync`. The resource contains the original file name, as well as a `Stream` containing the image. There are extension methods for writing the file to disk.

```csharp
var file = await Client.DownloadAsync(mediaCode);
file.WriteToFile("C:\\downloads", file.FileName);
file.Dispose();
```

If a file name is not provided, the original file name will be used.