using System;
using System.IO;

namespace Softwarekueche.MimeTypeDetective
{
    public static class UriExtensions
    {
        public const string UnresolvedMimeType = "unknown/unknown";

        public static string GetMimeType(this Uri uri)
        {
            if (uri.IsFile)
            {
                // lets check the file extension
                var mime = new MimeTypeByExtension().GetMimeTypeFor(uri);
                if (mime != UnresolvedMimeType) 
                    return mime;

                // use system api as fallback
                return new Urlmon().GetMimeTypeFor(uri);
            }

            return new MimeTypeByResponseHeader().GetMimeTypeFor(uri);
        }

        public static Uri ToUri(this FileInfo fileInfo)
        {
            return new Uri(fileInfo.FullName);
        }
    }
}
