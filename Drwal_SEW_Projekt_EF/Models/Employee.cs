﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Drwal_SEW_Projekt_EF.Models
{
    public partial class Employee
    {
        public string Ssn { get; set; }
        public int ClientId { get; set; }

        public virtual Client Client { get; set; }
    }
}