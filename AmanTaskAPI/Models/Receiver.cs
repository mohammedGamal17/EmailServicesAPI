﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AmanTaskAPI.Models;

[Table("Receiver")]
public partial class Receiver
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Email { get; set; }

    [InverseProperty("Receiver")]
    public virtual ICollection<Mail> Mail { get; set; } = new List<Mail>();
}