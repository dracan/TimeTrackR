using System.Collections.Generic;

namespace TimeTrackR.Core.Tags
{
    public interface ITagSetProvider
    {
        void AddTag(Tag tag);
        void Empty();

        IEnumerable<Tag> GetCurrentTagSet();
    }
}