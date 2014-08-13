using System.Collections.Generic;
using System.Linq;

namespace TimeTrackR.Core.Tags
{
    public class TagSetProvider : ITagSetProvider
    {
        private readonly IList<Tag> _tags = new List<Tag>();

        public void AddTag(Tag tag)
        {
            _tags.Add(tag);
        }

        public void Clear()
        {
            _tags.Clear();
        }

        public IEnumerable<Tag> GetCurrentTagSet()
        {
            return _tags;
        }

        public void AddFromDelimitedString(string delimitedString)
        {
            var entries = (from x in delimitedString.Split(',')
                           select x.Trim()).ToList();

            foreach(var entry in entries)
            {
                AddTag(new Tag {Name = entry});
            }
        }
    }
}