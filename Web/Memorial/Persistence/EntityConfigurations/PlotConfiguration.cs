using System.Data.Entity.ModelConfiguration;
using Memorial.Core.Domain;

namespace Memorial.Persistence.EntityConfigurations
{
    public class PlotConfiguration : EntityTypeConfiguration<Plot>
    {
        public PlotConfiguration()
        {
            Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(255);

            Property(p => p.Size)
            .IsRequired()
            .HasMaxLength(255);

            HasRequired(p => p.PlotType)
                .WithMany(pt => pt.Plots)
                .HasForeignKey(p => p.PlotTypeId)
                .WillCascadeOnDelete(false);

            HasRequired(p => p.CemeteryArea)
                .WithMany(pa => pa.Plots)
                .HasForeignKey(p => p.CemeteryAreaId)
                .WillCascadeOnDelete(false);

            HasOptional(p => p.Applicant)
                .WithMany(a => a.Plots)
                .HasForeignKey(p => p.ApplicantId)
                .WillCascadeOnDelete(false);

            HasMany(d => d.Deceaseds)
                .WithOptional(p => p.Plot)
                .HasForeignKey(d => d.PlotId)
                .WillCascadeOnDelete(false);
        }
    }
}