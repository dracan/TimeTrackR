using System.Collections.Generic;
using System.Linq;
using System.Windows;
using TimeTrackR.Core.Tags;
using TimeTrackR.Core.Timer;

namespace TimeTrackR
{
    public class TagSetListItem
    {
        public IEnumerable<string> Tags { get; set; }

        public string TagsAsString
        {
            get { return string.Join(", ", Tags); }
        }
    }

    /// <summary>
    /// Interaction logic for TagSelection.xaml
    /// </summary>
    public partial class TagSelection : Window
    {
        public IEnumerable<TagSetListItem> Items { get; set; }

        private readonly Timer _timer;
        private readonly ITagSetProvider _tagSetProvider;

        public TagSelection(Timer timer, ITagSetProvider tagSetProvider)
        {
            _timer = timer;
            _tagSetProvider = tagSetProvider;

            InitialiseItems();

            InitializeComponent();

            TextBoxTagEntry.Focus();
        }

        private void InitialiseItems()
        {
            Items = (from x in _timer.HistoryItems
                     select new TagSetListItem
                            {
                                Tags = x.TagSet.Select(t => t.Name).ToList()
                            }).Distinct().ToList(); //(medium) This distinct doesn't work, as we need to compare the tags within the instances. Need to Google how to do this. Perhaps adding an Equals overload to the TagSetListItem class, or something like that?
        }

        private void ButtonOkay_OnClick(object sender, RoutedEventArgs e)
        {
            _tagSetProvider.Clear();
            _tagSetProvider.AddFromDelimitedString(TextBoxTagEntry.Text);
            Close();
        }
    }
}
