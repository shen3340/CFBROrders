using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CFBROrders.SDK.Models;

[Keyless]
[Table("users")]
[Index("Id", Name = "users_uid", IsUnique = true)]
[Index("Uname", "Platform", Name = "users_uname_platform", IsUnique = true)]
public partial class User
{
    [Column("id")]
    public int Id { get; set; }

    [Column("uname", TypeName = "citext")]
    public string Uname { get; set; } = null!;

    [Column("platform", TypeName = "citext")]
    public string Platform { get; set; } = null!;

    [Column("join_date", TypeName = "timestamp without time zone")]
    public DateTime? JoinDate { get; set; }

    [Column("current_team")]
    public int CurrentTeam { get; set; }

    [Column("auth_key", TypeName = "citext")]
    public string? AuthKey { get; set; }

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

    [Column("role_id")]
    public int? RoleId { get; set; }

    [Column("playing_for")]
    public int? PlayingFor { get; set; }

    [NotMapped]
    public List<int>? PastTeams { get; set; }

    [Column("past_teams_string")]
    public string? PastTeamsString
    {
        get => PastTeams != null ? string.Join(",", PastTeams) : null;
        set => PastTeams = !string.IsNullOrEmpty(value)
                            ? value.Split(',').Select(int.Parse).ToList()
                            : new List<int>();
    }

    [Column("awards_bak")]
    public int? AwardsBak { get; set; }

    [Column("discord_id")]
    public long? DiscordId { get; set; }

    [Column("is_alt")]
    public bool? IsAlt { get; set; }

    [Column("must_captcha")]
    public bool? MustCaptcha { get; set; }
}
