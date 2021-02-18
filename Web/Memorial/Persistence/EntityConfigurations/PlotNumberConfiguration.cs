using System.Data.Entity.ModelConfiguration;
using Memorial.Core.Domain;

namespace Memorial.Persistence.EntityConfigurations
{
    public class PlotNumberConfiguration : EntityTypeConfiguration<PlotNumber>
    {
        public PlotNumberConfiguration()
        {
            HasRequired(pn => pn.PlotItem)
                .WithMany(pi => pi.PlotNumbers)
                .HasForeignKey(pn => pn.PlotItemId)
                .WillCascadeOnDelete(false);
        }
    }
}