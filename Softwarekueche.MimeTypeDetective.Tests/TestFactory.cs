using System;
using System.IO;

namespace Softwarekueche.MimeTypeDetective.Tests
{
    static class TestFactory
    {
        public static Uri ToUri(this string fileName)
        {
            return new FileInfo(fileName).ToUri();
        }
    }
}