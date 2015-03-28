using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace TimeTrackR.Core.Tags
{
    public class TagSetProvider : ITagSetProvider
    {
        private readonly IList<Tag> _tags = new List<Tag>();

        public EventHandler OnTagChanged { get; set; }

        public void AddTag(Tag tag)
        {
            _tags.Add(tag);

            if(OnTagChanged != null)
            {
                OnTagChanged(this, new EventArgs());
            }
        }

        public void Clear()
        {
            _tags.Clear();

            if(OnTagChanged != null)
            {
                OnTagChanged(this, new EventArgs());
            }
        }

        public ObservableCollection<Tag> GetCurrentTagSet()
        {
            return new ObservableCollection<Tag>(_tags);
        }

        public void AddFromDelimitedString(string delimitedString)
        {
            if(!string.IsNullOrEmpty(delimitedString))
            {
                var entries = (from x in delimitedString.Split(',')
                               select x.Trim()).ToList();

                foreach(var entry in entries)
                {
                    if(!string.IsNullOrEmpty(entry))
                    {
                        AddTag(new Tag {Name = entry});
                    }
                }
            }
        }
    }
}