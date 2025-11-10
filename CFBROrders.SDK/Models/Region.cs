using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CFBROrders.SDK.Models;

[Keyless]
[Table("regions")]
public partial class Region
{
    [Column("id")]
    public int Id { get; set; }

    [Column("name", TypeName = "citext")]
    public string? Name { get; set; }

    [Column("submap")]
    public int Submap { get; set; }
}
