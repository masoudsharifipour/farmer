using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Farmer.Modern.Models;

public class Category
{
    [Key]
    public long Id { get; set; }
    [DisplayName("نام دسته")]
    public string Name { get; set; }
    [DisplayName("توضیحات")]
    public string Description { get; set; }
}