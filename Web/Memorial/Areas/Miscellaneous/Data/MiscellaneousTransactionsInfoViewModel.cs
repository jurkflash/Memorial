using Memorial.Core.Dtos;

namespace Memorial.ViewModels
{
    public class MiscellaneousTransactionsInfoViewModel
    {
        public MiscellaneousTransactionDto MiscellaneousTransactionDto { get; set; }

        public bool ExportToPDF { get; set; }

        public string Header { get; set; }
    }
}