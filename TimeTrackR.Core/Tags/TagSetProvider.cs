using System;
using System.Collections.Generic;
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

        public ICollection<Tag> GetCurrentTagSet()
        {
            return _tags.ToList();
        }

        public void AddFromDelimitedString(string delimitedString)
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