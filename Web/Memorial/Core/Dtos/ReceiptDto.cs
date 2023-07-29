using System;
using System.ComponentModel.DataAnnotations;
using Memorial.Core.Domain;

namespace Memorial.Core.Dtos
{
    public class ReceiptDto
    {
        public string RE { get; set; }

        public InvoiceDto InvoiceDto { get; set; }

        public string InvoiceDtoIV { get; set; }

        public CemeteryTransaction CemeteryTransaction { get; set; }

        public string CemeteryTransactionAF { get; set; }

        public CremationTransaction CremationTransaction { get; set; }

        public string CremationTransactionAF { get; set; }

        public AncestralTabletTransaction AncestralTabletTransaction { get; set; }

        public string AncestralTabletTransactionAF { get; set; }

        public MiscellaneousTransaction MiscellaneousTransaction { get; set; }

        public string MiscellaneousTransactionAF { get; set; }

        public ColumbariumTransaction ColumbariumTransaction { get; set; }

        public string ColumbariumTransactionAF { get; set; }

        public SpaceTransaction SpaceTransaction { get; set; }

        public string SpaceTransactionAF { get; set; }

        public UrnTransaction UrnTransaction { get; set; }

        public string UrnTransactionAF { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        public byte PaymentMethodId { get; set; }

        public string PaymentRemark { get; set; }

        public bool isDeposit { get; set; }

        [Required]
        public float Amount { get; set; }

        [MaxLength(255)]
        public string Remark { get; set; }

        public DateTime CreatedUtcTime { get; set; }

    }
}