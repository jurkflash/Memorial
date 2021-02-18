using System.Data.Entity.ModelConfiguration;
using Memorial.Core.Domain;

namespace Memorial.Persistence.EntityConfigurations
{
    public class PlotItemConfiguration : EntityTypeConfiguration<PlotItem>
    {
        public PlotItemConfiguration()
        {
            Property(pi => pi.Name)
                .IsRequired()
                .HasMaxLength(255);

            Property(pi => pi.Description)
                .HasMaxLength(255);

            Property(pi => pi.Code)
                .IsRequired()
                .HasMaxLength(10);

            HasRequired(pi => pi.PlotArea)
                .WithMany(pa => pa.PlotItems)
                .HasForeignKey(pi => pi.PlotAreaId)
                .WillCascadeOnDelete(false);
        }
    }
}