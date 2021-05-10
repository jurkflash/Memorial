using System.Data.Entity.ModelConfiguration;
using Memorial.Core.Domain;

namespace Memorial.Persistence.EntityConfigurations
{
    public class PlotTransactionConfiguration : EntityTypeConfiguration<PlotTransaction>
    {
        public PlotTransactionConfiguration()
        {
            Property(pt => pt.AF)
                .IsRequired()
                .HasMaxLength(50);

            Property(pt => pt.Remark)
                .HasMaxLength(255);

            HasKey(pt => pt.AF);

            HasRequired(pt => pt.PlotItem)
                .WithMany(pi => pi.PlotTransactions)
                .HasForeignKey(pt => pt.PlotItemId)
                .WillCascadeOnDelete(false);

            HasRequired(pt => pt.Plot)
                .WithMany(p => p.PlotTransactions)
                .HasForeignKey(pt => pt.PlotId)
                .WillCascadeOnDelete(false);

            HasOptional(pt => pt.FengShuiMaster)
                .WithMany(d => d.PlotTransactions)
                .HasForeignKey(pt => pt.FengShuiMasterId)
                .WillCascadeOnDelete(false);

            HasRequired(pt => pt.Applicant)
                .WithMany(a => a.PlotTransactions1)
                .HasForeignKey(pt => pt.ApplicantId)
                .WillCascadeOnDelete(false);

            HasOptional(pt => pt.Deceased1)
                .WithMany(d => d.PlotTransactions1)
                .HasForeignKey(pt => pt.Deceased1Id)
                .WillCascadeOnDelete(false);

            HasOptional(pt => pt.Deceased2)
                .WithMany(d => d.PlotTransactions2)
                .HasForeignKey(pt => pt.Deceased2Id)
                .WillCascadeOnDelete(false);

            HasOptional(pt => pt.Deceased3)
                .WithMany(d => d.PlotTransactions3)
                .HasForeignKey(pt => pt.Deceased3Id)
                .WillCascadeOnDelete(false);

            HasOptional(pt => pt.ClearedApplicant)
                .WithMany(a => a.PlotTransactions2)
                .HasForeignKey(pt => pt.ClearedApplicantId)
                .WillCascadeOnDelete(false);

        }
    }
}