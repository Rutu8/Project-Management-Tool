using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace projectmanagementtoolProject.Models;

public partial class Project
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    [StringLength(500)]
    public string? Name { get; set; }

    [Column("description")]
    [StringLength(500)]
    public string? Description { get; set; }

    [Column("ownerId")]
    public int OwnerId { get; set; }

    [InverseProperty("Project")]
    public virtual ICollection<Job>? Jobs { get; set; } = new List<Job>();

    [ForeignKey("OwnerId")]
    [InverseProperty("Projects")]
    public virtual User? Owner { get; set; } = null!;
}
