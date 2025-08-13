using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace projectmanagementtoolProject.Models;

public partial class User
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    [StringLength(500)]
    public string? Name { get; set; }

    [Column("email")]
    [StringLength(100)]
    public string? Email { get; set; }

    [Column("mobileNo")]
    [StringLength(10)]
    public string? MobileNo { get; set; }

    [Column("password")]
    [StringLength(10)]
    public string? Password { get; set; }

    [Column("usertype")]
    [StringLength(100)]
    public string? Usertype { get; set; }

    [InverseProperty("Owner")]
    public virtual ICollection<Project>? Projects { get; set; } = new List<Project>();
}
