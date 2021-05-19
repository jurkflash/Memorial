using System.Data.Entity.ModelConfiguration;
using Memorial.Core.Domain;

namespace Memorial.Persistence.EntityConfigurations
{
    public class NicheConfiguration : EntityTypeConfiguration<Niche>
    {
        public NicheConfiguration()
        {
            Property(q => q.Name)
            .IsRequired()
            .HasMaxLength(100);

            Property(q => q.Description)
            .HasMaxLength(100);

            Property(q => q.Remark)
            .HasMaxLength(255);

            HasMany(d => d.Deceaseds)
                .WithOptional(q => q.Niche)
                .HasForeignKey(d => d.NicheId)
                .WillCascadeOnDelete(false);

            HasRequired(q => q.NicheType)
                .WithMany(qt => qt.Niches)
                .HasForeignKey(q => q.NicheTypeId)
                .WillCascadeOnDelete(false);

            HasRequired(q => q.ColumbariumArea)
                .WithMany(qa => qa.Niches)
                .HasForeignKey(q => q.ColumbariumAreaId)
                .WillCascadeOnDelete(false);

            HasOptional(q => q.Applicant)
                .WithMany(a => a.Niches)
                .HasForeignKey(q => q.ApplicantId)
                .WillCascadeOnDelete(false);
        }
    }
}