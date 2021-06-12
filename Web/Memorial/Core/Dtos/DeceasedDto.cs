using System;
using System.ComponentModel.DataAnnotations;

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

        [StringLength(255)]
        public string Remark { get; set; }

        public GenderTypeDto GenderTypeDto { get; set; }

        public byte GenderTypeDtoId { get; set; }

        public string Province { get; set; }

        public NationalityTypeDto NationalityTypeDto { get; set; }

        public byte NationalityTypeDtoId { get; set; }

        public MaritalTypeDto MaritalTypeDto { get; set; }

        public byte MaritalTypeDtoId { get; set; }

        public ReligionTypeDto ReligionTypeDto { get; set; }

        public byte ReligionTypeDtoId { get; set; }

        public string DeathPlace { get; set; }

        [Required]
        public DateTime DeathDate { get; set; }

        public string DeathRegistrationCentre { get; set; }

        public string DeathCertificate { get; set; }

        public string BurialCertificate { get; set; }

        public string ImportPermitNumber { get; set; }

    }
}