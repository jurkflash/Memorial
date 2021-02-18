using System.Data.Entity.ModelConfiguration;
using Memorial.Core.Domain;

namespace Memorial.Persistence.EntityConfigurations
{
    public class CremationTransactionConfiguration : EntityTypeConfiguration<CremationTransaction>
    {
        public CremationTransactionConfiguration()
        {
            Property(ct => ct.AF)
                .IsRequired()
                .HasMaxLength(50);

            Property(ct => ct.Remark)
                .HasMaxLength(255);

            HasKey(ct => ct.AF);

            HasRequired(ct => ct.CremationItem)
                .WithMany(ci => ci.CremationTransactions)
                .HasForeignKey(ct => ct.CremationItemId)
                .WillCascadeOnDelete(false);

            HasRequired(ct => ct.Applicant)
                .WithMany(a => a.CremationTransactions)
                .HasForeignKey(ct => ct.ApplicantId)
                .WillCascadeOnDelete(false);

            HasRequired(ct => ct.Deceased)
                .WithMany(d => d.CremationTransactions)
                .HasForeignKey(ct => ct.DeceasedId)
                .WillCascadeOnDelete(false);

            HasOptional(ct => ct.FuneralCompany)
                .WithMany(fc => fc.CremationTransactions)
                .HasForeignKey(ct => ct.FuneralCompanyId)
                .WillCascadeOnDelete(false);
        }
    }
}