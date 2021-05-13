using Memorial.Core.Domain;

namespace Memorial.Core.Dtos
{
    public class AncestorDto
    {
        public int Id { get; set; }

        public byte PositionX { get; set; }

        public byte PositionY { get; set; }

        public string Name { get; set; }

        public float Price { get; set; }

        public float Maintenance { get; set; }

        public string Remark { get; set; }

        public int? ApplicantId { get; set; }

        public int AncestorAreaId { get; set; }

        public bool hasDeceased { get; set; }

        public bool hasFreeOrder { get; set; }

    }
}