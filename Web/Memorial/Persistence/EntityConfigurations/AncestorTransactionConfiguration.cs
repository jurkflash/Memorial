using System.Data.Entity.ModelConfiguration;
using Memorial.Core.Domain;

namespace Memorial.Persistence.EntityConfigurations
{
    public class AncestorTransactionConfiguration : EntityTypeConfiguration<AncestorTransaction>
    {
        public AncestorTransactionConfiguration()
        {
            Property(at => at.AF)
                .IsRequired()
                .HasMaxLength(50);

            Property(at => at.Remark)
                .HasMaxLength(255);

            HasKey(at => at.AF);

            HasRequired(at => at.AncestorItem)
                .WithMany(ai => ai.AncestorTransactions)
                .HasForeignKey(at => at.AncestorItemId)
                .WillCascadeOnDelete(false);

            HasRequired(at => at.Ancestor)
                .WithMany(a => a.AncestorTransactions)
                .HasForeignKey(at => at.AncestorId)
                .WillCascadeOnDelete(false);

            HasRequired(at => at.Applicant)
                .WithMany(ai => ai.AncestorTransactions)
                .HasForeignKey(at => at.ApplicantId)
                .WillCascadeOnDelete(false);

            HasOptional(at => at.Deceased)
                .WithMany(ai => ai.AncestorTransactions)
                .HasForeignKey(at => at.DeceasedId)
                .WillCascadeOnDelete(false);

            HasRequired(at => at.RelationshipType)
                .WithMany(r => r.AncestorTransactions)
                .HasForeignKey(at => at.RelationshipTypeId)
                .WillCascadeOnDelete(false);
        }
    }
}