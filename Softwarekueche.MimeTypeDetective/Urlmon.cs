using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;

namespace Softwarekueche.MimeTypeDetective
{
    public class Urlmon : IMimeTypeResolver
    {
        [DllImport(@"urlmon.dll", CharSet = CharSet.Auto)]
        private extern static UInt32 FindMimeFromData(
            UInt32 pBC,
            [MarshalAs(UnmanagedType.LPStr)] String pwzUrl,
            [MarshalAs(UnmanagedType.LPArray)] byte[] pBuffer,
            UInt32 cbSize,
            [MarshalAs(UnmanagedType.LPStr)] String pwzMimeProposed,
            UInt32 dwMimeFlags,
            out UInt32 ppwzMimeOut,
            UInt32 dwReserverd
        );

        public string GetMimeTypeFor(Uri uri)
        {
            var fileInfo = new FileInfo(uri.LocalPath);
            if (!fileInfo.Exists) throw new FileNotFoundException(fileInfo.FullName + " not found");

            var buffer = new byte[256];
            using (var fs = new FileStream(fileInfo.FullName, FileMode.Open))
            {
                if (fs.Length >= 256)
                    fs.Read(buffer, 0, 256);
                else
                    fs.Read(buffer, 0, (int)fs.Length);
            }

            try
            {
                UInt32 mimetype;
                FindMimeFromData(0, null, buffer, 256, null, 0, out mimetype, 0);

                var mimeTypePtr = new IntPtr(mimetype);
                string mime = Marshal.PtrToStringUni(mimeTypePtr);
                Marshal.FreeCoTaskMem(mimeTypePtr);
                return mime;
            }
            catch (Exception ex)
            {
                Trace.WriteLine(string.Format("cannot find mime type for file '{0}' because '{1}'", fileInfo.FullName, ex.Message));
                return UriExtensions.UnresolvedMimeType;
            }
        }
    }
}
