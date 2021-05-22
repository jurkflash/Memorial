using System.Data.Entity.ModelConfiguration;
using Memorial.Core.Domain;

namespace Memorial.Persistence.EntityConfigurations
{
    public class AncestorConfiguration : EntityTypeConfiguration<Ancestor>
    {
        public AncestorConfiguration()
        {
            Property(a => a.Name)
                .IsRequired()
                .HasMaxLength(50);

            Property(a => a.Remark)
                .HasMaxLength(255);

            HasOptional(a => a.Applicant)
                .WithMany(a => a.Ancestors)
                .HasForeignKey(a => a.ApplicantId)
                .WillCascadeOnDelete(false);

            HasMany(a => a.Deceaseds)
                .WithOptional(d => d.Ancestor)
                .HasForeignKey(a => a.AncestorId)
                .WillCascadeOnDelete(false);

            HasRequired(a => a.AncestralTabletArea)
                .WithMany(aa => aa.Ancestors)
                .HasForeignKey(a => a.AncestralTabletAreaId)
                .WillCascadeOnDelete(false);
        }
    }
}