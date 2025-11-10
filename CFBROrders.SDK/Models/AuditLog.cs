using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CFBROrders.SDK.Models;

[Keyless]
[Table("audit_log")]
public partial class AuditLog
{
    [Column("id")]
    public int Id { get; set; }

    [Column("user_id")]
    public int UserId { get; set; }

    [Column("event")]
    public int Event { get; set; }

    [Column("timestamp", TypeName = "timestamp without time zone")]
    public DateTime Timestamp { get; set; }

    [Column("data", TypeName = "json")]
    public string? Data { get; set; }

    [Column("cip")]
    public string? Cip { get; set; }

    [Column("ua")]
    [StringLength(256)]
    public string? Ua { get; set; }
}
