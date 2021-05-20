using System.Data.Entity.ModelConfiguration;
using Memorial.Core.Domain;

namespace Memorial.Persistence.EntityConfigurations
{
    public class CemeteryTransactionConfiguration : EntityTypeConfiguration<CemeteryTransaction>
    {
        public CemeteryTransactionConfiguration()
        {
            Property(pt => pt.AF)
                .IsRequired()
                .HasMaxLength(50);

            Property(pt => pt.Remark)
                .HasMaxLength(255);

            HasKey(pt => pt.AF);

            HasRequired(pt => pt.PlotItem)
                .WithMany(pi => pi.CemeteryTransactions)
                .HasForeignKey(pt => pt.PlotItemId)
                .WillCascadeOnDelete(false);

            HasRequired(pt => pt.Plot)
                .WithMany(p => p.CemeteryTransactions)
                .HasForeignKey(pt => pt.PlotId)
                .WillCascadeOnDelete(false);

            HasOptional(pt => pt.FengShuiMaster)
                .WithMany(d => d.CemeteryTransactions)
                .HasForeignKey(pt => pt.FengShuiMasterId)
                .WillCascadeOnDelete(false);

            HasRequired(pt => pt.Applicant)
                .WithMany(a => a.CemeteryTransactions1)
                .HasForeignKey(pt => pt.ApplicantId)
                .WillCascadeOnDelete(false);

            HasOptional(pt => pt.Deceased1)
                .WithMany(d => d.CemeteryTransactions1)
                .HasForeignKey(pt => pt.Deceased1Id)
                .WillCascadeOnDelete(false);

            HasOptional(pt => pt.Deceased2)
                .WithMany(d => d.CemeteryTransactions2)
                .HasForeignKey(pt => pt.Deceased2Id)
                .WillCascadeOnDelete(false);

            HasOptional(pt => pt.Deceased3)
                .WithMany(d => d.CemeteryTransactions3)
                .HasForeignKey(pt => pt.Deceased3Id)
                .WillCascadeOnDelete(false);

            HasOptional(pt => pt.ClearedApplicant)
                .WithMany(a => a.CemeteryTransactions2)
                .HasForeignKey(pt => pt.ClearedApplicantId)
                .WillCascadeOnDelete(false);

            HasOptional(pt => pt.TransferredApplicant)
                .WithMany(a => a.CemeteryTransactions3)
                .HasForeignKey(pt => pt.TransferredApplicantId)
                .WillCascadeOnDelete(false);

            HasOptional(pt => pt.TransferredCemeteryTransaction)
                .WithMany()
                .HasForeignKey(pt => pt.TransferredCemeteryTransactionAF)
                .WillCascadeOnDelete(false);

        }
    }
}