using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace projectmanagementtoolProject.Models;

[Table("UserJob")]
public partial class UserJob
{
    [Key]
    public int Id { get; set; }

    public int UserId { get; set; }

    public int JobId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime DateAssigned { get; set; }

    [ForeignKey("JobId")]
    [InverseProperty("UserJobs")]
    public virtual Job? Job { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("UserJobs")]
    public virtual User? User { get; set; } = null!;
}
