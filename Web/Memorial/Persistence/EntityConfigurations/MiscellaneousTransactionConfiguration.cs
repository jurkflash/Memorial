using System.Data.Entity.ModelConfiguration;
using Memorial.Core.Domain;

namespace Memorial.Persistence.EntityConfigurations
{
    public class MiscellaneousTransactionConfiguration : EntityTypeConfiguration<MiscellaneousTransaction>
    {
        public MiscellaneousTransactionConfiguration()
        {
            Property(mt => mt.AF)
                .IsRequired()
                .HasMaxLength(50);

            Property(mt => mt.Remark)
                .HasMaxLength(255);

            HasKey(mt => mt.AF);

            HasRequired(mt => mt.MiscellaneousItem)
                .WithMany(mi => mi.MiscellaneousTransactions)
                .HasForeignKey(mt => mt.MiscellaneousItemId)
                .WillCascadeOnDelete(false);

            HasRequired(mt => mt.Applicant)
                .WithMany(a => a.MiscellaneousTransactions)
                .HasForeignKey(mt => mt.ApplicantId)
                .WillCascadeOnDelete(false);
        }
    }
}