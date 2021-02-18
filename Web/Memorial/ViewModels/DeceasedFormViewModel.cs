using System;
using System.Collections.Generic;
using System.Linq;
using Memorial.Core.Domain;
using Memorial.Core.Dtos;

namespace Memorial.ViewModels
{
    public class DeceasedFormViewModel
    {
        public IEnumerable<GenderType> GenderTypes { get; set; }
        public IEnumerable<MaritalType> MaritalTypes { get; set; }
        public IEnumerable<NationalityType> NationalityTypes { get; set; }
        public IEnumerable<RelationshipType> RelationshipTypes { get; set; }
        public IEnumerable<ReligionType> ReligionTypes { get; set; }
        public int ApplicantId { get; set; }
        public DeceasedDto DeceasedDto { get; set; }
    }
}