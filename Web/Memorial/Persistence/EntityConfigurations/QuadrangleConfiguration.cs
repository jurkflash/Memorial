using System.Data.Entity.ModelConfiguration;
using Memorial.Core.Domain;

namespace Memorial.Persistence.EntityConfigurations
{
    public class QuadrangleConfiguration : EntityTypeConfiguration<Quadrangle>
    {
        public QuadrangleConfiguration()
        {
            Property(q => q.Name)
            .IsRequired()
            .HasMaxLength(100);

            Property(q => q.Description)
            .HasMaxLength(100);

            Property(q => q.Remark)
            .HasMaxLength(255);

            HasMany(d => d.Deceaseds)
                .WithOptional(q => q.Quadrangle)
                .HasForeignKey(d => d.QuadrangleId)
                .WillCascadeOnDelete(false);

            HasRequired(q => q.QuadrangleType)
                .WithMany(qt => qt.Quadrangles)
                .HasForeignKey(q => q.QuadrangleTypeId)
                .WillCascadeOnDelete(false);

            HasRequired(q => q.QuadrangleArea)
                .WithMany(qa => qa.Quadrangles)
                .HasForeignKey(q => q.QuadrangleAreaId)
                .WillCascadeOnDelete(false);

            HasOptional(q => q.Applicant)
                .WithMany(a => a.Quadrangles)
                .HasForeignKey(q => q.ApplicantId)
                .WillCascadeOnDelete(false);
        }
    }
}