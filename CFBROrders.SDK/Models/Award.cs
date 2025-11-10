using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CFBROrders.SDK.Models;

[Keyless]
[Table("awards")]
public partial class Award
{
    [Column("id")]
    public int Id { get; set; }

    [Column("user_id")]
    public int? UserId { get; set; }

    [Column("award_id")]
    public int? AwardId { get; set; }

    [Column("award_date")]
    public DateTime? AwardDate { get; set; }
}
