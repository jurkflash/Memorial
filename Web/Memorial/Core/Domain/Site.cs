using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Memorial.Core.Domain
{
    public class Site
    {
        public Site()
        {
            PlotAreas = new HashSet<PlotArea>();

            AncestorAreas = new HashSet<AncestorArea>();

            Spaces = new HashSet<Space>();

            Miscellaneous = new HashSet<Miscellaneous>();

            QuadrangleCentres = new HashSet<QuadrangleCentre>();

            Urns = new HashSet<Urn>();

            Catalogs = new HashSet<Catalog>();
        }

        public byte Id { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public string Address { get; set; }

        public string Remark { get; set; }

        public ICollection<PlotArea> PlotAreas { get; set; }

        public ICollection<AncestorArea> AncestorAreas { get; set; }

        public ICollection<Space> Spaces { get; set; }

        public ICollection<Miscellaneous> Miscellaneous { get; set; }

        public ICollection<QuadrangleCentre> QuadrangleCentres { get; set; }

        public ICollection<Cremation> Cremations { get; set; }

        public ICollection<Urn> Urns { get; set; }

        public ICollection<Catalog> Catalogs { get; set; }
    }
}