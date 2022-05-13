using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Farmer.Modern.Dto;

public class HarvestDto
{
    [Key] public long Id { get; set; }
    public long Product { get; set; }
    public long Garden { get; set; }
    [DisplayName("مقدار حجم برداشت")] public int Size { get; set; }

    [DisplayName("تاریخ برداشت")]
    [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
    public DateTime HarvestDate { get; set; }

    [DisplayName("توضیحات")] public string Description { get; set; }
}