using System.Data.Entity.ModelConfiguration;
using Memorial.Core.Domain;

namespace Memorial.Persistence.EntityConfigurations
{
    public class ApplicantDeceasedConfiguration : EntityTypeConfiguration<ApplicantDeceased>
    {
        public ApplicantDeceasedConfiguration()
        {
            Property(a => a.ApplicantId)
                .IsRequired();

            Property(a => a.DeceasedId)
                .IsRequired();

            Property(a => a.RelationshipTypeId)
                .IsRequired();

            HasRequired(r => r.RelationshipType)
                .WithMany(ad => ad.ApplicantDeceaseds)
                .HasForeignKey(r => r.RelationshipTypeId)
                .WillCascadeOnDelete(false);

            HasRequired(a => a.Applicant)
                .WithMany(ad => ad.ApplicantDeceaseds)
                .HasForeignKey(a => a.ApplicantId)
                .WillCascadeOnDelete(false);

            HasRequired(d => d.Deceased)
                .WithMany(ad => ad.ApplicantDeceaseds)
                .HasForeignKey(d => d.DeceasedId)
                .WillCascadeOnDelete(false);

            EntityTypeConfigurationExtension.ConfigureAuditColumns(this);

        }
    }
}