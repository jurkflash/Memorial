using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Memorial.Core.Domain
{
    public class Plot : Base
    {
        public Plot()
        {
            Deceaseds = new HashSet<Deceased>();

            CemeteryTransactions = new HashSet<CemeteryTransaction>();

            CemeteryTrackings = new HashSet<CemeteryTracking>();

            CemeteryItems = new HashSet<CemeteryItem>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Size { get; set; }

        public float Price { get; set; }

        public float Maintenance { get; set; }

        public float Wall { get; set; }

        public float Dig { get; set; }

        public float Brick { get; set; }

        public string Remark { get; set; }

        public PlotType PlotType { get; set; }

        public byte PlotTypeId { get; set; }

        public CemeteryArea CemeteryArea { get; set; }

        public int CemeteryAreaId { get; set; }

        public Applicant Applicant { get; set; }

        public int? ApplicantId { get; set; }

        public bool hasDeceased { get; set; }

        public bool hasCleared { get; set; }

        public ICollection<Deceased> Deceaseds { get; set; }

        public ICollection<CemeteryTransaction> CemeteryTransactions { get; set; }

        public ICollection<CemeteryTracking> CemeteryTrackings { get; set; }

        public ICollection<CemeteryItem> CemeteryItems { get; set; }
    }
}