using System.Data.Entity.ModelConfiguration;
using Memorial.Core.Domain;

namespace Memorial.Persistence.EntityConfigurations
{
    public class ReceiptConfiguration : EntityTypeConfiguration<Receipt>
    {
        public ReceiptConfiguration()
        {
            Property(r => r.RE)
                .IsRequired()
                .HasMaxLength(50);

            Property(r => r.Remark)
                .HasMaxLength(255);

            HasKey(r => r.RE);

            HasOptional(r => r.Invoice)
                .WithMany(i => i.Receipts)
                .HasForeignKey(r => r.InvoiceIV)
                .WillCascadeOnDelete(false);

            HasOptional(r => r.AncestorTransaction)
                .WithMany(at => at.Receipts)
                .HasForeignKey(r => r.AncestorTransactionAF)
                .WillCascadeOnDelete(false);

            HasOptional(r => r.CremationTransaction)
                .WithMany(ct => ct.Receipts)
                .HasForeignKey(r => r.CremationTransactionAF)
                .WillCascadeOnDelete(false);

            HasOptional(r => r.MiscellaneousTransaction)
                .WithMany(mt => mt.Receipts)
                .HasForeignKey(r => r.MiscellaneousTransactionAF)
                .WillCascadeOnDelete(false);

            HasOptional(r => r.PlotTransaction)
                .WithMany(pt => pt.Receipts)
                .HasForeignKey(r => r.PlotTransactionAF)
                .WillCascadeOnDelete(false);

            HasOptional(r => r.QuadrangleTransaction)
                .WithMany(qt => qt.Receipts)
                .HasForeignKey(r => r.QuadrangleTransactionAF)
                .WillCascadeOnDelete(false);

            HasOptional(r => r.SpaceTransaction)
                .WithMany(sp => sp.Receipts)
                .HasForeignKey(r => r.SpaceTransactionAF)
                .WillCascadeOnDelete(false);

            HasOptional(r => r.UrnTransaction)
               .WithMany(ut => ut.Receipts)
               .HasForeignKey(r => r.UrnTransactionAF)
               .WillCascadeOnDelete(false);

            HasRequired(r => r.PaymentMethod)
                 .WithMany(pm => pm.Receipts)
                 .HasForeignKey(r => r.PaymentMethodId)
                 .WillCascadeOnDelete(false);

        }
    }
}