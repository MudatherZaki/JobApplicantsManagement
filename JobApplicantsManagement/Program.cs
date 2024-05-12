using FluentValidation;
using JobApplicantsManagement.Features.Commands;
using JobApplicantsManagement.Features.Validators;
using JobApplicantsManagement.Infrastructure.Middleware;
using JobApplicantsManagement.Infrastructure.Presistence;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using static System.Net.Mime.MediaTypeNames;

namespace JobApplicantsManagement
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddPersistence();
            builder.Services.AddMediatR(c => c.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            builder.Services.AddValidatorsFromAssemblyContaining<CreateJobApplicantCommandValidator>();




            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            if (app.Environment.IsDevelopment())
            {
                using (var scope = app.Services.CreateScope())
                {
                    var context = scope.ServiceProvider
                        .GetRequiredService<IApplicationDbContext>();
                    try
                    {
                        context.Database.Migrate();

                        Console.WriteLine("Migrations applied successfully.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Migrations failed. Error:{ex.Message}");
                    }
                }
            }

            app.UseMiddleware<CustomResponseMiddleware>();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
