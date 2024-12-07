using System;
using System.Collections.Generic;

namespace DBLaba6.MainDB;

public partial class Categoty
{
    public int CategoryId { get; set; }

    public string Name { get; set; } = null!;

    public int? ParentCategoryId { get; set; }

    public virtual ICollection<Datum> Data { get; set; } = new List<Datum>();

    public virtual ICollection<Categoty> InverseParentCategory { get; set; } = new List<Categoty>();

    public virtual Categoty? ParentCategory { get; set; }
}
