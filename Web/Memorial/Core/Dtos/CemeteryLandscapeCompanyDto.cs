using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Memorial.Core.Dtos
{
    public class CemeteryLandscapeCompanyDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ContactPerson { get; set; }

        public string ContactNumber { get; set; }

        public string Remark { get; set; }

        public DateTime CreateDate { get; set; }
    }
}