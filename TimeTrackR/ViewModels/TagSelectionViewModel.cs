using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using TimeTrackR.Core.Tags;
using TimeTrackR.Core.Timer;

namespace TimeTrackR.ViewModels
{
    public class TagSelectionViewModel
    {
        public bool HaveTagsChanged { get; set; }

        private readonly Timer _timer;
        private readonly ITagSetProvider _tagSetProvider;

        public string TagsAsDelimitedString { get; set; }

        public TagSelectionViewModel(Timer timer, ITagSetProvider tagSetProvider)
        {
            _timer = timer;
            _tagSetProvider = tagSetProvider;
        }

        private IEnumerable<TagSetListItem> _items;
        public IEnumerable<TagSetListItem> Items
        {
            get
            {
                return _items ?? (_items = (from x in _timer.HistoryItems
                                            where x.Tags.Any()
                                            select new TagSetListItem
                                                   {
                                                       Tags = x.Tags.Select(t => t.Name).ToList()
                                                   }).Distinct().ToList());
            }
        }

        public ICommand OkayCommand
        {
            get
            {
                return new DelegateCommand
                       {
                           CanExecuteFunc = () => true,
                           CommandAction = o =>
                                           {
                                               OnOkay();

                                               if(o != null)
                                               {
                                                   var window = o as Window;

                                                   if(window != null)
                                                   {
                                                       window.Close();
                                                   }
                                               }
                                           }
                       };
            }
        }

        private void OnOkay()
        {
            var oldTagSetAsString = _tagSetProvider.GetCurrentTagSet().Aggregate("", (current, tag) => current + tag.Name);

            _tagSetProvider.Clear();
            _tagSetProvider.AddFromDelimitedString(TagsAsDelimitedString);

            var newTagSetAsString = _tagSetProvider.GetCurrentTagSet().Aggregate("", (current, tag) => current + tag.Name);

            HaveTagsChanged = oldTagSetAsString != newTagSetAsString;
        }
    }
}