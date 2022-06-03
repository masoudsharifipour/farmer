using Farmer.Modern.Models;
using System;

namespace Farmer.Modern.Dto
{
    public class ActionDto
    {
        public ActionStatus Status { get; set; }
        public DateTime FinishDataTime { get; set; } = DateTime.Now;
    }
}

