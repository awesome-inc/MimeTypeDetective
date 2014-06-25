using System;
using System.Linq;
using System.Net;

namespace Softwarekueche.MimeTypeDetective
{
    public class MimeTypeByResponseHeader : IMimeTypeResolver
    {
        public string GetMimeTypeFor(Uri uri)
        {
            var request = WebRequest.Create(uri);
            using (var response = request.GetResponse())
            {
                var contentType = response.ContentType;
                // strip of "; charset..."
                return contentType.Split(';').First();
            }
        }
    }
}