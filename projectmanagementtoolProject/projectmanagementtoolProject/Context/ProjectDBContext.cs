using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using projectmanagementtoolProject.Models;

namespace projectmanagementtoolProject.Context;

public partial class ProjectDBContext : DbContext
{
    public ProjectDBContext()
    {
    }

    public ProjectDBContext(DbContextOptions<ProjectDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Job> Jobs { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<User> Users { get; set; }

   
    
}
