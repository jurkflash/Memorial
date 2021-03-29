using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Domain;
using Memorial.Core.Dtos;

namespace Memorial.ViewModels
{
    public class AncestorItemsViewModel
    {
        public IEnumerable<AncestorItemDto> AncestorItemDtos { get; set; }

        public AncestorDto AncestorDto { get; set; }

        public ApplicantDto ApplicantDto { get; set; }

    }
}