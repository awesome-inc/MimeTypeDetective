MimeTypeDetective
=================

Inspect file to gather the mime type.

`GetMimeType` is an extension method for the `FileInfo` class.

**Usage:**

    [Test()]
    [TestCase(@"data\Textdokument.txt", "text/plain")]
    [TestCase(@"data\Notify.wav", "audio/wav")]
    [TestCase(@"data\Overture.mp3", "audio/mpeg3")]
    [TestCase(@"data\Unknowndokument.thisisnotanextension", "unknown/unknown")]
    public void TryFilesWhichWorkWithUrlmon(string filename, string mimetype)
    {
        var fi = new FileInfo(filename);
        var mime = fi.GetMimeType();
        mime.Should().Be(mimetype);
    }
