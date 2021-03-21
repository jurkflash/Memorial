using System;
using System.Collections.Generic;
using Memorial.Core.Domain;

namespace Memorial.Core.Dtos
{
    public class QuadrangleAreaDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public QuadrangleCentreDto QuadrangleCentreDto { get; set; }

        public int QuadrangleCentreDtoId { get; set; }
    }
}