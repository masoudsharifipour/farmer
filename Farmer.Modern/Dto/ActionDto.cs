using Farmer.Modern.Models;

namespace Farmer.Modern.Dto;

public class ActionDto
{
    public ActionStatus Status { get; set; }
    public DateTime FinishDataTime { get; set; } = DateTime.Now;
}