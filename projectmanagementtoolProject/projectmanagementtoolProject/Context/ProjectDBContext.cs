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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=Rutuja\\SQLEXPRESS;Initial Catalog=ProjectManagementtool;Integrated Security=True;Trust Server Certificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Job>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Tasks");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Priority)
                .HasMaxLength(500)
                .IsFixedLength()
                .HasColumnName("priority");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.Status)
                .HasMaxLength(500)
                .IsFixedLength()
                .HasColumnName("status");
            entity.Property(e => e.Title)
                .HasMaxLength(500)
                .IsFixedLength()
                .HasColumnName("title");

            entity.HasOne(d => d.Project).WithMany(p => p.Jobs)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tasks_Projects");

            entity.HasOne(d => d.User).WithMany(p => p.Jobs)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Jobs_Users");
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .IsFixedLength()
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(500)
                .IsFixedLength()
                .HasColumnName("name");
            entity.Property(e => e.OwnerId).HasColumnName("ownerId");

            entity.HasOne(d => d.Owner).WithMany(p => p.Projects)
                .HasForeignKey(d => d.OwnerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Projects_Users");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsFixedLength()
                .HasColumnName("email");
            entity.Property(e => e.MobileNo)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("mobileNo");
            entity.Property(e => e.Name)
                .HasMaxLength(500)
                .IsFixedLength()
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("password");
            entity.Property(e => e.Usertype)
                .HasMaxLength(100)
                .IsFixedLength()
                .HasColumnName("usertype");
        });

        modelBuilder.Entity<UserJob>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserJob__3214EC07E7A6B55F");

            entity.ToTable("UserJob");

            entity.Property(e => e.DateAssigned).HasColumnType("datetime");

            entity.HasOne(d => d.Job).WithMany(p => p.UserJobs)
                .HasForeignKey(d => d.JobId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserJob_Job");

            entity.HasOne(d => d.Project).WithMany(p => p.UserJobs)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("FK_UserJob_Projects");

            entity.HasOne(d => d.User).WithMany(p => p.UserJobs)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserJob_User");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
