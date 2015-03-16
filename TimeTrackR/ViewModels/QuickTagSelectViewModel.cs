using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using TimeTrackR.Core.Tags;
using TimeTrackR.Core.Timer;

namespace TimeTrackR.ViewModels
{
    public class QuickTagSelectViewModel
    {
        public bool HaveTagsChanged { get; set; }

        public TagSetListItem SelectedItem { get; set; }
        public bool Cancelled { get; set; }

        private readonly IEnumerable<TimerHistoryItem> _timerHistoryItems;
        private readonly ITagSetProvider _tagSetProvider;

        public QuickTagSelectViewModel(IEnumerable<TimerHistoryItem> timerHistoryItems, ITagSetProvider tagSetProvider)
        {
            _timerHistoryItems = timerHistoryItems;
            _tagSetProvider = tagSetProvider;
        }

        private IEnumerable<TagSetListItem> _items;
        public IEnumerable<TagSetListItem> Items
        {
            get
            {
                return _items ?? (_items = (from x in _timerHistoryItems
                                            where x.Tags.Any() && x.Start >= DateTime.Now.Date //(todo) Currently only shows today's tags. Perhaps this should be configurable?
                                            select x).Distinct().OrderByDescending(x => x.Start).Select(x => new TagSetListItem
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
                                               if(SelectedItem != null)
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
                                           }
                       };
            }
        }

        private void OnOkay()
        {
            var oldTagSetAsString = _tagSetProvider.GetCurrentTagSet().Aggregate("", (current, tag) => current + tag.Name);

            _tagSetProvider.Clear();
            _tagSetProvider.AddFromDelimitedString(SelectedItem.TagsAsString);

            var newTagSetAsString = _tagSetProvider.GetCurrentTagSet().Aggregate("", (current, tag) => current + tag.Name);

            HaveTagsChanged = oldTagSetAsString != newTagSetAsString;
        }
    }
}