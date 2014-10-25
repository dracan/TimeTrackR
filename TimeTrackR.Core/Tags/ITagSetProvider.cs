﻿using System;
using System.Collections.Generic;

namespace TimeTrackR.Core.Tags
{
    public interface ITagSetProvider
    {
        void AddTag(Tag tag);
        void Clear();
        void AddFromDelimitedString(string delimitedString);

        IEnumerable<Tag> GetCurrentTagSet();

        EventHandler OnTagChanged { get; set; }
    }
}