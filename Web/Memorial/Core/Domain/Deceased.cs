using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Memorial.Core.Domain
{
    public class Deceased
    {
        public Deceased()
        {
            SpaceTransactions = new HashSet<SpaceTransaction>();

            CremationTransactions = new HashSet<CremationTransaction>();

            AncestorTransactions = new HashSet<AncestorTransaction>();

            PlotTransactions = new HashSet<PlotTransaction>();
        }

        public int Id { get; set; }

        public string IC { get; set; }

        public string Name { get; set; }

        public string Name2 { get; set; }

        public byte Age { get; set; }

        public string Address { get; set; }

        public string Remark { get; set; }

        public GenderType GenderType { get; set; }

        public byte GenderTypeId { get; set; }

        public string Province { get; set; }

        public NationalityType NationalityType { get; set; }

        public byte NationalityTypeId { get; set; }

        public MaritalType MaritalType { get; set; }

        public byte MaritalTypeId { get; set; }

        public ReligionType ReligionType { get; set; }

        public byte ReligionTypeId { get; set; }

        public RelationshipType RelationshipType { get; set; }

        public byte RelationshipTypeId { get; set; }

        public Applicant Applicant { get; set; }

        public int ApplicantId { get; set; }

        public string DeathPlace { get; set; }

        public DateTime DeathDate { get; set; }

        public string DeathRegistrationCentre { get; set; }

        public string DeathCertificate { get; set; }

        public string BurialCertificate { get; set; }

        public string ImportPermitNumber { get; set; }

        public Quadrangle Quadrangle { get; set; }

        public int? QuadrangleId { get; set; }

        public Plot Plot { get; set; }

        public int? PlotId { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? ModifyDate { get; set; }

        public DateTime? DeleteDate { get; set; }

        public ICollection<SpaceTransaction> SpaceTransactions { get; set; }

        public ICollection<CremationTransaction> CremationTransactions { get; set; }

        public ICollection<AncestorTransaction> AncestorTransactions { get; set; }

        public ICollection<PlotTransaction> PlotTransactions { get; set; }

    }
}