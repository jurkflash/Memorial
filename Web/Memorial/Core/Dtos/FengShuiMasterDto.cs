﻿using System.ComponentModel.DataAnnotations;

namespace Memorial.Core.Dtos
{
    public class FengShuiMasterDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [StringLength(255)]
        public string ContactPerson { get; set; }

        [StringLength(255)]
        public string ContactNumber { get; set; }

        public string Remark { get; set; }

    }
}