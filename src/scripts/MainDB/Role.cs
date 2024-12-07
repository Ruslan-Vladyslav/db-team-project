using System;
using System.Collections.Generic;

namespace DBLaba6.MainDB;

public partial class Role
{
    public int RoleId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Access> Accesses { get; set; } = new List<Access>();
}
