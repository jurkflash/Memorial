using System.ComponentModel.DataAnnotations;

namespace Memorial.Core.Dtos
{
    public class SpaceDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

        [StringLength(255)]
        public string Remark { get; set; }

        public SiteDto SiteDto { get; set; }

        public int SiteDtoId { get; set; }

        [Required]
        [StringLength(7, MinimumLength = 7)]
        public string ColorCode { get; set; }
    }
}