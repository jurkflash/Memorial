using System.Data.Entity.ModelConfiguration;
using Memorial.Core.Domain;

namespace Memorial.Persistence.EntityConfigurations
{
    public class PlotNumberConfiguration : EntityTypeConfiguration<PlotNumber>
    {
        public PlotNumberConfiguration()
        {
            Property(pn => pn.ItemCode)
                .IsRequired()
                .HasMaxLength(10);
        }
    }
}