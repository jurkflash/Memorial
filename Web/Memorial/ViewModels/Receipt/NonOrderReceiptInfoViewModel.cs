using Memorial.Core.Dtos;

namespace Memorial.ViewModels
{
    public class NonOrderReceiptInfoViewModel
    {
        public string AF { get; set; }

        public string SummaryItem { get; set; }

        public ReceiptDto ReceiptDto { get; set; }

        public bool ExportToPDF { get; set; }

        public string Header { get; set; }
    }
}