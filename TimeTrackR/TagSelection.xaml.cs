using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using TimeTrackR.Core.Tags;
using TimeTrackR.Core.Timer;

namespace TimeTrackR
{
    /// <summary>
    /// Interaction logic for TagSelection.xaml
    /// </summary>
    public partial class TagSelection : Window
    {
        public IEnumerable<TagSetListItem> Items { get; set; }
        public bool HaveTagsChanged { get; set; }

        private readonly Timer _timer;
        private readonly ITagSetProvider _tagSetProvider;

        public TagSelection(Timer timer, ITagSetProvider tagSetProvider)
        {
            _timer = timer;
            _tagSetProvider = tagSetProvider;

            InitialiseItems();

            InitializeComponent();

            TextBoxTagEntry.Focus();

            PreviewKeyDown += OnKeyDown;
        }

        private void InitialiseItems()
        {
            Items = (from x in _timer.HistoryItems
                     select new TagSetListItem
                            {
                                Tags = x.TagSet.Select(t => t.Name).ToList()
                            }).Distinct().ToList();
        }

        private void ButtonOkay_OnClick(object sender, RoutedEventArgs e)
        {
            var oldTagSetAsString = _tagSetProvider.GetCurrentTagSet().Aggregate("", (current, tag) => current + tag.Name);

            _tagSetProvider.Clear();
            _tagSetProvider.AddFromDelimitedString(TextBoxTagEntry.Text);

            var newTagSetAsString = _tagSetProvider.GetCurrentTagSet().Aggregate("", (current, tag) => current + tag.Name);

            HaveTagsChanged = oldTagSetAsString != newTagSetAsString;

            Close();
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Escape)
            {
                Close();
            }
        }
    }
}
