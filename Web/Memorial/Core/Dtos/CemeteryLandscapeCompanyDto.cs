using System;
using System.ComponentModel.DataAnnotations;

namespace Memorial.Core.Dtos
{
    public class CemeteryLandscapeCompanyDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [StringLength(255)]
        public string ContactPerson { get; set; }

        [StringLength(255)]
        public string ContactNumber { get; set; }

        [StringLength(255)]
        public string Remark { get; set; }

        public DateTime CreateDate { get; set; }
    }
}