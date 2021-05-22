using System.Data.Entity.ModelConfiguration;
using Memorial.Core.Domain;

namespace Memorial.Persistence.EntityConfigurations
{
    public class AncestralTabletTrackingConfiguration : EntityTypeConfiguration<AncestralTabletTracking>
    {
        public AncestralTabletTrackingConfiguration()
        {
            HasRequired(at => at.Ancestor)
                .WithMany(a => a.AncestralTabletTrackings)
                .HasForeignKey(at => at.AncestorId)
                .WillCascadeOnDelete(false);

            HasOptional(qt => qt.Applicant)
                .WithMany(a => a.AncestralTabletTrackings)
                .HasForeignKey(qt => qt.ApplicantId)
                .WillCascadeOnDelete(false);

            HasOptional(qt => qt.Deceased)
                .WithMany(d => d.AncestralTabletTrackings)
                .HasForeignKey(qt => qt.DeceasedId)
                .WillCascadeOnDelete(false);

            HasRequired(qt => qt.AncestralTabletTransaction)
                .WithMany(qts => qts.AncestralTabletTrackings)
                .HasForeignKey(qt => qt.AncestralTabletTransactionAF)
                .WillCascadeOnDelete(false);

        }
    }
}