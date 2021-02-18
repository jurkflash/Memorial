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

            HasRequired(pt => pt.Applicant)
                .WithMany(a => a.PlotTransactions)
                .HasForeignKey(pt => pt.ApplicantId)
                .WillCascadeOnDelete(false);

            HasOptional(pt => pt.Deceased)
                .WithMany(d => d.PlotTransactions)
                .HasForeignKey(pt => pt.DeceasedId)
                .WillCascadeOnDelete(false);
        }
    }
}