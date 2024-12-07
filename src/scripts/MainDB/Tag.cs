using System;
using System.Collections.Generic;

namespace DBLaba6.MainDB;

public partial class Tag
{
    public int TagId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Link> Links { get; set; } = new List<Link>();
}
