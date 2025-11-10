using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CFBROrders.SDK.Models;

[Keyless]
[Table("logs")]
public partial class Log
{
    [Column("id")]
    public int Id { get; set; }

    [Column("route")]
    public string? Route { get; set; }

    [Column("query")]
    public string? Query { get; set; }

    [Column("payload")]
    public string? Payload { get; set; }

    [Column("timestamp", TypeName = "timestamp without time zone")]
    public DateTime? Timestamp { get; set; }
}
