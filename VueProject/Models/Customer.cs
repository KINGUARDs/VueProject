﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace VueProject.Models;

[Index("City", Name = "City")]
[Index("CompanyName", Name = "CompanyName")]
[Index("PostalCode", Name = "PostalCode")]
[Index("Region", Name = "Region")]
public partial class Customer
{
    [Key]
    [Column("CustomerID")]
    [StringLength(5)]
    public string CustomerId { get; set; }

    [Required]
    [StringLength(40)]
    public string CompanyName { get; set; }

    [StringLength(30)]
    public string ContactName { get; set; }

    [StringLength(30)]
    public string ContactTitle { get; set; }

    [StringLength(60)]
    public string Address { get; set; }

    [StringLength(15)]
    public string City { get; set; }

    [StringLength(15)]
    public string Region { get; set; }

    [StringLength(10)]
    public string PostalCode { get; set; }

    [StringLength(15)]
    public string Country { get; set; }

    [StringLength(24)]
    public string Phone { get; set; }

    [StringLength(24)]
    public string Fax { get; set; }

    [InverseProperty("Customer")]
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}