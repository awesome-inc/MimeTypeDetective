using System;
using System.IO;

namespace Softwarekueche.MimeTypeDetective
{
    public static class UriExtensions
    {
        public const string UnresolvedMimeType = "unknown/unknown";

        public static string GetMimeType(this Uri uri)
        {
            return uri.IsFile 
                ? new MimeTypeByExtension().GetMimeTypeFor(uri)
                : new MimeTypeByResponseHeader().GetMimeTypeFor(uri);
        }

        public static Uri ToUri(this FileInfo fileInfo)
        {
            return new Uri(fileInfo.FullName);
        }
    }
}
