﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace WebApplication2.Models;

public partial class Order
{
    public int Id { get; set; }

    public DateTime? CreatedDate { get; set; }

    public int? CustomerId { get; set; }

    public string ShipName { get; set; }

    public string ShipMobile { get; set; }

    public string ShipAddress { get; set; }

    public string ShipEmail { get; set; }

    public int? Status { get; set; }
}