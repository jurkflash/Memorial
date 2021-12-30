using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Memorial.Core.Domain
{
    public class PlotType : Base
    {
        public PlotType()
        {
            Plots = new HashSet<Plot>();
        }

        public byte Id { get; set; }

        public string Name { get; set; }

        public byte NumberOfPlacement { get; set; }

        public Boolean isFengShuiPlot { get; set; }

        public ICollection<Plot> Plots { get; set; }

    }
}