using System.ComponentModel.DataAnnotations;

namespace Memorial.Core.Dtos
{
    public class ColumbariumAreaDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

        public ColumbariumCentreDto ColumbariumCentreDto { get; set; }

        public int ColumbariumCentreDtoId { get; set; }
    }
}