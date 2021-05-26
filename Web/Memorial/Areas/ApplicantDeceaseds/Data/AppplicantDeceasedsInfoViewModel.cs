using System.Collections.Generic;
using Memorial.Core.Dtos;

namespace Memorial.ViewModels
{
    public class AppplicantDeceasedsInfoViewModel
    {
        public int? ApplicantId { get; set; }

        public string RelationshipTypeName { get; set; }

        public int? DeceasedId { get; set; }

        public bool ShowApplicant { get; set; }
    }
}