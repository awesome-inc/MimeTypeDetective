using FluentAssertions;
using NUnit.Framework;

namespace Softwarekueche.MimeTypeDetective.Tests
{
    [TestFixture]
    public class FileExtensionTests
    {
        [Test]
        [TestCase(@"data\Textdokument.txt", "text/plain")]
        [TestCase(@"data\Notify.wav", "audio/wav")]
        [TestCase(@"data\Overture.mp3", "audio/mpeg3")]
        [TestCase(@"data\Unknowndokument.thisisnotanextension", "application/octet-stream")]
        public void TryFilesWhichWorkWithUrlmon(string filename, string mimetype)
        {
            var uri = filename.ToUri();
            var mime = uri.GetMimeType();
            mime.Should().Be(mimetype);
        }
    }
}
