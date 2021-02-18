using System.Data.Entity.ModelConfiguration;
using Memorial.Core.Domain;

namespace Memorial.Persistence.EntityConfigurations
{
    public class PlotAreaConfiguration : EntityTypeConfiguration<PlotArea>
    {
        public PlotAreaConfiguration()
        {
            Property(pa => pa.Name)
                .IsRequired()
                .HasMaxLength(255);


            HasRequired(pa => pa.Site)
                .WithMany(s => s.PlotAreas)
                .HasForeignKey(pa => pa.SiteId)
                .WillCascadeOnDelete(false);
        }
    }
}