namespace Farmer.Modern.Models;

public class Experiment
{
    public long Id { get; set; }
    public Garden Garden { get; set; }
    public WaterMotor WaterMotor { get; set; }
    public string Description { get; set; }
    public string Result { get; set; }
}