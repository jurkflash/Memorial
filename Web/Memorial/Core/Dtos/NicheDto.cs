using System.ComponentModel.DataAnnotations;

namespace Memorial.Core.Dtos
{
    public class NicheDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(100)]
        public string Description { get; set; }

        public byte PositionX { get; set; }

        public byte PositionY { get; set; }

        public float Price { get; set; }

        public float Maintenance { get; set; }

        public float LifeTimeMaintenance { get; set; }

        [StringLength(255)]
        public string Remark { get; set; }

        public NicheTypeDto NicheTypeDto { get; set; }

        public byte NicheTypeDtoId { get; set; }

        public ColumbariumAreaDto ColumbariumAreaDto { get; set; }

        public int ColumbariumAreaDtoId { get; set; }

        public ApplicantDto ApplicantDto { get; set; }

        public int? ApplicantDtoId { get; set; }

        public bool hasDeceased { get; set; }

        public bool hasFreeOrder { get; set; }

    }
}