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

            HasRequired(at => at.AncestorItem)
                .WithMany(ai => ai.AncestralTabletTransactions)
                .HasForeignKey(at => at.AncestorItemId)
                .WillCascadeOnDelete(false);

            HasRequired(at => at.Ancestor)
                .WithMany(a => a.AncestralTabletTransactions)
                .HasForeignKey(at => at.AncestorId)
                .WillCascadeOnDelete(false);

            HasRequired(at => at.Applicant)
                .WithMany(ai => ai.AncestralTabletTransactions)
                .HasForeignKey(at => at.ApplicantId)
                .WillCascadeOnDelete(false);

            HasOptional(at => at.Deceased)
                .WithMany(ai => ai.AncestralTabletTransactions)
                .HasForeignKey(at => at.DeceasedId)
                .WillCascadeOnDelete(false);

            HasOptional(at => at.ShiftedAncestor)
               .WithMany(q => q.ShiftedAncestralTabletTransactions)
               .HasForeignKey(at => at.ShiftedAncestorId)
               .WillCascadeOnDelete(false);

            HasOptional(at => at.ShiftedAncestralTabletTransaction)
                .WithMany()
                .HasForeignKey(at => at.ShiftedAncestralTabletTransactionAF)
                .WillCascadeOnDelete(false);
        }
    }
}