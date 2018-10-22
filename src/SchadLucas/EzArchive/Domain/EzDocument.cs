using System;
using SchadLucas.EzArchive.Storage;

namespace SchadLucas.EzArchive.Domain
{
    public class EzDocument : EzEntity
    {
        public string FileExtension { get; set; }
        public string FileName { get; set; }

        /// <summary>
        ///     Gets the FileSize in kb.
        /// </summary>
        public double FileSize { get; set; }

        public Guid Folder { get; set; }

        public string MimeType { get; set; }
    }
}