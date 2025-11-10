using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CFBROrders.SDK.Models;

[Keyless]
[Table("Teams")]
public partial class Territory
{
    [Column("id")]
    public int Id { get; set; }

    [Column("name", TypeName = "citext")]
    public string? Name { get; set; }

    [Column("region")]
    public int? Region { get; set; }
}
