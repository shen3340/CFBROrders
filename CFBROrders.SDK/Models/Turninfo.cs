using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CFBROrders.SDK.Models;

[Keyless]
[Table("turninfo")]
[Index("Season", "Day", Name = "turninfo_day_", IsUnique = true)]
[Index("Id", Name = "turninfo_day_id", IsUnique = true)]
public partial class Turninfo
{
    [Column("id")]
    public int Id { get; set; }

    [Column("season")]
    public int? Season { get; set; }

    [Column("day")]
    public int? Day { get; set; }

    [Column("complete")]
    public bool? Complete { get; set; }

    [Column("active")]
    public bool? Active { get; set; }

    [Column("finale")]
    public bool? Finale { get; set; }

    [Column("chaosrerolls")]
    public int? Chaosrerolls { get; set; }

    [Column("chaosweight")]
    public int? Chaosweight { get; set; }

    [Column("rollendtime", TypeName = "timestamp without time zone")]
    public DateTime? Rollendtime { get; set; }

    [Column("rollstarttime", TypeName = "timestamp without time zone")]
    public DateTime? Rollstarttime { get; set; }

    [Column("allornothingenabled")]
    public bool? Allornothingenabled { get; set; }

    [Column("map")]
    public string? Map { get; set; }
}
