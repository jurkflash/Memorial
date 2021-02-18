using Memorial.Core.Domain;

namespace Memorial.Core.Dtos
{
    public class AncestorDto
    {
        public int Id { get; set; }

        public byte PositionX { get; set; }

        public byte PositionY { get; set; }

        public string Code { get; set; }

        public float Price { get; set; }

        public float Maintenance { get; set; }

        public string Remark { get; set; }

        public bool isPurchased { get; set; }

        public AncestorArea AncestorArea { get; set; }

        public int AncestorAreaId { get; set; }

    }
}