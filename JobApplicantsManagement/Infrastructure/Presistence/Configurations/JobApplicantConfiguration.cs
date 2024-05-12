
using JobApplicantsManagement.Infrastructure.Presistence.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobApplicantsManagement.Infrastructure.Presistence.Configurations
{
    public class JobApplicantConfiguration : IEntityTypeConfiguration<JobApplicant>
    {
        private const int MaxNameLength = 50;
        public void Configure(EntityTypeBuilder<JobApplicant> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.FirstName)
                .IsRequired()
                .HasMaxLength(MaxNameLength);

            builder.Property(a => a.LastName)
                .IsRequired()
                .HasMaxLength(MaxNameLength);

            builder.Property(a => a.Email)
                .IsRequired();

            builder.Property(a => a.Comment)
                .IsRequired();

            builder.ToTable("JobApplicants");
        }
    }
}
