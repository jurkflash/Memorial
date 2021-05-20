using System.Data.Entity.ModelConfiguration;
using Memorial.Core.Domain;

namespace Memorial.Persistence.EntityConfigurations
{
    public class CemeteryItemConfiguration : EntityTypeConfiguration<CemeteryItem>
    {
        public CemeteryItemConfiguration()
        {
            Property(pi => pi.Name)
                .IsRequired()
                .HasMaxLength(255);

            Property(pi => pi.Description)
                .HasMaxLength(255);

            Property(pi => pi.Code)
                .IsRequired()
                .HasMaxLength(10);

            HasRequired(pi => pi.Plot)
                .WithMany(pa => pa.CemeteryItems)
                .HasForeignKey(pi => pi.PlotId)
                .WillCascadeOnDelete(false);
        }
    }
}