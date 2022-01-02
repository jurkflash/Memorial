using System;
using System.ComponentModel.DataAnnotations;
using Memorial.Core.Domain;

namespace Memorial.Core.Dtos
{
    public class InvoiceDto
    {
        public string IV { get; set; }

        public float Amount { get; set; }

        public Boolean isPaid { get; set; }

        public Boolean hasReceipt { get; set; }

        [StringLength(255)]
        public string Remark { get; set; }

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

        public bool AllowDeposit { get; set; }

        public DateTime CreatedDate { get; set; }

    }
}