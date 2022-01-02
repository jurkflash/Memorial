using System;

namespace Memorial.Core.Dtos
{
    public class PlotTypeDto
    {
        public byte Id { get; set; }

        public string Name { get; set; }

        public byte NumberOfPlacement { get; set; }

        public Boolean isFengShuiPlot { get; set; }

        public DateTime CreatedDate { get; set; }

    }
}