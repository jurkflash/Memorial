using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Memorial.Core.Dtos
{
    public class PlotTypeDto
    {
        public byte Id { get; set; }

        public string Name { get; set; }

        public byte NumberOfPlacement { get; set; }

        public Boolean isFengShuiPlot { get; set; }

        public string Code { get; set; }

        public DateTime CreateDate { get; set; }

    }
}