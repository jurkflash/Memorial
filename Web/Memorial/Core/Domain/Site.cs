using System;
using System.Collections.Generic;

namespace Memorial.Core.Domain
{
    public class Site : Base
    {
        public Site()
        {
            Applicants = new HashSet<Applicant>();

            CemeteryAreas = new HashSet<CemeteryArea>();

            AncestralTabletAreas = new HashSet<AncestralTabletArea>();

            Spaces = new HashSet<Space>();

            Miscellaneous = new HashSet<Miscellaneous>();

            ColumbariumCentres = new HashSet<ColumbariumCentre>();

            Urns = new HashSet<Urn>();

            Catalogs = new HashSet<Catalog>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public string Address { get; set; }

        public string Header { get; set; }

        public string Remark { get; set; }

        public ICollection<Applicant> Applicants { get; set; }

        public ICollection<CemeteryArea> CemeteryAreas { get; set; }

        public ICollection<AncestralTabletArea> AncestralTabletAreas { get; set; }

        public ICollection<Space> Spaces { get; set; }

        public ICollection<Miscellaneous> Miscellaneous { get; set; }

        public ICollection<ColumbariumCentre> ColumbariumCentres { get; set; }

        public ICollection<Cremation> Cremations { get; set; }

        public ICollection<Urn> Urns { get; set; }

        public ICollection<Catalog> Catalogs { get; set; }
    }
}