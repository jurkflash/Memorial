using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Memorial.Core.Domain
{
    public class PlotArea
    {
        public PlotArea()
        {
            Plots = new HashSet<Plot>();

            PlotItems = new HashSet<PlotItem>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Site Site { get; set; }

        public byte SiteId { get; set; }

        public ICollection<Plot> Plots { get; set; }

        public ICollection<PlotItem> PlotItems { get; set; }
    }
}