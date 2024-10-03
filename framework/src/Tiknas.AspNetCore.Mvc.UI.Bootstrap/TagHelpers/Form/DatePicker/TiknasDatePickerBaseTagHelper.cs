using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Tiknas.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form.DatePicker;

public abstract class
    TiknasDatePickerBaseTagHelper<TTagHelper> : TiknasTagHelper<TTagHelper, TiknasDatePickerBaseTagHelperService<TTagHelper>>, ITiknasDatePickerOptions
    where TTagHelper : TiknasDatePickerBaseTagHelper<TTagHelper>

{
    private ITiknasDatePickerOptions _tiknasDatePickerOptionsImplementation;

    public string? Label { get; set; }

    public string? LabelTooltip { get; set; }

    public string LabelTooltipIcon { get; set; } = "bi-info-circle";

    public string LabelTooltipPlacement { get; set; } = "right";

    public bool LabelTooltipHtml { get; set; } = false;

    [HtmlAttributeName("info")]
    public string? InfoText { get; set; }

    [HtmlAttributeName("disabled")]
    public bool IsDisabled { get; set; } = false;

    [HtmlAttributeName("readonly")]
    public bool? IsReadonly { get; set; } = false;

    public bool AutoFocus { get; set; }

    public TiknasFormControlSize Size { get; set; } = TiknasFormControlSize.Default;

    [HtmlAttributeName("required-symbol")]
    public bool DisplayRequiredSymbol { get; set; } = true;

    public string? Name { get; set; }

    public string? Value { get; set; }

    public bool SuppressLabel { get; set; }

    public bool AddMarginBottomClass  { get; set; } = true;

    protected TiknasDatePickerBaseTagHelper(TiknasDatePickerBaseTagHelperService<TTagHelper> service) : base(service)
    {
        _tiknasDatePickerOptionsImplementation = new TiknasDatePickerOptions();
    }

    public void SetDatePickerOptions(ITiknasDatePickerOptions options)
    {
        _tiknasDatePickerOptionsImplementation = options;
    }

    public string? PickerId {
        get => _tiknasDatePickerOptionsImplementation.PickerId;
        set => _tiknasDatePickerOptionsImplementation.PickerId = value;
    }

    public DateTime? MinDate {
        get => _tiknasDatePickerOptionsImplementation.MinDate;
        set => _tiknasDatePickerOptionsImplementation.MinDate = value;
    }

    public DateTime? MaxDate {
        get => _tiknasDatePickerOptionsImplementation.MaxDate;
        set => _tiknasDatePickerOptionsImplementation.MaxDate = value;
    }

    public object? MaxSpan {
        get => _tiknasDatePickerOptionsImplementation.MaxSpan;
        set => _tiknasDatePickerOptionsImplementation.MaxSpan = value;
    }

    public bool? ShowDropdowns {
        get => _tiknasDatePickerOptionsImplementation.ShowDropdowns;
        set => _tiknasDatePickerOptionsImplementation.ShowDropdowns = value;
    }

    public int? MinYear {
        get => _tiknasDatePickerOptionsImplementation.MinYear;
        set => _tiknasDatePickerOptionsImplementation.MinYear = value;
    }

    public int? MaxYear {
        get => _tiknasDatePickerOptionsImplementation.MaxYear;
        set => _tiknasDatePickerOptionsImplementation.MaxYear = value;
    }

    public TiknasDatePickerWeekNumbers WeekNumbers {
        get => _tiknasDatePickerOptionsImplementation.WeekNumbers;
        set => _tiknasDatePickerOptionsImplementation.WeekNumbers = value;
    }

    public bool? TimePicker {
        get => _tiknasDatePickerOptionsImplementation.TimePicker;
        set => _tiknasDatePickerOptionsImplementation.TimePicker = value;
    }

    public int? TimePickerIncrement {
        get => _tiknasDatePickerOptionsImplementation.TimePickerIncrement;
        set => _tiknasDatePickerOptionsImplementation.TimePickerIncrement = value;
    }

    public bool? TimePicker24Hour {
        get => _tiknasDatePickerOptionsImplementation.TimePicker24Hour;
        set => _tiknasDatePickerOptionsImplementation.TimePicker24Hour = value;
    }

    public bool? TimePickerSeconds {
        get => _tiknasDatePickerOptionsImplementation.TimePickerSeconds;
        set => _tiknasDatePickerOptionsImplementation.TimePickerSeconds = value;
    }

    public List<TiknasDatePickerRange>? Ranges {
        get => _tiknasDatePickerOptionsImplementation.Ranges;
        set => _tiknasDatePickerOptionsImplementation.Ranges = value;
    }

    public bool? ShowCustomRangeLabel {
        get => _tiknasDatePickerOptionsImplementation.ShowCustomRangeLabel;
        set => _tiknasDatePickerOptionsImplementation.ShowCustomRangeLabel = value;
    }

    public bool? AlwaysShowCalendars {
        get => _tiknasDatePickerOptionsImplementation.AlwaysShowCalendars;
        set => _tiknasDatePickerOptionsImplementation.AlwaysShowCalendars = value;
    }

    public TiknasDatePickerOpens Opens {
        get => _tiknasDatePickerOptionsImplementation.Opens;
        set => _tiknasDatePickerOptionsImplementation.Opens = value;
    }

    public TiknasDatePickerDrops Drops {
        get => _tiknasDatePickerOptionsImplementation.Drops;
        set => _tiknasDatePickerOptionsImplementation.Drops = value;
    }

    public string? ButtonClasses {
        get => _tiknasDatePickerOptionsImplementation.ButtonClasses;
        set => _tiknasDatePickerOptionsImplementation.ButtonClasses = value;
    }

    public string? TodayButtonClasses {
        get => _tiknasDatePickerOptionsImplementation.TodayButtonClasses;
        set => _tiknasDatePickerOptionsImplementation.TodayButtonClasses = value;
    }

    public string? ApplyButtonClasses {
        get => _tiknasDatePickerOptionsImplementation.ApplyButtonClasses;
        set => _tiknasDatePickerOptionsImplementation.ApplyButtonClasses = value;
    }

    public string? ClearButtonClasses {
        get => _tiknasDatePickerOptionsImplementation.ClearButtonClasses;
        set => _tiknasDatePickerOptionsImplementation.ClearButtonClasses = value;
    }

    public object? Locale {
        get => _tiknasDatePickerOptionsImplementation.Locale;
        set => _tiknasDatePickerOptionsImplementation.Locale = value;
    }

    public bool? AutoApply {
        get => _tiknasDatePickerOptionsImplementation.AutoApply;
        set => _tiknasDatePickerOptionsImplementation.AutoApply = value;
    }

    public bool? LinkedCalendars {
        get => _tiknasDatePickerOptionsImplementation.LinkedCalendars;
        set => _tiknasDatePickerOptionsImplementation.LinkedCalendars = value;
    }

    public bool? AutoUpdateInput {
        get => _tiknasDatePickerOptionsImplementation.AutoUpdateInput;
        set => _tiknasDatePickerOptionsImplementation.AutoUpdateInput = value;
    }

    public string? ParentEl {
        get => _tiknasDatePickerOptionsImplementation.ParentEl;
        set => _tiknasDatePickerOptionsImplementation.ParentEl = value;
    }

    [Obsolete("Use VisibleDateFormat instead.")]
    public string? DateFormat {
        get => _tiknasDatePickerOptionsImplementation.DateFormat;
        set => _tiknasDatePickerOptionsImplementation.DateFormat = value;
    }
    
    public string? VisibleDateFormat {
        get => _tiknasDatePickerOptionsImplementation.VisibleDateFormat;
        set => _tiknasDatePickerOptionsImplementation.VisibleDateFormat = value;
    }
    
    public string? InputDateFormat {
        get => _tiknasDatePickerOptionsImplementation.InputDateFormat;
        set => _tiknasDatePickerOptionsImplementation.InputDateFormat = value;
    }

    public bool OpenButton {
        get => _tiknasDatePickerOptionsImplementation.OpenButton;
        set => _tiknasDatePickerOptionsImplementation.OpenButton = value;
    }

    public bool? ClearButton {
        get => _tiknasDatePickerOptionsImplementation.ClearButton;
        set => _tiknasDatePickerOptionsImplementation.ClearButton = value;
    }

    public bool SingleOpenAndClearButton {
        get => _tiknasDatePickerOptionsImplementation.SingleOpenAndClearButton;
        set => _tiknasDatePickerOptionsImplementation.SingleOpenAndClearButton = value;
    }

    public bool? IsUtc {
        get => _tiknasDatePickerOptionsImplementation.IsUtc;
        set => _tiknasDatePickerOptionsImplementation.IsUtc = value;
    }

    public bool? IsIso {
        get => _tiknasDatePickerOptionsImplementation.IsIso;
        set => _tiknasDatePickerOptionsImplementation.IsIso = value;
    }

    public object? Options {
        get => _tiknasDatePickerOptionsImplementation.Options;
        set => _tiknasDatePickerOptionsImplementation.Options = value;
    }
}
