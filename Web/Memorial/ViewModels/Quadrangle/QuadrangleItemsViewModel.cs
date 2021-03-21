using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Domain;
using Memorial.Core.Dtos;

namespace Memorial.ViewModels
{
    public class QuadrangleItemsViewModel
    {
        public IEnumerable<QuadrangleItemDto> QuadrangleItemDtos { get; set; }

        public QuadrangleDto QuadrangleDto { get; set; }

        public ApplicantDto ApplicantDto { get; set; }

    }
}