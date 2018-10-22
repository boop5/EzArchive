using System.Diagnostics.CodeAnalysis;
using Caliburn.Micro;
using SchadLucas.EzArchive.Domain.Messages;
using SchadLucas.EzArchive.Modules.DetailPane;
using SchadLucas.EzArchive.Modules.DocumentPane;
using SchadLucas.EzArchive.Modules.MenuBar;
using SchadLucas.EzArchive.Modules.NavigationPane;
using SchadLucas.EzArchive.Modules.Preview;
using SchadLucas.EzArchive.Modules.Ribbon;
using SchadLucas.EzArchive.Modules.StatusBar;
using SchadLucas.EzArchive.Modules.TabBar;

namespace SchadLucas.EzArchive.Shell
{
    public class ShellViewModel : Screen, IHandle<Messages.DocumentSelected>
    {
        [SuppressMessage("ReSharper", "SuggestBaseTypeForParameter", Justification = "Consturctor Injection")]
        public ShellViewModel(
            DetailPaneViewModel detail,
            DocumentPaneViewModel documentPane,
            MenuBarViewModel menuBar,
            NavigationPaneViewModel navPane,
            PreviewViewModel preview,
            RibbonViewModel ribbon,
            StatusBarViewModel statusBar,
            TabBarViewModel tabBar)
        {
            Details = detail;
            DocumentPane = documentPane;
            MenuBar = menuBar;
            NavPane = navPane;
            Preview = preview;
            Ribbon = ribbon;
            StatusBar = statusBar;
            TabBar = tabBar;
        }

        public Screen Details { get; }
        public Screen DocumentPane { get; }
        public Screen MenuBar { get; }
        public Screen NavPane { get; }
        public Screen Preview { get; }
        public Screen Ribbon { get; }
        public Screen StatusBar { get; }
        public Screen TabBar { get; }

        public bool DocumentSelected {get; private set;}
        public void Handle(Messages.DocumentSelected message)
        {
            DocumentSelected = !Equals(message.Document, null);
            NotifyOfPropertyChange(() => DocumentSelected);
        }
    }
}