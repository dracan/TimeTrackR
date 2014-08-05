using System.Collections.Generic;

namespace TimeTrackR.Core.Tags
{
    public class TagSetProvider : ITagSetProvider
    {
        private readonly IList<Tag> _tags = new List<Tag>();

        public void AddTag(Tag tag)
        {
            _tags.Add(tag);
        }

        public void Empty()
        {
            _tags.Clear();
        }

        public IEnumerable<Tag> GetCurrentTagSet()
        {
            return _tags;
        }
    }
}