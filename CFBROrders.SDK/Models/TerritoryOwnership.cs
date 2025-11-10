using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CFBROrders.SDK.Models;

[Keyless]
[Table("territory_ownership")]
[Index("OwnerId", "TurnId", Name = "territory_ownershi_idx_owner_id_turn_id")]
public partial class TerritoryOwnership
{
    [Column("id")]
    public int Id { get; set; }

    [Column("territory_id")]
    public int? TerritoryId { get; set; }

    [Column("owner_id")]
    public int? OwnerId { get; set; }

    [Column("previous_owner_id")]
    public int? PreviousOwnerId { get; set; }

    [Column("random_number")]
    public double? RandomNumber { get; set; }

    [Column("timestamp", TypeName = "timestamp without time zone")]
    public DateTime? Timestamp { get; set; }

    [Column("mvp")]
    public int? Mvp { get; set; }

    [Column("turn_id")]
    public int? TurnId { get; set; }

    [Column("is_respawn")]
    public bool IsRespawn { get; set; }
}
