using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CFBROrders.SDK.Models;

[Keyless]
public partial class Rollinfo
{
    [Column("rollstarttime")]
    public string? Rollstarttime { get; set; }

    [Column("rollendtime")]
    public string? Rollendtime { get; set; }

    [Column("chaosrerolls")]
    public int? Chaosrerolls { get; set; }

    [Column("chaosweight")]
    public int? Chaosweight { get; set; }

    [Column("day")]
    public int? Day { get; set; }

    [Column("season")]
    public int? Season { get; set; }

    [Column("json_agg", TypeName = "json")]
    public string? JsonAgg { get; set; }
}
