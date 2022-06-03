using System.Collections.Generic;

namespace Farmer.Modern.Dto
{
    public class HomeDto
    {
        public WorkCount WorkCount { get; set; }
    }

    public class WorkCount
    {
        public int Todo { get; set; }
        public int InProgress { get; set; }
        public int Block { get; set; }
        public int Done { get; set; }

        public int All { get; set; }
    }
}

