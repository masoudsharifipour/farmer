using System.Collections.Generic;
using System.ComponentModel;
using Farmer.Modern.Models;

namespace Farmer.Modern.Dto
{
    public class ExperimentDto
    {
        public long Id { get; set; }
        [DisplayName("نام زمین")] public long Garden { get; set; }
        [DisplayName("نام موتور")] public long WaterMotor { get; set; }
        [DisplayName("نتیجه")] public string Result { get; set; }
        [DisplayName("توضیحات")] public string Description { get; set; }

    }
}

