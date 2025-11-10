using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CFBROrders.SDK.Models;

[Keyless]
[Table("continuation_responses")]
public partial class ContinuationResponse
{
    [Column("id")]
    public int Id { get; set; }

    [Column("poll_id")]
    public int? PollId { get; set; }

    [Column("user_id")]
    public int? UserId { get; set; }

    [Column("response")]
    public bool? Response { get; set; }
}
