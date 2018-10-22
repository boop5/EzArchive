using System.Windows;

namespace SchadLucas.EzArchive.WpfControls
{
    public partial class SearchBoxView
    {
        public SearchBoxView()
        {
            InitializeComponent();

            DataObject.AddPastingHandler(SearchText, OnPaste);
        }

        private static void OnPaste(object sender, DataObjectPastingEventArgs e)
        {
            e.CancelCommand();
        }
    }
}