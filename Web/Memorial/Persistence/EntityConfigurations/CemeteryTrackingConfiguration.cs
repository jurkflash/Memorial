using System.Data.Entity.ModelConfiguration;
using Memorial.Core.Domain;

namespace Memorial.Persistence.EntityConfigurations
{
    public class CemeteryTrackingConfiguration : EntityTypeConfiguration<CemeteryTracking>
    {
        public CemeteryTrackingConfiguration()
        {
            HasRequired(qt => qt.Plot)
                .WithMany(q => q.CemeteryTrackings)
                .HasForeignKey(qt => qt.PlotId)
                .WillCascadeOnDelete(false);

            HasRequired(qt => qt.CemeteryTransaction)
                .WithMany(qts => qts.CemeteryTrackings)
                .HasForeignKey(qt => qt.CemeteryTransactionAF)
                .WillCascadeOnDelete(false);

            HasOptional(qt => qt.Applicant)
                .WithMany(a => a.CemeteryTrackings)
                .HasForeignKey(qt => qt.ApplicantId)
                .WillCascadeOnDelete(false);

            HasOptional(qt => qt.Deceased1)
                .WithMany(d => d.CemeteryTrackings1)
                .HasForeignKey(qt => qt.Deceased1Id)
                .WillCascadeOnDelete(false);

            HasOptional(qt => qt.Deceased2)
                .WithMany(d => d.CemeteryTrackings2)
                .HasForeignKey(qt => qt.Deceased2Id)
                .WillCascadeOnDelete(false);

            HasOptional(qt => qt.Deceased3)
                .WithMany(d => d.CemeteryTrackings3)
                .HasForeignKey(qt => qt.Deceased3Id)
                .WillCascadeOnDelete(false);

        }
    }
}