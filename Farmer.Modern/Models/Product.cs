using System.ComponentModel.DataAnnotations;

namespace Farmer.Modern.Models;

public class Product
{
    [Key] public long Id { get; set; }

    [Display(Name = "نام محصول")] public string Name { get; set; }

    [Display(Name = "توضیحات")] public string? Description { get; set; }
}