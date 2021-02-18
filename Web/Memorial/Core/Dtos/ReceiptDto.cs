using System;
using System.ComponentModel.DataAnnotations;
using Memorial.Core.Domain;

namespace Memorial.Core.Dtos
{
    public class ReceiptDto
    {
        public string RE { get; set; }

        public Invoice Invoice { get; set; }

        public string InvoiceIV { get; set; }

        public PlotTransaction PlotTransaction { get; set; }

        public string PlotTransactionAF { get; set; }

        public CremationTransaction CremationTransaction { get; set; }

        public string CremationTransactionAF { get; set; }

        public AncestorTransaction AncestorTransaction { get; set; }

        public string AncestorTransactionAF { get; set; }

        public MiscellaneousTransaction MiscellaneousTransaction { get; set; }

        public string MiscellaneousTransactionAF { get; set; }

        public QuadrangleTransaction QuadrangleTransaction { get; set; }

        public string QuadrangleTransactionAF { get; set; }

        public SpaceTransaction SpaceTransaction { get; set; }

        public string SpaceTransactionAF { get; set; }

        public UrnTransaction UrnTransaction { get; set; }

        public string UrnTransactionAF { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        public byte PaymentMethodId { get; set; }

        public string PaymentRemark { get; set; }

        [Required]
        public float Amount { get; set; }

        [MaxLength(255)]
        public string Remark { get; set; }

        public DateTime CreateDate { get; set; }

    }
}