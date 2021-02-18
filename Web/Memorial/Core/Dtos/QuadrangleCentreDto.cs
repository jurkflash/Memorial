﻿using System;
using System.Collections.Generic;
using Memorial.Core.Domain;

namespace Memorial.Core.Dtos
{
    public class QuadrangleCentreDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Site Site { get; set; }

        public byte SiteId { get; set; }

    }
}