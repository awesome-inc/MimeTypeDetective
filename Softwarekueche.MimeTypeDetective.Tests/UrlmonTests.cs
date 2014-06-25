using FluentAssertions;
using NUnit.Framework;

namespace Softwarekueche.MimeTypeDetective.Tests
{
    [TestFixture]
    public class UrlmonTests
    {
        [Test]
        [TestCase(@"data\Textdokument.txt", "text/plain")]
        [TestCase(@"data\Notify.wav", "audio/wav")]
        public void TryFilesWhichWorkWithUrlmon(string filename, string mimetype)
        {
            var uri = filename.ToUri();
            var urlmonMtUrlmon = new Urlmon().GetMimeTypeFor(uri);
            urlmonMtUrlmon.Should().Be(mimetype);
        }

        [Test]
        [TestCase(@"data\Overture.mp3", "audio/mpeg3")]
        [TestCase(@"data\Unknowndokument.thisisnotanextension", "")]
        public void TryFilesWhichWontWorkWithUrlmon(string filename, string mimetype)
        {
            var uri = filename.ToUri();
            var urlmonMtUrlmon = new Urlmon().GetMimeTypeFor(uri);
            urlmonMtUrlmon.Should().NotBe(mimetype);
            urlmonMtUrlmon.Should().Be("application/octet-stream");
        }
    }
}
