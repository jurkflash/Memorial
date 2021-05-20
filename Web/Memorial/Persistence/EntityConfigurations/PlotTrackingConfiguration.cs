using System.Data.Entity.ModelConfiguration;
using Memorial.Core.Domain;

namespace Memorial.Persistence.EntityConfigurations
{
    public class PlotTrackingConfiguration : EntityTypeConfiguration<PlotTracking>
    {
        public PlotTrackingConfiguration()
        {
            HasRequired(qt => qt.Plot)
                .WithMany(q => q.PlotTrackings)
                .HasForeignKey(qt => qt.PlotId)
                .WillCascadeOnDelete(false);

            HasRequired(qt => qt.CemeteryTransaction)
                .WithMany(qts => qts.PlotTrackings)
                .HasForeignKey(qt => qt.CemeteryTransactionAF)
                .WillCascadeOnDelete(false);

            HasOptional(qt => qt.Applicant)
                .WithMany(a => a.PlotTrackings)
                .HasForeignKey(qt => qt.ApplicantId)
                .WillCascadeOnDelete(false);

            HasOptional(qt => qt.Deceased1)
                .WithMany(d => d.PlotTrackings1)
                .HasForeignKey(qt => qt.Deceased1Id)
                .WillCascadeOnDelete(false);

            HasOptional(qt => qt.Deceased2)
                .WithMany(d => d.PlotTrackings2)
                .HasForeignKey(qt => qt.Deceased2Id)
                .WillCascadeOnDelete(false);

            HasOptional(qt => qt.Deceased3)
                .WithMany(d => d.PlotTrackings3)
                .HasForeignKey(qt => qt.Deceased3Id)
                .WillCascadeOnDelete(false);

        }
    }
}