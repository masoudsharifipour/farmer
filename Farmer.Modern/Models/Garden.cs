using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Farmer.Modern.Models;

public class Garden
{
    [Key] public long Id { get; set; }
    [DisplayName("نام زمین")] public string Name { get; set; }
    [DisplayName("کد زمین")] public string Code { get; set; }
    [DisplayName("هکتار")] public int Size { get; set; }

    /// <summary>
    /// نوع کشت
    /// </summary>
    [DisplayName("نوع کشت")]
    public string Cultivation { get; set; }

    [DisplayName("مقدار جی پی اس")] public string Gps { get; set; }
    public long CreatorUserId { get; set; }

    [DisplayName(" تاریخ ثبت")]
    [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
    public DateTime CreationDateTime { get; set; }

    [DisplayName("توضیحات")] public string Description { get; set; }
    [DisplayName("وضعیت زمین")] public bool? IsActive { get; set; }
}