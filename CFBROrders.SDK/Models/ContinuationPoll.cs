using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CFBROrders.SDK.Models;

[Keyless]
[Table("continuation_polls")]
public partial class ContinuationPoll
{
    [Column("id")]
    public int Id { get; set; }

    [Column("question")]
    public string? Question { get; set; }

    [Column("incrment")]
    public int? Incrment { get; set; }

    [Column("turn_id")]
    public int? TurnId { get; set; }
}
