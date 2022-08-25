// Copyright (c) xxx, 2022. All rights reserved.

using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiDemo01.Models;

public class Product
{
    public long Id { get; set; }

    public string Name { get; set; } = String.Empty;

    [Column(TypeName = "decimal(8, 2)")]
    public decimal Price { get; set; }

    public string Category { get; set; } = String.Empty;
}
