using System.IO;

namespace Softwarekueche.MimeTypeDetective
{
    public static class FileInfoExtension
    {
        public const string UnresolvedMimeType= "unknown/unknown";

        public static string GetMimeType(this FileInfo file)
        {
            // lets check the file extension
            var mime = new MimeTypeByExtension().GetMimeTypeFor(file);
            if (mime != UnresolvedMimeType) return mime;

            // use system api as fallback
            return new Urlmon().GetMimeTypeFor(file);
        }
    }
}
