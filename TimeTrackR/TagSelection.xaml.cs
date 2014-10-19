using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using TimeTrackR.Core.Tags;
using TimeTrackR.Core.Timer;

namespace TimeTrackR
{
    public class TagSetListItem : IEquatable<TagSetListItem>
    {
        public IEnumerable<string> Tags { get; set; }

        public string TagsAsString
        {
            get { return string.Join(", ", Tags); }
        }

        public bool Equals(TagSetListItem other)
        {
            // Check whether the compared object is null
            if(ReferenceEquals(other, null)) return false;

            // Check whether the compared object references the same data
            if(ReferenceEquals(this, other)) return true;

            // Check whether the objects' properties are equal
            return TagsAsString.Equals(other.TagsAsString);
        }

        public override int GetHashCode()
        {
            return TagsAsString.GetHashCode();
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
                            }).Distinct().ToList();
        }

        private void ButtonOkay_OnClick(object sender, RoutedEventArgs e)
        {
            _tagSetProvider.Clear();
            _tagSetProvider.AddFromDelimitedString(TextBoxTagEntry.Text);
            Close();
        }
    }
}
