using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CFBROrders.SDK.Models;

[Keyless]
public partial class Player
{
    [Column("id")]
    public int? Id { get; set; }

    [Column("uname", TypeName = "citext")]
    public string? Uname { get; set; }

    [Column("platform", TypeName = "citext")]
    public string? Platform { get; set; }

    [Column("current_team")]
    public int? CurrentTeam { get; set; }

    [Column("overall")]
    public int? Overall { get; set; }

    [Column("turns")]
    public int? Turns { get; set; }

    [Column("game_turns")]
    public int? GameTurns { get; set; }

    [Column("mvps")]
    public int? Mvps { get; set; }

    [Column("streak")]
    public int? Streak { get; set; }

    [Column("awards")]
    public int? Awards { get; set; }

    [Column("tname", TypeName = "citext")]
    public string? Tname { get; set; }
}
