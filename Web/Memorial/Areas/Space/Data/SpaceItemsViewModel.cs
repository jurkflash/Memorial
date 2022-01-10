using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Dtos;

namespace Memorial.ViewModels
{
    public class SpaceItemsViewModel
    {
        public IEnumerable<SpaceItemDto> SpaceItemDtos { get; set; }
        public int ApplicantId { get; set; }
        public SpaceDto SpaceDto { get; set; }
    }
}