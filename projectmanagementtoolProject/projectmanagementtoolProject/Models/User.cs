using System;
using System.Collections.Generic;

namespace projectmanagementtoolProject.Models;

public partial class User
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }

    public string? MobileNo { get; set; }

    public string? Password { get; set; }

    public string? Usertype { get; set; }

    public virtual ICollection<Job>? Jobs { get; set; } = new List<Job>();

    public virtual ICollection<Project>? Projects { get; set; } = new List<Project>();

    public virtual ICollection<UserJob>? UserJobs { get; set; } = new List<UserJob>();
}
