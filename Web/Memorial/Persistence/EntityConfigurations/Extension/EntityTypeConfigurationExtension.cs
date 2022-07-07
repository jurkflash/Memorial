using Memorial.Core.Domain;
using System.Data.Entity.ModelConfiguration;

namespace Memorial.Persistence.EntityConfigurations
{
    public static class EntityTypeConfigurationExtension
    {
        public static EntityTypeConfiguration<T> ConfigureAuditColumns<T>(this EntityTypeConfiguration<T> builder) where T : Base
        {
            builder.Property(a => a.ActiveStatus)
                .HasColumnOrder(1000);

            builder.Property(a => a.CreatedById)
                .HasMaxLength(40)
                .HasColumnOrder(1001);

            builder.Property(a => a.CreatedDate)
                .HasColumnOrder(1002);

            builder.Property(a => a.ModifiedById)
                .HasMaxLength(40)
                .HasColumnOrder(1003);

            builder.Property(a => a.ModifiedDate)
                .HasColumnOrder(1004);

            builder.Property(a => a.DeletedById)
                .HasMaxLength(40)
                .HasColumnOrder(1005);

            builder.Property(a => a.DeletedDate)
                .HasColumnOrder(1006);

            return builder;
        }
    }
}
