using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Memorial.Core.Dtos
{
    public class NationalityTypeDto
    {
        public byte Id { get; set; }

        public string Name { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? ModifyDate { get; set; }

        public DateTime? DeleteDate { get; set; }

    }
}