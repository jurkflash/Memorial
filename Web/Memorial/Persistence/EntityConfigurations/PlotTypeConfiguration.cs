using System.Data.Entity.ModelConfiguration;
using Memorial.Core.Domain;

namespace Memorial.Persistence.EntityConfigurations
{
    public class PlotTypeConfiguration : EntityTypeConfiguration<PlotType>
    {
        public PlotTypeConfiguration()
        {
            Property(pt => pt.Name)
            .IsRequired()
            .HasMaxLength(255);

            Property(pt => pt.Code)
            .IsRequired()
            .HasMaxLength(10);
        }
    }
}