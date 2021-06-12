using System;
using System.ComponentModel.DataAnnotations;

namespace Memorial.Core.Dtos
{
    public class SiteDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [StringLength(5)]
        public string Code { get; set; }

        public string Address { get; set; }

        public string Remark { get; set; }

        [Required]
        public string Header { get; set; }

        public DateTime CreateDate { get; set; }
    }
}