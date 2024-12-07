using System;
using System.Collections.Generic;

namespace DBLaba6.MainDB;

public partial class Datum
{
    public int DataId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string Format { get; set; } = null!;

    public string Content { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public int CategoryId { get; set; }

    public virtual ICollection<Access> Accesses { get; set; } = new List<Access>();

    public virtual Categoty Category { get; set; } = null!;

    public virtual ICollection<Link> Links { get; set; } = new List<Link>();
}
