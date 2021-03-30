﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Memorial.Core.Domain
{
    public class PlotLandscapeCompany
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ContactPerson { get; set; }

        public string ContactNumber { get; set; }

        public string Remark { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? ModifyDate { get; set; }

        public DateTime? DeleteDate { get; set; }
    }
}