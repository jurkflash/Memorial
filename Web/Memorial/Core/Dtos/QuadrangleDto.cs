using System;
using System.Collections.Generic;
using Memorial.Core.Domain;

namespace Memorial.Core.Dtos
{
    public class QuadrangleDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public byte PositionX { get; set; }

        public byte PositionY { get; set; }

        public float Price { get; set; }

        public float Maintenance { get; set; }

        public float LifeTimeMaintenance { get; set; }

        public string Remark { get; set; }

        public QuadrangleTypeDto QuadrangleTypeDto { get; set; }

        public byte QuadrangleTypeDtoId { get; set; }

        public QuadrangleAreaDto QuadrangleAreaDto { get; set; }

        public int QuadrangleAreaDtoId { get; set; }

        public int? ApplicantDtoId { get; set; }

        public bool hasDeceased { get; set; }

        public bool hasFreeOrder { get; set; }

    }
}