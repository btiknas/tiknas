using System.Collections.Generic;

namespace Tiknas.Validation.StringValues;

public interface ISelectionStringValueItemSource
{
    ICollection<ISelectionStringValueItem> Items { get; }
}
