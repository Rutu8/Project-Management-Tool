using System;
using System.Collections.Generic;

namespace projectmanagementtoolProject.Models;

public partial class Project
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public int OwnerId { get; set; }

    public virtual ICollection<Job>? Jobs { get; set; } = new List<Job>();

    public virtual User? Owner { get; set; } = null!;

    public virtual ICollection<UserJob>? UserJobs { get; set; } = new List<UserJob>();
}
