using Memorial.Core.Domain;

namespace Memorial.Core.Dtos
{
    public class CremationNumberDto
    {
        public int Id { get; set; }

        public CremationItem CremationItem { get; set; }

        public int CremationItemId { get; set; }

        public int Year { get; set; }

        public int AF { get; set; }

        public int PO { get; set; }

        public int IV { get; set; }

        public int RE { get; set; }
    }
}