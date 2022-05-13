using System;
using Farmer.Modern.Models;
using Category = Farmer.Modern.Migrations.Category;
using Garden = Farmer.Modern.Migrations.Garden;
using Product = Farmer.Modern.Migrations.Product;

namespace Farmer.Modern.Dto;

public class WorkDto
{
    public long Id { get; set; }
    public Garden Garden { get; set; }
    public long GardenId { get; set; }
    public string GardenName{ get; set; }
    public Product Product { get; set; }
    public long ProductId { get; set; }
    public string ProductName { get; set; }
    public Category Category { get; set; }
    public long CategoryId { get; set; }
    public string CategoryName { get; set; }
    public WaterMotor? WaterMotor { get; set; }
    public long? WaterMotorId { get; set; }
    public string WaterMotorName { get; set; }
    public DateTime ActionDatetime { get; set; }
    public int? Size { get; set; }
    public string Type { get; set; }
    public string Description { get; set; }
    public Guid? CreatorUserId { get; set; }
    public string CreatorFullName { get; set; }
    public Guid? Agent { get; set; }
    public string FullNameAgent { get; set; }
    public DateTime? EndActionDateTime { get; set; }
    public ActionStatus Status { get; set; }
}