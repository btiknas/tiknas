namespace Tiknas.Validation.StringValues;

public class LocalizableSelectionStringValueItem : ISelectionStringValueItem
{
    public string Value { get; set; } = default!;

    public LocalizableStringInfo DisplayText { get; set; } = default!;
}
