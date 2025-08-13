using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace projectmanagementtoolProject.Models;

public partial class Job
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("title")]
    [StringLength(500)]
    public string? Title { get; set; }

    [Column("status")]
    [StringLength(500)]
    public string? Status { get; set; }

    [Column("projectId")]
    public int ProjectId { get; set; }

    [Column("priority")]
    [StringLength(500)]
    public string? Priority { get; set; }
        
    [ForeignKey("ProjectId")]
    [InverseProperty("Jobs")]
    public virtual Project? Project { get; set; }
}
