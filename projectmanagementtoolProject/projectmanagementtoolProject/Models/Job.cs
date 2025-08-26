using System;
using System.Collections.Generic;

namespace projectmanagementtoolProject.Models;

public partial class Job
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public string? Status { get; set; }

    public int ProjectId { get; set; }

    public string? Priority { get; set; }

    public int? UserId { get; set; }

    public virtual Project? Project { get; set; } = null!;

    public virtual User? User { get; set; }

    public virtual ICollection<UserJob>? UserJobs { get; set; } = new List<UserJob>();
}
