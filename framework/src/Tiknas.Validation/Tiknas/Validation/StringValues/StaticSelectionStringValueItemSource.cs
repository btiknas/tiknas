﻿using System.Collections.Generic;

namespace Tiknas.Validation.StringValues;

public class StaticSelectionStringValueItemSource : ISelectionStringValueItemSource
{
    public ICollection<ISelectionStringValueItem> Items { get; }

    public StaticSelectionStringValueItemSource(params ISelectionStringValueItem[] items)
    {
        Items = Check.NotNullOrEmpty(items, nameof(items));
    }
}
