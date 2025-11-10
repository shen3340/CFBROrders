using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CFBROrders.SDK.Models;

[Keyless]
[Table("bans")]
public partial class Ban
{
    [Column("id")]
    public int Id { get; set; }

    /// <summary>
    /// // Username: 1
    /// // Prevent ban, username, for suspend flag: 2
    /// // Allow login without email: 3
    /// // Prevent ban, Reddit ban: 4
    /// </summary>
    [Column("class")]
    public int? Class { get; set; }

    [Column("cip", TypeName = "citext")]
    public string? Cip { get; set; }

    [Column("uname", TypeName = "citext")]
    public string? Uname { get; set; }

    [Column("ua", TypeName = "citext")]
    public string? Ua { get; set; }

    [Column("reason")]
    [StringLength(256)]
    public string? Reason { get; set; }
}
