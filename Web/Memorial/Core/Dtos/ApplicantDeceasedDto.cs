using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Memorial.Core.Dtos
{
    public class ApplicantDeceasedDto
    {
        public int Id { get; set; }

        public ApplicantDto ApplicantDto { get; set; }

        public int ApplicantDtoId { get; set; }

        public DeceasedDto DeceasedDto { get; set; }

        public int DeceasedDtoId { get; set; }

        public RelationshipTypeDto RelationshipTypeDto { get; set; }

        public byte RelationshipTypeDtoId { get; set; }

    }
}