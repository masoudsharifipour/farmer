using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Farmer.Modern.Models;

public class Experiment
{
    [Key] public long Id { get; set; }
    [DisplayName("نام زمین")] public Garden Garden { get; set; }
    public long GardenId { get; set; }
    [DisplayName("نام موتور")] public WaterMotor WaterMotor { get; set; }
    public long WaterMotorId { get; set; }
    [DisplayName("نتیجه")] public string Result { get; set; }
    [DisplayName("توضیحات")] public string Description { get; set; }
}