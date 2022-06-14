using System;
using System.ComponentModel;
using Farmer.Modern.Models;
using Category = Farmer.Modern.Migrations.Category;
using Garden = Farmer.Modern.Migrations.Garden;
using Product = Farmer.Modern.Migrations.Product;
using System.Collections.Generic;

namespace Farmer.Modern.Dto
{
    public class WorkInputDto
    {
        public long Id { get; set; }
        [DisplayName("نام زمین")] 
        public Garden? Garden { get; set; }
        [DisplayName("نام زمین")]
   
        public long GardenId { get; set; }

        [DisplayName("نام محصول")] public Product? Product { get; set; }
        [DisplayName("نام محصول")] public long ProductId { get; set; }

        [DisplayName("نوع عملیات")] public List<CategoryDto> Category { get; set; }
        [DisplayName("نوع عملیات")] public long? CategoryId { get; set; }

        [DisplayName("نام موتور")] public WaterMotor? WaterMotor { get; set; }
        [DisplayName("نام موتور")] public long? WaterMotorId { get; set; }

        [DisplayName("تاریخ انجام")] public DateTime ActionDateTime { get; set; }
        [DisplayName("حجم")] public int? Size { get; set; }
        [DisplayName("نوع")] public string Type { get; set; }
        [DisplayName("توضیحات")] public string Description { get; set; }
        [DisplayName("کاربر")] public Guid? CreatorUserId { get; set; }

        [DisplayName("کاربر انجام دهنده")] public Guid? Agent { get; set; }

        [DisplayName("پایان انجام کار")] public DateTime? EndActionDateTime { get; set; }
        [DisplayName("وضعیت")] public ActionStatus Status { get; set; }
    }
}

