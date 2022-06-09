﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace VodLib.models
{
    [Keyless]
    [Index("ssn", Name = "employee_ssn_uindex", IsUnique = true)]
    public partial class Employee
    {
        [Required]
        [StringLength(20)]
        public string ssn { get; set; }
        public int client_id { get; set; }

        [ForeignKey("client_id")]
        public virtual Client client { get; set; }
    }
}