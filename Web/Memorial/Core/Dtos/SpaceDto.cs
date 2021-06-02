using Memorial.Core.Domain;

namespace Memorial.Core.Dtos
{
    public class SpaceDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Remark { get; set; }

        public SiteDto SiteDto { get; set; }

        public byte SiteDtoId { get; set; }

        public string ColorCode { get; set; }
    }
}