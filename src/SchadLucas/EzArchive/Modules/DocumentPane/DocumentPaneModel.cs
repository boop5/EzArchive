using System;
using System.Collections.Generic;
using SchadLucas.EzArchive.Domain;
using SchadLucas.EzArchive.Storage;

namespace SchadLucas.EzArchive.Modules.DocumentPane
{
    public class DocumentPaneModel
    {
        private readonly EzDatabase _db;

        public DocumentPaneModel(EzDatabase db)
        {
            _db = db;
        }

        public IEnumerable<EzDocument> GetDocuments(Guid folderId)
        {
            return _db.QueryCollection<EzDocument, IEnumerable<EzDocument>>(c => c.Find(d => d.Folder == folderId));
        }

    }
}