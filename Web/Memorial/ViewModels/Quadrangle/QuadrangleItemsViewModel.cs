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

        public int QuadrangleId { get; set; }

        public string QuadrangleName { get; set; }

        public string QuadrangleDescription { get; set; }

        public int applicantId { get; set; }
    }
}