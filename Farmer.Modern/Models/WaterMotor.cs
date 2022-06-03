using System.ComponentModel.DataAnnotations;

namespace Farmer.Modern.Models
{
    public class WaterMotor
    {
        [Key] public long Id { get; set; }

        [Display(Name = "نام موتور")] public string Name { get; set; }

        [Display(Name = "توضیحات")] public string? Description { get; set; }
    }
}

