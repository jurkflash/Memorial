using System.Data.Entity.ModelConfiguration;
using Memorial.Core.Domain;

namespace Memorial.Persistence.EntityConfigurations
{
    public class QuadrangleTrackingConfiguration : EntityTypeConfiguration<QuadrangleTracking>
    {
        public QuadrangleTrackingConfiguration()
        {
            HasRequired(qt => qt.Quadrangle)
                .WithMany(q => q.QuadrangleTrackings1)
                .HasForeignKey(qt => qt.QuadrangleId)
                .WillCascadeOnDelete(false);

            HasRequired(qt => qt.ColumbariumTransaction)
                .WithMany(qts => qts.QuadrangleTrackings)
                .HasForeignKey(qt => qt.QuadrangleTransactionAF)
                .WillCascadeOnDelete(false);

            HasOptional(qt => qt.Applicant)
                .WithMany(a => a.QuadrangleTrackings)
                .HasForeignKey(qt => qt.ApplicantId)
                .WillCascadeOnDelete(false);

            HasOptional(qt => qt.Deceased1)
                .WithMany(d => d.QuadrangleTrackings1)
                .HasForeignKey(qt => qt.Deceased1Id)
                .WillCascadeOnDelete(false);

            HasOptional(qt => qt.Deceased2)
                .WithMany(d => d.QuadrangleTrackings2)
                .HasForeignKey(qt => qt.Deceased2Id)
                .WillCascadeOnDelete(false);

        }
    }
}