using System;
using System.Collections.Generic;
using System.Linq;
using Memorial.Core.Domain;
using Memorial.Core.Dtos;

namespace Memorial.ViewModels
{
    public class DeceasedFormViewModel
    {
        public IEnumerable<GenderTypeDto> GenderTypeDtos { get; set; }

        public IEnumerable<MaritalTypeDto> MaritalTypeDtos { get; set; }

        public IEnumerable<NationalityTypeDto> NationalityTypeDtos { get; set; }

        public IEnumerable<RelationshipTypeDto> RelationshipTypeDtos { get; set; }

        public IEnumerable<ReligionTypeDto> ReligionTypeDtos { get; set; }

        public int ApplicantId { get; set; }

        public DeceasedDto DeceasedDto { get; set; }

        public byte RelationshipTypeId { get; set; }
    }
}