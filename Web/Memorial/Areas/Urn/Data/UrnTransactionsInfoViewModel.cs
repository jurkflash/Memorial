using Memorial.Core.Dtos;

namespace Memorial.ViewModels
{
    public class UrnTransactionsInfoViewModel
    {
        public UrnTransactionDto UrnTransactionDto { get; set; }

        public bool ExportToPDF { get; set; }

        public string Header { get; set; }
    }
}