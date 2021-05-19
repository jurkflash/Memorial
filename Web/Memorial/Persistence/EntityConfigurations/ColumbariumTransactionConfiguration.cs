using System.Data.Entity.ModelConfiguration;
using Memorial.Core.Domain;

namespace Memorial.Persistence.EntityConfigurations
{
    public class ColumbariumTransactionConfiguration : EntityTypeConfiguration<ColumbariumTransaction>
    {
        public ColumbariumTransactionConfiguration()
        {
            Property(qt => qt.AF)
                .IsRequired()
                .HasMaxLength(50);

            Property(qt => qt.Remark)
                .HasMaxLength(255);

            HasKey(qt => qt.AF);

            HasRequired(qt => qt.QuadrangleItem)
                .WithMany(q => q.ColumbariumTransactions)
                .HasForeignKey(qt => qt.QuadrangleItemId)
                .WillCascadeOnDelete(false);

            HasRequired(qt => qt.Quadrangle)
                .WithMany(q => q.ColumbariumTransactions1)
                .HasForeignKey(qt => qt.QuadrangleId)
                .WillCascadeOnDelete(false);

            HasRequired(qt => qt.Applicant)
                .WithMany(a => a.ColumbariumTransactions1)
                .HasForeignKey(qt => qt.ApplicantId)
                .WillCascadeOnDelete(false);

            HasOptional(qt => qt.Deceased1)
                .WithMany(fc => fc.ColumbariumTransactions1)
                .HasForeignKey(qt => qt.Deceased1Id)
                .WillCascadeOnDelete(false);

            HasOptional(qt => qt.Deceased2)
                .WithMany(fc => fc.ColumbariumTransactions2)
                .HasForeignKey(qt => qt.Deceased2Id)
                .WillCascadeOnDelete(false);

            HasOptional(qt => qt.FuneralCompany)
                .WithMany(fc => fc.ColumbariumTransactions)
                .HasForeignKey(qt => qt.FuneralCompanyId)
                .WillCascadeOnDelete(false);

            HasOptional(qt => qt.ShiftedQuadrangle)
                .WithMany(q => q.ColumbariumTransactions2)
                .HasForeignKey(qt => qt.ShiftedQuadrangleId)
                .WillCascadeOnDelete(false);

            HasOptional(qt => qt.TransferredFromApplicant)
                .WithMany(a => a.ColumbariumTransactions3)
                .HasForeignKey(qt => qt.TransferredFromApplicantId)
                .WillCascadeOnDelete(false);

            HasOptional(qt => qt.ShiftedColumbariumTransaction)
                .WithMany()
                .HasForeignKey(qt => qt.ShiftedColumbariumTransactionAF)
                .WillCascadeOnDelete(false);

            HasOptional(qt => qt.TransferredApplicant)
                .WithMany(a => a.ColumbariumTransactions2)
                .HasForeignKey(qt => qt.TransferredApplicantId)
                .WillCascadeOnDelete(false);

            HasOptional(qt => qt.TransferredColumbariumTransaction)
                .WithMany()
                .HasForeignKey(qt => qt.TransferredColumbariumTransactionAF)
                .WillCascadeOnDelete(false);
        }
    }
}