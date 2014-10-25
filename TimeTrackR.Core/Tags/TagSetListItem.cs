using System;
using System.Collections.Generic;

namespace TimeTrackR.Core.Tags
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
}