using System.Data.Entity.ModelConfiguration;
using Memorial.Core.Domain;

namespace Memorial.Persistence.EntityConfigurations
{
    public class InvoiceConfiguration : EntityTypeConfiguration<Invoice>
    {
        public InvoiceConfiguration()
        {
            Property(i => i.IV)
                .IsRequired()
                .HasMaxLength(50);

            Property(i => i.Remark)
                .HasMaxLength(255);

            HasKey(i => i.IV);

            HasOptional(i => i.AncestralTabletTransaction)
                .WithMany(at => at.Invoices)
                .HasForeignKey(i => i.AncestralTabletTransactionAF)
                .WillCascadeOnDelete(false);

            HasOptional(i => i.CremationTransaction)
                .WithMany(ct => ct.Invoices)
                .HasForeignKey(i => i.CremationTransactionAF)
                .WillCascadeOnDelete(false);

            HasOptional(i => i.MiscellaneousTransaction)
                .WithMany(mt => mt.Invoices)
                .HasForeignKey(i => i.MiscellaneousTransactionAF)
                .WillCascadeOnDelete(false);

            HasOptional(i => i.CemeteryTransaction)
                .WithMany(pt => pt.Invoices)
                .HasForeignKey(i => i.CemeteryTransactionAF)
                .WillCascadeOnDelete(false);

            HasOptional(i => i.ColumbariumTransaction)
                .WithMany(qt => qt.Invoices)
                .HasForeignKey(i => i.ColumbariumTransactionAF)
                .WillCascadeOnDelete(false);

            HasOptional(i => i.SpaceTransaction)
                .WithMany(sp => sp.Invoices)
                .HasForeignKey(i => i.SpaceTransactionAF)
                .WillCascadeOnDelete(false);

            HasOptional(i => i.UrnTransaction)
               .WithMany(ut => ut.Invoices)
               .HasForeignKey(i => i.UrnTransactionAF)
               .WillCascadeOnDelete(false);

            EntityTypeConfigurationExtension.ConfigureAuditColumns(this);
        }
    }
}