using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace Softwarekueche.MimeTypeDetective.Tests
{
    [TestFixture]
    public class MimeTypeByExtensionTests
    {
        [Test]
        [TestCase(@"data\Textdokument.txt", "text/plain")]
        [TestCase(@"data\Notify.wav", "audio/wav")]
        [TestCase(@"data\Overture.mp3", "audio/mpeg3")]
        public void TryFilesWhichWorkWithUrlmon(string filename, string mimetype)
        {
            var uri = filename.ToUri();
            var actual = new MimeTypeByExtension().GetMimeTypeFor(uri);
            actual.Should().Be(mimetype);
        }

        [Test]
        [TestCase(@"data\Unknowndokument.thisisnotanextension", "thisisnotanextension")]
        public void TryFilesWhichWontWorkWithUrlmon(string filename, string mimetype)
        {
            var uri = filename.ToUri();
            var actual = new MimeTypeByExtension().GetMimeTypeFor(uri);
            actual.Should().NotBe(mimetype);
            actual.Should().Be(UriExtensions.UnresolvedMimeType);
        }

        [Test]
        public void TestRegisterCustomExtension()
        {
            MimeTypeByExtension.IsRegistered("cs").Should().BeFalse();
            try
            {
                MimeTypeByExtension.Register("cs", "text/plain");
                MimeTypeByExtension.IsRegistered("cs").Should().BeTrue();
                new MimeTypeByExtension().GetMimeTypeFor(@"test\code.cs".ToUri()).Should().Be("text/plain");
            }
            finally { MimeTypeByExtension.UseDefaulMapping(); }
        }

        [Test]
        public void TestOverrideMimeMapping()
        {
            // cf.: http://stackoverflow.com/questions/208056/what-are-the-mime-types-for-net-project-source-code-files
            MimeTypeByExtension.IsRegistered("cs").Should().BeFalse();
            try
            {
                var myMapping = new Dictionary<string, string> { { "cs", "text/plain" } };
                MimeTypeByExtension.UseMimeMapping(myMapping);
                new MimeTypeByExtension().GetMimeTypeFor(@"test\code.cs".ToUri()).Should().Be("text/plain");
            }
            finally { MimeTypeByExtension.UseDefaulMapping(); }
        }

        [Test]
        [TestCase("audio/x-ms-wma", "wma")]
        [TestCase("audio/mpeg3", "mp3")]
        [TestCase("unknown/unknown", "")]
        [TestCase("application/octet-stream", "bin")]
        [TestCase("text/rtf", "rtf")]
        public void TestGuessDefaultFileExtension(string mimeType, string extension)
        {
            var sut = new MimeTypeByExtension();
            var actual = sut.GuessDefaultExtensionFor(mimeType);
            actual.Should().Be(extension);
        }

        [TestCase("application/octet-stream", "bin,dll,exe,dmg,dms,lha,lzh,so")]
        [TestCase("image/jpeg", "jpe,jpg,jpeg")]
        public void TestGetExtensions(string mimeType, string extensions)
        {
            var sut = new MimeTypeByExtension();
            var actual = sut.GetExtensionsFor(mimeType);
            var expected = extensions.Split(',');
            actual.Should().Contain(expected);
        }
    }
}