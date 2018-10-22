using System.Collections.Generic;
using SchadLucas.EzArchive.Domain;
using SchadLucas.EzArchive.Storage;

namespace SchadLucas.EzArchive.Modules.NavigationPane
{
    public class NavigationPaneModel
    {
        private readonly EzDatabase _db;

        public NavigationPaneModel(EzDatabase db)
        {
            _db = db;
        }

        public IEnumerable<EzFolder> GetAllFolders()
        {
            return _db.QueryCollection<EzFolder, IEnumerable<EzFolder>>(c => c.FindAll());
        }
    }
}