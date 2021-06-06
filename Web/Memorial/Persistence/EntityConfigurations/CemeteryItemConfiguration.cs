using System.Data.Entity.ModelConfiguration;
using Memorial.Core.Domain;

namespace Memorial.Persistence.EntityConfigurations
{
    public class CemeteryItemConfiguration : EntityTypeConfiguration<CemeteryItem>
    {
        public CemeteryItemConfiguration()
        {
            Property(pi => pi.Code)
                .HasMaxLength(10);

            HasRequired(pi => pi.Plot)
                .WithMany(pa => pa.CemeteryItems)
                .HasForeignKey(pi => pi.PlotId)
                .WillCascadeOnDelete(false);

            HasRequired(pi => pi.SubProductService)
                .WithMany(pa => pa.CemeteryItems)
                .HasForeignKey(pi => pi.SubProductServiceId)
                .WillCascadeOnDelete(false);
        }
    }
}