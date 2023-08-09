using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Memorial.Core.Domain
{
    public class ApplicantDeceasedFlatten
    {
        public int Id { get; set; }

        public int ApplicantId { get; set; }

        public string ApplicantName { get; set; }

        public string ApplicantName2 { get; set; }

        public int DeceasedId { get; set; }

        public string DeceasedName { get; set; }

        public string DeceasedName2 { get; set; }

        public byte RelationshipTypeId { get; set; }

        public string RelationshipTypeName { get; set; }

    }
}