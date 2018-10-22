using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Caliburn.Micro;
using SchadLucas.EzArchive.Domain;
using SchadLucas.EzArchive.Domain.Messages;
using SchadLucas.Logging;
using SchadLucas.Utilities;

namespace SchadLucas.EzArchive.Modules.NavigationPane
{
    public class NavigationPaneViewModel : Screen
    {
        private readonly IEzLogger _logger;
        private readonly NavigationPaneModel _model;
        private readonly IEventAggregator _eventAggregator;
        private Node _selectedNode;

        public NavigationPaneViewModel(IEzLogger logger, NavigationPaneModel model, IEventAggregator eventAggregator)
        {
            _logger = logger;
            _model = model;
            _eventAggregator = eventAggregator;

            AwaitableTask.Run(BuildNodeTree);
        }

        public Node Root { get; } = new Node(new EzFolder {Name = "ROOT", Id = Guid.Empty});

        public Node SelectedNote
        {
            get => _selectedNode;
            set
            {
                _selectedNode = value;
                OnSelectedNoteChanged(_selectedNode);
            }
        }

        private void BuildNodeTree()
        {
            var allNodes = _model.GetAllFolders().Select(f => new Node(f)).ToList();

            void IterateOverChildren(IEnumerable<Node> nodes)
            {
                foreach (var node in nodes)
                {
                    Task.Run(() =>
                    {
                        var children = allNodes.Where(n => n.Folder.Parent == node.Folder.Id);
                        node.Children.AddRange(children.OrderBy(n => n.Folder.Name));

                        IterateOverChildren(node.Children);
                    });
                }
            }


            IterateOverChildren(new[] {Root});
        }

        private void OnSelectedNoteChanged(in Node node)
        {
            _eventAggregator.PublishOnBackgroundThread(new Messages.FolderSelected(node.Folder));
        }
    }
}