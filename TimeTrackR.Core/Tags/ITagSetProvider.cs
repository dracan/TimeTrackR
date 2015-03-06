using System;
using System.Collections.ObjectModel;

namespace TimeTrackR.Core.Tags
{
    public interface ITagSetProvider
    {
        void AddTag(Tag tag);
        void Clear();
        void AddFromDelimitedString(string delimitedString);

        ObservableCollection<Tag> GetCurrentTagSet();

        EventHandler OnTagChanged { get; set; }
    }
}