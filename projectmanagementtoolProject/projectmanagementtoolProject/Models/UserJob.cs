using System;
using System.Collections.Generic;

namespace projectmanagementtoolProject.Models;

public partial class UserJob
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int JobId { get; set; }

    public DateTime DateAssigned { get; set; }

    public int? ProjectId { get; set; }

    public virtual Job? Job { get; set; } = null!;

    public virtual Project? Project { get; set; }

    public virtual User? User { get; set; } = null!;
}
