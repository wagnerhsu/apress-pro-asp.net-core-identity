// Copyright (c) xxx, 2022. All rights reserved.

using System.ComponentModel.DataAnnotations;

namespace WebApiDemo01.Models;

public class ProductBindingTarget
{
    [Required]
    public string Name { get; set; } = String.Empty;

    [Required]
    public decimal Price { get; set; }

    [Required]
    public string Category { get; set; } = String.Empty;
}
