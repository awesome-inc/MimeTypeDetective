using System;
using System.Linq;
using System.Net;

namespace Softwarekueche.MimeTypeDetective
{
    public class MimeTypeByResponseHeader : IMimeTypeResolver
    {
        private readonly Func<Uri, WebRequest> _createRequest;
        public TimeSpan TimeOut { get; set; }

        public MimeTypeByResponseHeader(Func<Uri, WebRequest> createRequest = null)
        {
            _createRequest = createRequest ?? WebRequest.Create;
            TimeOut = TimeSpan.FromSeconds(15);
        }

        public string GetMimeTypeFor(Uri uri)
        {
            var request = _createRequest(uri);

            request.Method = WebRequestMethods.Http.Head;

            if (TimeOut > TimeSpan.Zero)
                request.Timeout = (int) TimeOut.TotalMilliseconds;

            using (var response = request.GetResponse())
            {
                var contentType = response.ContentType;
                // strip of "; charset..."
                return contentType.Split(';').First();
            }
        }
    }
}