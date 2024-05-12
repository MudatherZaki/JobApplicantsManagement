using JobApplicantsManagement.Infrastructure.Presistence.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace JobApplicantsManagement.Infrastructure.Presistence
{
    public interface IApplicationDbContext
    {
        DbSet<JobApplicant> JobApplicants { set; get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
        int SaveChanges();
        DatabaseFacade Database { get; }
    }
}
