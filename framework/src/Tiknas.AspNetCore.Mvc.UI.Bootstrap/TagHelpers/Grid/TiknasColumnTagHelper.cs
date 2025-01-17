﻿using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Tiknas.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Grid;

public class TiknasColumnTagHelper : TiknasTagHelper<TiknasColumnTagHelper, TiknasColumnTagHelperService>
{
    // public string Size { get; set; }

    public ColumnSize Size { get; set; }

    public ColumnSize SizeSm { get; set; }

    public ColumnSize SizeMd { get; set; }

    public ColumnSize SizeLg { get; set; }

    public ColumnSize SizeXl { get; set; }

    public ColumnSize SizeXxl { get; set;}

    public ColumnSize Offset { get; set; }

    public ColumnSize OffsetSm { get; set; }

    public ColumnSize OffsetMd { get; set; }

    public ColumnSize OffsetLg { get; set; }

    public ColumnSize OffsetXl { get; set; }

    public ColumnSize OffsetXxl { get; set; }

    [HtmlAttributeName("order")]
    public ColumnOrder ColumnOrder { get; set; }

    public VerticalAlign VAlign { get; set; } = VerticalAlign.Default;

    public TiknasColumnTagHelper(TiknasColumnTagHelperService tagHelperService)
        : base(tagHelperService)
    {

    }
}
