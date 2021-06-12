using System.ComponentModel.DataAnnotations;

namespace Memorial.Core.Dtos
{
    public class AncestralTabletDto
    {
        public int Id { get; set; }

        public byte PositionX { get; set; }

        public byte PositionY { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public float Price { get; set; }

        public float Maintenance { get; set; }

        [StringLength(255)]
        public string Remark { get; set; }

        public ApplicantDto ApplicantDto { get; set; }

        public int? ApplicantDtoId { get; set; }

        public AncestralTabletAreaDto AncestralTabletAreaDto { get; set; }

        public int AncestralTabletAreaDtoId { get; set; }

        public bool hasDeceased { get; set; }

        public bool hasFreeOrder { get; set; }

    }
}