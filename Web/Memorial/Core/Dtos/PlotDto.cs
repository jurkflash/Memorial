﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Memorial.Core.Dtos
{
    public class PlotDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Size { get; set; }

        public float Price { get; set; }

        public float Maintenance { get; set; }

        public float Wall { get; set; }

        public float Dig { get; set; }

        public float Brick { get; set; }

        public string Remark { get; set; }

        public PlotTypeDto PlotTypeDto { get; set; }

        public byte PlotTypeDtoId { get; set; }

        public int PlotAreaId { get; set; }

        public int? ApplicantId { get; set; }

        public bool hasDeceased { get; set; }

        public DateTime CreateDate { get; set; }

    }
}