using System;
using System.Collections.Generic;

namespace DBLaba6.MainDB;

public partial class Link
{
    public int LinkId { get; set; }

    public int DataId { get; set; }

    public int TagId { get; set; }

    public virtual Datum Data { get; set; } = null!;

    public virtual Tag Tag { get; set; } = null!;
}
