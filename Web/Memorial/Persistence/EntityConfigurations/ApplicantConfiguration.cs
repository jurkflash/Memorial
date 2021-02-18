using System.Data.Entity.ModelConfiguration;
using Memorial.Core.Domain;

namespace Memorial.Persistence.EntityConfigurations
{
    public class ApplicantConfiguration : EntityTypeConfiguration<Applicant>
    {
        public ApplicantConfiguration()
        {
            Property(a => a.Name)
                .IsRequired()
                .HasMaxLength(255);

            Property(a => a.IC)
                .IsRequired()
                .HasMaxLength(20);

            Property(a => a.Remark)
                .HasMaxLength(255);
        }
    }
}