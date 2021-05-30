using Memorial.Core.Dtos;

namespace Memorial.ViewModels
{
    public class ColumbariumTransactionsInfoViewModel
    {
        public ColumbariumTransactionDto ColumbariumTransactionDto { get; set; }

        public string ItemName { get; set; }

        public NicheDto NicheDto { get; set; }

        public ColumbariumCentreDto ColumbariumCentreDto { get; set; }

        public float TotalAmount { get; set; }

        public int ApplicantId { get; set; }

        public int? DeceasedId { get; set; }

        public bool ExportToPDF { get; set; }

        public string Header { get; set; }

    }
}