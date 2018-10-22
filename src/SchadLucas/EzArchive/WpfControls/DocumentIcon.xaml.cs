using System.Windows;
using SchadLucas.EzArchive.Domain;
using DependencyPropertyHelper = SchadLucas.Wpf.Utilities.DependencyPropertyHelper;

namespace SchadLucas.EzArchive.WpfControls
{
    public partial class DocumentIcon
    {

        public DocumentIcon()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty DocumentProperty =  DependencyPropertyHelper.Register<DocumentIcon, EzDocument>(nameof(Document));

        public EzDocument Document
        {
            get => GetValue(DocumentProperty) as EzDocument;
            set => SetValue(DocumentProperty, value);
        }
    }
}