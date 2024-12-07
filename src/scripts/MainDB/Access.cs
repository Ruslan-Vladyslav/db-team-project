using System;
using System.Collections.Generic;

namespace DBLaba6.MainDB;

public partial class Access
{
    public int AccessId { get; set; }

    public int DataId { get; set; }

    public int RoleId { get; set; }

    public int UserId { get; set; }

    public virtual Datum Data { get; set; } = null!;

    public virtual Role Role { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
