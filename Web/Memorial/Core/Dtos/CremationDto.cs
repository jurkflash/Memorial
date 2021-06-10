using Memorial.Core.Domain;

namespace Memorial.Core.Dtos
{
    public class CremationDto
    {
        public byte Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public SiteDto SiteDto { get; set; }

        public int SiteDtoId { get; set; }
    }
}