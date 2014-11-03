using System.Collections.Generic;

namespace Softwarekueche.MimeTypeDetective
{
    public interface IFileExtensionGuesser
    {
        string GuessDefaultExtensionFor(string mimeType);
        IEnumerable<string> GetExtensionsFor(string mimeType);
    }
}