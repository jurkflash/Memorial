using System.Data.Entity.ModelConfiguration;
using Memorial.Core.Domain;

namespace Memorial.Persistence.EntityConfigurations
{
    public class ColumbariumTrackingConfiguration : EntityTypeConfiguration<ColumbariumTracking>
    {
        public ColumbariumTrackingConfiguration()
        {
            HasRequired(qt => qt.Quadrangle)
                .WithMany(q => q.ColumbariumTrackings1)
                .HasForeignKey(qt => qt.QuadrangleId)
                .WillCascadeOnDelete(false);

            HasRequired(qt => qt.ColumbariumTransaction)
                .WithMany(qts => qts.ColumbariumTrackings)
                .HasForeignKey(qt => qt.ColumbariumTransactionAF)
                .WillCascadeOnDelete(false);

            HasOptional(qt => qt.Applicant)
                .WithMany(a => a.ColumbariumTrackings)
                .HasForeignKey(qt => qt.ApplicantId)
                .WillCascadeOnDelete(false);

            HasOptional(qt => qt.Deceased1)
                .WithMany(d => d.ColumbariumTrackings1)
                .HasForeignKey(qt => qt.Deceased1Id)
                .WillCascadeOnDelete(false);

            HasOptional(qt => qt.Deceased2)
                .WithMany(d => d.ColumbariumTrackings2)
                .HasForeignKey(qt => qt.Deceased2Id)
                .WillCascadeOnDelete(false);

        }
    }
}