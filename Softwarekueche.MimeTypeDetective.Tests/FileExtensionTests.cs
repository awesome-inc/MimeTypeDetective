using NUnit.Framework;
using FluentAssertions;

namespace Softwarekueche.MimeTypeDetective.Tests
{
    [TestFixture]
    class FileExtensionTests
    {
        [Test]
        [TestCase(@"data\Textdokument.txt", "text/plain")]
        [TestCase(@"data\Notify.wav", "audio/wav")]
        [TestCase(@"data\Overture.mp3", "audio/mpeg3")]
        [TestCase(@"data\Unknowndokument.thisisnotanextension", "unknown/unknown")]
        public void TryFilesWhichWorkWithUrlmon(string filename, string mimeType)
        {
            var uri = filename.ToUri();
            var mime = uri.GetMimeType();
            mime.Should().Be(mimeType);
        }
    }
}
