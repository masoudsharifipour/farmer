#nullable enable
using System;
using System.ComponentModel.DataAnnotations;

namespace Farmer.Modern.Models;

public class Work
{
    [Key] public long Id { get; set; }
    public Garden? Garden { get; set; }
    public long GardenId { get; set; }
    public Product? Product { get; set; }
    public long ProductId { get; set; }
    public Category? Category { get; set; }
    public long CategoryId { get; set; }
    public WaterMotor? WaterMotor { get; set; }
    public long? WaterMotorId { get; set; }
    public DateTime ActionDatetime { get; set; }
    public int? Size { get; set; }
    public string? Type { get; set; }
    public string? Description { get; set; }
    public Guid? CreatorUserId { get; set; }
    
    public Guid? AgentId { get; set; }
    public DateTime? EndActionDateTime { get; set; }
    public ActionStatus Status { get; set; }

    public DateTime CreationDatetime { get; set; } = DateTime.Now;
}

public enum ActionStatus
{
    Todo = 1,
    InProgress = 2,
    Block = 3,
    Done = 4
}