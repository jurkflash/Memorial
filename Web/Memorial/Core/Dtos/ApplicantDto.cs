using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Memorial.Core.Dtos
{
    public class ApplicantDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string IC { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public string Name2 { get; set; }

        public string Address { get; set; }

        public string HousePhone { get; set; }

        public string MobileNumber { get; set; }

        public string Remark { get; set; }
    }
}