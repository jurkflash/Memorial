using System.Data.Entity.ModelConfiguration;
using Memorial.Core.Domain;

namespace Memorial.Persistence.EntityConfigurations
{
    public class AncestorTrackingConfiguration : EntityTypeConfiguration<AncestorTracking>
    {
        public AncestorTrackingConfiguration()
        {
            HasRequired(at => at.Ancestor)
                .WithMany(a => a.AncestorTrackings)
                .HasForeignKey(at => at.AncestorId)
                .WillCascadeOnDelete(false);

            HasOptional(qt => qt.Applicant)
                .WithMany(a => a.AncestorTrackings)
                .HasForeignKey(qt => qt.ApplicantId)
                .WillCascadeOnDelete(false);

            HasOptional(qt => qt.Deceased)
                .WithMany(d => d.AncestorTrackings)
                .HasForeignKey(qt => qt.DeceasedId)
                .WillCascadeOnDelete(false);

            HasRequired(qt => qt.AncestorTransaction)
                .WithMany(qts => qts.AncestorTrackings)
                .HasForeignKey(qt => qt.AncestorTransactionAF)
                .WillCascadeOnDelete(false);

        }
    }
}