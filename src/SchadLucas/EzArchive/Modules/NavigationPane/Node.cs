using Caliburn.Micro;
using SchadLucas.EzArchive.Domain;

namespace SchadLucas.EzArchive.Modules.NavigationPane
{
    public class Node
    {
        public Node(EzFolder folder)
        {
            Folder = folder;
        }

        public BindableCollection<Node> Children { get; } = new BindableCollection<Node>();
        public EzFolder Folder { get; }

        public override string ToString() => Folder.Name;
    }
}