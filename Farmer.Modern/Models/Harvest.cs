using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Farmer.Modern.Models
{
    public class Harvest
    {
        [Key] public long Id { get; set; }
        public Product Product { get; set; }
        public long ProductId { get; set; }
        public Garden Garden { get; set; }
        public long GardenId { get; set; }
        [DisplayName("مقدار حجم برداشت")] public int Size { get; set; }

        [DisplayName("تاریخ برداشت")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime HarvestDate { get; set; }

        [DisplayName("توضیحات")] public string? Description { get; set; }
    }
}

