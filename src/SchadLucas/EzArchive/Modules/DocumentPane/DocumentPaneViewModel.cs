using System.Linq;
using Caliburn.Micro;
using SchadLucas.EzArchive.Domain;
using SchadLucas.EzArchive.Domain.Messages;
using SchadLucas.Logging;
using SchadLucas.Utilities;

namespace SchadLucas.EzArchive.Modules.DocumentPane
{
    public class DocumentPaneViewModel : Screen, IHandle<Messages.FolderSelected>
    {
        private readonly IEzLogger _logger;
        private readonly DocumentPaneModel _model;
        private readonly IEventAggregator _eventAggregator;
        private EzDocument _selectedDocument;

        public DocumentPaneViewModel(IEzLogger logger, DocumentPaneModel model, IEventAggregator eventAggregator)
        {
            _logger = logger;
            _model = model;
            _eventAggregator = eventAggregator;
        }

        public BindableCollection<EzDocument> Documents { get; } = new BindableCollection<EzDocument>();

        public EzFolder Folder { get; private set; }

        public EzDocument SelectedDocument
        {
            get => _selectedDocument;
            set
            {
                if (!Equals(_selectedDocument, value))
                {
                    _selectedDocument = value;
                    NotifyOfPropertyChange();
                    OnDocumentSelected(_selectedDocument);
                }
            }
        }


        public void Handle(Messages.FolderSelected message)
        {
            AwaitableTask.Run(() =>
            {
                Folder = message.Folder;

                Documents.Clear();
                var documents = _model.GetDocuments(Folder.Id).OrderBy(d => d.FileName);
                Documents.AddRange(documents);
            });
        }

        private void OnDocumentSelected(EzDocument selectedDocument)
        {
            _eventAggregator.PublishOnBackgroundThread(new Messages.DocumentSelected(selectedDocument));
        }
    }
}