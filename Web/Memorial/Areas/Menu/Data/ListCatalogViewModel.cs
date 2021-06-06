using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Dtos;

namespace Memorial.ViewModels
{
    public class ListCatalogViewModel
    {
        public IEnumerable<CatalogDto> CatalogDtos { get; set; }
        public int ApplicantId { get; set; }
    }
}