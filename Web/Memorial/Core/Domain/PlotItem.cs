using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Memorial.Core.Domain
{
    public class PlotItem
    {
        public PlotItem()
        {
            PlotNumbers = new HashSet<PlotNumber>();

            PlotTransactions = new HashSet<PlotTransaction>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public float Price { get; set; }

        public string Code { get; set; }

        public string SystemCode { get; set; }

        public bool isOrder { get; set; }

        public PlotArea PlotArea { get; set; }

        public int PlotAreaId { get; set; }

        public ICollection<PlotNumber> PlotNumbers { get; set; }

        public ICollection<PlotTransaction> PlotTransactions { get; set; }
    }
}