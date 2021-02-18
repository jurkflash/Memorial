using System.Data.Entity.ModelConfiguration;
using Memorial.Core.Domain;

namespace Memorial.Persistence.EntityConfigurations
{
    public class QuadrangleTransactionConfiguration : EntityTypeConfiguration<QuadrangleTransaction>
    {
        public QuadrangleTransactionConfiguration()
        {
            Property(qt => qt.AF)
                .IsRequired()
                .HasMaxLength(50);

            Property(qt => qt.Remark)
                .HasMaxLength(255);

            HasKey(qt => qt.AF);

            HasRequired(qt => qt.QuadrangleItem)
                .WithMany(q => q.QuadrangleTransactions)
                .HasForeignKey(qt => qt.QuadrangleItemId)
                .WillCascadeOnDelete(false);

            HasRequired(qt => qt.Quadrangle)
                .WithMany(q => q.QuadrangleTransactions)
                .HasForeignKey(qt => qt.QuadrangleId)
                .WillCascadeOnDelete(false);

            HasRequired(qt => qt.Applicant)
                .WithMany(a => a.QuadrangleTransactions)
                .HasForeignKey(qt => qt.ApplicantId)
                .WillCascadeOnDelete(false);

            HasOptional(qt => qt.FuneralCompany)
                .WithMany(fc => fc.QuadrangleTransactions)
                .HasForeignKey(qt => qt.FuneralCompanyId)
                .WillCascadeOnDelete(false);
        }
    }
}