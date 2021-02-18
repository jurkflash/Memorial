using Memorial.Core.Domain;
namespace Memorial.Core.Dtos
{
    public class AncestorNumberDto
    {
        public int Id { get; set; }

        public AncestorItem AncestorItem { get; set; }

        public int AncestorItemId { get; set; }

        public int Year { get; set; }

        public int AF { get; set; }

        public int PO { get; set; }

        public int IV { get; set; }

        public int RE { get; set; }
    }
}