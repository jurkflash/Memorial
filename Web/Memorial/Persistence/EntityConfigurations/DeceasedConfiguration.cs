using System.Data.Entity.ModelConfiguration;
using Memorial.Core.Domain;

namespace Memorial.Persistence.EntityConfigurations
{
    public class DeceasedConfiguration : EntityTypeConfiguration<Deceased>
    {
        public DeceasedConfiguration()
        {
            Property(d => d.Name)
                .IsRequired()
                .HasMaxLength(255);

            Property(d => d.IC)
                .IsRequired()
                .HasMaxLength(20);

            Property(d => d.DeathDate)
                .IsRequired();

            Property(d => d.Remark)
                .HasMaxLength(255);

            HasRequired(a => a.Applicant)
                .WithMany(d => d.Deceaseds)
                .HasForeignKey(a => a.ApplicantId)
                .WillCascadeOnDelete(false);

            HasRequired(g => g.GenderType)
                .WithMany(d => d.Deceaseds)
                .HasForeignKey(g => g.GenderTypeId)
                .WillCascadeOnDelete(false);

            HasRequired(n => n.NationalityType)
                .WithMany(d => d.Deceaseds)
                .HasForeignKey(n => n.NationalityTypeId)
                .WillCascadeOnDelete(false);

            HasRequired(m => m.MaritalType)
                .WithMany(d => d.Deceaseds)
                .HasForeignKey(m => m.MaritalTypeId)
                .WillCascadeOnDelete(false);

            HasRequired(r => r.ReligionType)
                .WithMany(d => d.Deceaseds)
                .HasForeignKey(r => r.ReligionTypeId)
                .WillCascadeOnDelete(false);

            HasRequired(r => r.RelationshipType)
                .WithMany(d => d.Deceaseds)
                .HasForeignKey(r => r.RelationshipTypeId)
                .WillCascadeOnDelete(false);

        }
    }
}