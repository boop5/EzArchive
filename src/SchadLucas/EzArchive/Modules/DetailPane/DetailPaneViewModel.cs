using Caliburn.Micro;
using SchadLucas.EzArchive.Domain;
using SchadLucas.EzArchive.Domain.Messages;

namespace SchadLucas.EzArchive.Modules.DetailPane
{
    public sealed class DetailPaneViewModel : Screen, IHandle<Messages.DocumentSelected>
    {
        public EzDocument Document { get; private set; }

        public void Handle(Messages.DocumentSelected message)
        {
            Document = message.Document;
            NotifyOfPropertyChange(() => Document);
        }
    }
}