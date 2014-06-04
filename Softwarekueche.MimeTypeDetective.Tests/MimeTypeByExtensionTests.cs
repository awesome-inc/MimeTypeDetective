using System.IO;
using FluentAssertions;
using NUnit.Framework;

namespace Softwarekueche.MimeTypeDetective.Tests
{
    [TestFixture()]
    public class MimeTypeByExtensionTests
    {
        [Test()]
        [TestCase(@"data\Textdokument.txt", "text/plain")]
        [TestCase(@"data\Notify.wav", "audio/wav")]
        [TestCase(@"data\Overture.mp3", "audio/mpeg3")]
        public void TryFilesWhichWorkWithUrlmon(string filename, string mimetype)
        {
            var fi = new FileInfo(filename);

            var urlmonMtUrlmon = new MimeTypeByExtension().GetMimeTypeFor(fi);
            urlmonMtUrlmon.Should().Be(mimetype);
        }

        [Test()]
        [TestCase(@"data\Unknowndokument.thisisnotanextension", "thisisnotanextension")]
        public void TryFilesWhichWontWorkWithUrlmon(string filename, string mimetype)
        {
            var fi = new FileInfo(filename);

            var urlmonMtUrlmon = new MimeTypeByExtension().GetMimeTypeFor(fi);
            urlmonMtUrlmon.Should().NotBe(mimetype);
            urlmonMtUrlmon.Should().Be("application/octet-stream");
        }
    }
}