using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace JobApplicantsManagement.Infrastructure.Presistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(BuildDbContextOptions);

            services.AddScoped<IApplicationDbContext, ApplicationDbContext>();

            return services;
        }
        private static void BuildDbContextOptions(IServiceProvider serviceProvider, DbContextOptionsBuilder options)
        {
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();

            var connString = configuration.GetConnectionString("DefaultConnection");

            options.LogTo(s => System.Diagnostics.Debug.WriteLine(s));

            options.UseSqlServer(connString);
        }

    }
}
