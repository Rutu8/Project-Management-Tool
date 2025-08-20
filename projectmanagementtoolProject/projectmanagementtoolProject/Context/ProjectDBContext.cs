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

    public virtual DbSet<UserJob> UserJobs { get; set; }

   

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Job>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Tasks");

            entity.Property(e => e.Priority).IsFixedLength();
            entity.Property(e => e.Status).IsFixedLength();
            entity.Property(e => e.Title).IsFixedLength();

            entity.HasOne(d => d.Project).WithMany(p => p.Jobs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tasks_Projects");

            entity.HasOne(d => d.User).WithMany(p => p.Jobs).HasConstraintName("FK_Jobs_Users");
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.Property(e => e.Description).IsFixedLength();
            entity.Property(e => e.Name).IsFixedLength();

            entity.HasOne(d => d.Owner).WithMany(p => p.Projects)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Projects_Users");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.Email).IsFixedLength();
            entity.Property(e => e.MobileNo).IsFixedLength();
            entity.Property(e => e.Name).IsFixedLength();
            entity.Property(e => e.Password).IsFixedLength();
            entity.Property(e => e.Usertype).IsFixedLength();
        });

        modelBuilder.Entity<UserJob>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserJob__3214EC07E7A6B55F");

            entity.HasOne(d => d.Job).WithMany(p => p.UserJobs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserJob_Job");

            entity.HasOne(d => d.User).WithMany(p => p.UserJobs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserJob_User");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
