﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace VodLib.models
{
    [Index("client_id", Name = "client_client_id_uindex", IsUnique = true)]
    public partial class Client
    {
        public Client()
        {
            Order = new HashSet<Order>();
        }

        [Key]
        public int client_id { get; set; }
        [Required]
        [StringLength(40)]
        public string firstname { get; set; }
        [Required]
        [StringLength(40)]
        public string lastname { get; set; }
        [Required]
        [StringLength(100)]
        public string address { get; set; }
        [StringLength(20)]
        public string postalcode { get; set; }
        public DateTime dateofbirth { get; set; }

        [InverseProperty("client")]
        public virtual ICollection<Order> Order { get; set; }
    }
}