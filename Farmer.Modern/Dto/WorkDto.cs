using System;
using System.ComponentModel;
using Farmer.Modern.Models;
using Category = Farmer.Modern.Migrations.Category;
using Garden = Farmer.Modern.Migrations.Garden;
using Product = Farmer.Modern.Migrations.Product;

namespace Farmer.Modern.Dto;

public class WorkDto
{
    public long Id { get; set; }
    [DisplayName("نام زمین")]
    public Garden Garden { get; set; }
    [DisplayName("نام زمین")]
    public long GardenId { get; set; }
    [DisplayName("نام زمین")]
    public string GardenName{ get; set; }
    [DisplayName("نام محصول")]
    public Product Product { get; set; }
    [DisplayName("نام محصول")]
    public long ProductId { get; set; }
    [DisplayName("نام محصول")]
    public string ProductName { get; set; }
    [DisplayName("نوع عملیات")]
    public Category Category { get; set; }
    [DisplayName("نوع عملیات")]
    public long CategoryId { get; set; }
    [DisplayName("نوع عملیات")]
    public string CategoryName { get; set; }
    [DisplayName("نام موتور")]
    public WaterMotor? WaterMotor { get; set; }
    [DisplayName("نام موتور")]
    public long? WaterMotorId { get; set; }
    [DisplayName("نام موتور")]
    public string WaterMotorName { get; set; }
    [DisplayName("تاریخ انجام")]
    public DateTime ActionDatetime { get; set; }
    [DisplayName("حجم")]
    public int? Size { get; set; }
    [DisplayName("نوع")]
    public string Type { get; set; }
    [DisplayName("توضیحات")]
    public string Description { get; set; }
    [DisplayName("کاربر")]
    public Guid? CreatorUserId { get; set; }
    [DisplayName("کاربر ثبت کننده")]
    public string CreatorFullName { get; set; }
    [DisplayName("کاربر انجام دهنده")]
    public Guid? Agent { get; set; }
    [DisplayName("کاربر انجام دهنده")]
    public string FullNameAgent { get; set; }
    [DisplayName("پایان انجام کار")]
    public DateTime? EndActionDateTime { get; set; }
    [DisplayName("وضعیت")]
    public ActionStatus Status { get; set; }
}