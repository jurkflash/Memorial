using System.Data.Entity.ModelConfiguration;
using Memorial.Core.Domain;

namespace Memorial.Persistence.EntityConfigurations
{
    public class SpaceTransactionConfiguration : EntityTypeConfiguration<SpaceTransaction>
    {
        public SpaceTransactionConfiguration()
        {
            Property(st => st.AF)
                .IsRequired()
                .HasMaxLength(50);

            Property(st => st.Remark)
                .HasMaxLength(255);

            HasKey(st => st.AF);

            HasRequired(st => st.SpaceItem)
                .WithMany(si => si.SpaceTransactions)
                .HasForeignKey(st => st.SpaceItemId)
                .WillCascadeOnDelete(false);

            HasRequired(st => st.Applicant)
                .WithMany(a => a.SpaceTransactions)
                .HasForeignKey(st => st.ApplicantId)
                .WillCascadeOnDelete(false);

            HasOptional(st => st.FuneralCompany)
                .WithMany(fc => fc.SpaceTransactions)
                .HasForeignKey(st => st.FuneralCompanyId)
                .WillCascadeOnDelete(false);

            HasOptional(st => st.Deceased)
                .WithMany(d => d.SpaceTransactions)
                .HasForeignKey(st => st.DeceasedId)
                .WillCascadeOnDelete(false);
        }
    }
}