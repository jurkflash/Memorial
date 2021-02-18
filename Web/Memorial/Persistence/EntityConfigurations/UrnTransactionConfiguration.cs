using System.Data.Entity.ModelConfiguration;
using Memorial.Core.Domain;

namespace Memorial.Persistence.EntityConfigurations
{
    public class UrnTransactionConfiguration : EntityTypeConfiguration<UrnTransaction>
    {
        public UrnTransactionConfiguration()
        {
            Property(ut => ut.AF)
                .IsRequired()
                .HasMaxLength(50);

            Property(qt => qt.Remark)
                .HasMaxLength(255);

            HasKey(qt => qt.AF);

            HasRequired(ut => ut.UrnItem)
                .WithMany(ui => ui.UrnTransactions)
                .HasForeignKey(ut => ut.UrnItemId)
                .WillCascadeOnDelete(false);

            HasRequired(ut => ut.Applicant)
                .WithMany(a => a.UrnTransactions)
                .HasForeignKey(ut => ut.ApplicantId)
                .WillCascadeOnDelete(false);
        }
    }
}