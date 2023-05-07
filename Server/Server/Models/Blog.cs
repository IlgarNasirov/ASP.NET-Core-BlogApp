using System;
using System.Collections.Generic;

namespace Server.Models;

public partial class Blog
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public DateTime Date { get; set; }

    public string Data { get; set; } = null!;

    public int Userid { get; set; }

    public int Views { get; set; }

    public bool? Status { get; set; }

    public virtual User User { get; set; } = null!;
}
