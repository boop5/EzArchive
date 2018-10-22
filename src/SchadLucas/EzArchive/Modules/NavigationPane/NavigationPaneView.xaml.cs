using System.Windows;

namespace SchadLucas.EzArchive.Modules.NavigationPane
{
    public partial class NavigationPaneView
    {
        private NavigationPaneViewModel _viewModel;

        public NavigationPaneView()
        {
            InitializeComponent();
        }

        private void NavigationPaneView_OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is NavigationPaneViewModel vm)
            {
                _viewModel = vm;
            }
        }

        private void TreeView_OnSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (_viewModel != null && e.NewValue is Node node)
            {
                _viewModel.SelectedNote = node;
            }
        }
    }
}