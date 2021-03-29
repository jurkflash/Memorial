using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Memorial.Core.Domain
{
    public class RelationshipType
    {
        public RelationshipType()
        {
            ApplicantDeceaseds = new HashSet<ApplicantDeceased>();
        }

        public byte Id { get; set; }

        public string Name { get; set; }

        public ICollection<ApplicantDeceased> ApplicantDeceaseds { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? ModifyDate { get; set; }

        public DateTime? DeleteDate { get; set; }

    }
}