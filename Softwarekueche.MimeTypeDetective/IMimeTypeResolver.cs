using System.IO;

namespace Softwarekueche.MimeTypeDetective
{
    public interface IMimeTypeResolver
    {
        string GetMimeTypeFor(FileInfo fileinfo);
    }
}