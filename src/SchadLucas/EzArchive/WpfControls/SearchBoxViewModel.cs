using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Input;
using Caliburn.Micro;
using SchadLucas.Logging;
using SchadLucas.Wpf.Utilities;

namespace SchadLucas.EzArchive.WpfControls
{
    public class SearchBoxViewModel : Screen
    {
        private static readonly int MinLength = 3;

        private readonly IEzLogger _logger;
        private string _searchText = string.Empty;

        public SearchBoxViewModel(IEzLogger logger)
        {
            _logger = logger;
        }

        [SuppressMessage("ReSharper", "CollectionNeverQueried.Global", Justification = "XAML Binding")]
        public BindableCollection<string> SearchTerms { get; } = new BindableCollection<string>();

        public string SearchText
        {
            get => _searchText;
            set => Set(ref _searchText, value);
        }

        public void OnKeyDown(KeyEventArgs e)
        {
            // stop processing if CTRL or ALT are active
            if ((Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control || (Keyboard.Modifiers & ModifierKeys.Alt) == ModifierKeys.Alt)
            {
                return;
            }

            if (e.Key == Key.Space || e.Key == Key.Enter)
            {
                Submit(e);
            }
            else
            {
                var keyChar = KeyEventUtil.GetCharFromKey(e.Key);
                if (keyChar == '"' && SearchText.Length > 1 && SearchText.StartsWith("\""))
                {
                    SearchText += "\"";
                    Submit(e);
                    return;
                }

                if (!CanHandleKey(e, keyChar))
                {
                    e.Handled = true;
                }
            }
        }

        public void RemoveSearchTag(object sender)
        {
            if (sender is string tag)
            {
                _logger.Debug($"Remove [{tag}] as Search-Term.");

                SearchTerms.Remove(tag);
            }
        }

        private bool CanHandleKey(KeyEventArgs e, char keyChar)
        {
            switch (keyChar)
            {
                case '#':
                case '"':
                    return SearchText.Length == 0;
            }

            return true;
        }

        private void Submit(RoutedEventArgs e)
        {
            bool CanSubmit() => SearchText.Length >= MinLength && !SearchTerms.Contains(SearchText);
            bool IsFullTextSearch() => SearchText.Equals("\"") || SearchText.StartsWith("\"") && !SearchText.EndsWith("\"");


            if (IsFullTextSearch())
            {
                return;
            }

            e.Handled = true;

            if (CanSubmit())
            {
                _logger.Debug($"Add [{SearchText}] as Search-Term.");

                SearchTerms.Add(SearchText);
                SearchText = string.Empty;
            }
        }
    }
}