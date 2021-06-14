using System.ComponentModel.DataAnnotations;

namespace Memorial.Core.Dtos
{
    public class CremationDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

        public SiteDto SiteDto { get; set; }

        public int SiteDtoId { get; set; }
    }
}