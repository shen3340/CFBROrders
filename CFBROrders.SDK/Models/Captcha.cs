using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CFBROrders.SDK.Models;

[Keyless]
[Table("captchas")]
public partial class Captcha
{
    [Column("id")]
    public int Id { get; set; }

    [Column("title")]
    [StringLength(32)]
    public string? Title { get; set; }

    [Column("content", TypeName = "citext")]
    public string? Content { get; set; }

    [Column("creation", TypeName = "timestamp without time zone")]
    public DateTime? Creation { get; set; }
}
