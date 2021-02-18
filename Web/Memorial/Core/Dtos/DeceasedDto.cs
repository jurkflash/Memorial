using System;
using System.ComponentModel.DataAnnotations;
using Memorial.Core.Domain;

namespace Memorial.Core.Dtos
{
    public class DeceasedDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string IC { get; set; }

        [Required]
        [StringLength(255)]
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

        [Required]
        public DateTime DeathDate { get; set; }

        public string DeathRegistrationCentre { get; set; }

        public string DeathCertificate { get; set; }

        public string BurialCertificate { get; set; }

        public string ImportPermitNumber { get; set; }

    }
}