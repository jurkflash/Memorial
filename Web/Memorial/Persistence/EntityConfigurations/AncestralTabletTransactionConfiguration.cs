using System.Data.Entity.ModelConfiguration;
using Memorial.Core.Domain;

namespace Memorial.Persistence.EntityConfigurations
{
    public class AncestralTabletTransactionConfiguration : EntityTypeConfiguration<AncestralTabletTransaction>
    {
        public AncestralTabletTransactionConfiguration()
        {
            Property(at => at.AF)
                .IsRequired()
                .HasMaxLength(50);

            Property(at => at.Remark)
                .HasMaxLength(255);

            HasKey(at => at.AF);

            HasRequired(at => at.AncestralTabletItem)
                .WithMany(ai => ai.AncestralTabletTransactions)
                .HasForeignKey(at => at.AncestralTabletItemId)
                .WillCascadeOnDelete(false);

            HasRequired(at => at.AncestralTablet)
                .WithMany(a => a.AncestralTabletTransactions)
                .HasForeignKey(at => at.AncestralTabletId)
                .WillCascadeOnDelete(false);

            HasRequired(at => at.Applicant)
                .WithMany(ai => ai.AncestralTabletTransactions)
                .HasForeignKey(at => at.ApplicantId)
                .WillCascadeOnDelete(false);

            HasOptional(at => at.Deceased)
                .WithMany(ai => ai.AncestralTabletTransactions)
                .HasForeignKey(at => at.DeceasedId)
                .WillCascadeOnDelete(false);

            HasOptional(at => at.ShiftedAncestralTablet)
               .WithMany(q => q.ShiftedAncestralTabletTransactions)
               .HasForeignKey(at => at.ShiftedAncestralTabletId)
               .WillCascadeOnDelete(false);

            HasOptional(at => at.ShiftedAncestralTabletTransaction)
                .WithMany()
                .HasForeignKey(at => at.ShiftedAncestralTabletTransactionAF)
                .WillCascadeOnDelete(false);
        }
    }
}