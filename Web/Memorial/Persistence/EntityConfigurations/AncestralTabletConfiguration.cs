using System.Data.Entity.ModelConfiguration;
using Memorial.Core.Domain;

namespace Memorial.Persistence.EntityConfigurations
{
    public class AncestralTabletConfiguration : EntityTypeConfiguration<AncestralTablet>
    {
        public AncestralTabletConfiguration()
        {
            Property(a => a.Name)
                .IsRequired()
                .HasMaxLength(50);

            Property(a => a.Remark)
                .HasMaxLength(255);

            HasOptional(a => a.Applicant)
                .WithMany(a => a.AncestralTablets)
                .HasForeignKey(a => a.ApplicantId)
                .WillCascadeOnDelete(false);

            HasMany(a => a.Deceaseds)
                .WithOptional(d => d.AncestralTablet)
                .HasForeignKey(a => a.AncestralTabletId)
                .WillCascadeOnDelete(false);

            HasRequired(a => a.AncestralTabletArea)
                .WithMany(aa => aa.AncestralTablets)
                .HasForeignKey(a => a.AncestralTabletAreaId)
                .WillCascadeOnDelete(false);
        }
    }
}