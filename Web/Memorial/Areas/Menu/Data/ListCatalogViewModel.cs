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
        public byte SiteId { get; set; }
        public SiteDto SiteDto { get; set; }
        public int? ApplicantId { get; set; }
    }
}