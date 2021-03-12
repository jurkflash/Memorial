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

        public ApplicantDto Applicant { get; set; }

        public int ApplicantId { get; set; }

        public DeceasedDto Deceased { get; set; }

        public int DeceasedId { get; set; }

        public RelationshipTypeDto RelationshipType { get; set; }

        public byte RelationshipTypeId { get; set; }

    }
}