using System;
using System.ComponentModel.DataAnnotations;

namespace Memorial.Core.Dtos
{
    public class MiscellaneousDto
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

        public DateTime CreateDate { get; set; }
    }
}