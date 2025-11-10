using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CFBROrders.SDK.Models;

[Keyless]
[Table("teams")]
public partial class Team
{
    [Column("id")]
    public int Id { get; set; }

    [Column("tname", TypeName = "citext")]
    public string? Tname { get; set; }

    [Column("tshortname", TypeName = "citext")]
    public string? Tshortname { get; set; }

    [Column("creation_date", TypeName = "timestamp without time zone")]
    public DateTime? CreationDate { get; set; }

    [Column("color_1")]
    public string? Color1 { get; set; }

    [Column("color_2")]
    public string? Color2 { get; set; }

    [Column("logo")]
    public string? Logo { get; set; }

    [Column("seasons")]
    public List<int>? Seasons { get; set; }

    [Column("respawn_count")]
    public int RespawnCount { get; set; }
}
