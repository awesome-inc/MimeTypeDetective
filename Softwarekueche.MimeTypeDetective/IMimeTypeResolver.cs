using System;

namespace Softwarekueche.MimeTypeDetective
{
    public interface IMimeTypeResolver
    {
        string GetMimeTypeFor(Uri uri);
    }
}