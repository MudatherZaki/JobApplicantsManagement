using JobApplicantsManagement.Features.Commands;
using JobApplicantsManagement.Features.Validators;
using JobApplicantsManagement.Infrastructure.Exceptions;
using JobApplicantsManagement.Infrastructure.Presistence;
using JobApplicantsManagement.Infrastructure.Presistence.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace JobApplicantsManagement.Features.Handlers
{
    public class UpdateJobApplicantCommandHandler : IRequestHandler<UpdateJobApplicantCommand, Guid>
    {
        private IApplicationDbContext _context;
        public UpdateJobApplicantCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Guid> Handle(UpdateJobApplicantCommand command, CancellationToken cancellationToken)
        {
            ValidateCommand(command);

            var applicantToUpdates = await _context.JobApplicants
                .FirstOrDefaultAsync(a => a.Id == command.Id);

            var applicantToUpdate = applicantToUpdates;

            if (applicantToUpdate is null)
            {
                throw new NotFoundException("Applicant is not found.");
            }

            applicantToUpdate.FirstName = command.FirstName ?? applicantToUpdate.FirstName;
            applicantToUpdate.AvailableFrom = command.AvailableFrom ?? applicantToUpdate.AvailableFrom;
            applicantToUpdate.AvailableTo = command.AvailableTo ?? applicantToUpdate.AvailableTo;
            applicantToUpdate.Comment = command.Comment ?? applicantToUpdate.Comment;
            applicantToUpdate.Email = command.Email ?? applicantToUpdate.Email;
            applicantToUpdate.GitHub = command.GitHub ?? applicantToUpdate.GitHub;
            applicantToUpdate.LastName = command.LastName ?? applicantToUpdate.LastName;
            applicantToUpdate.LinkedIn = command.LinkedIn ?? applicantToUpdate.LinkedIn;
            applicantToUpdate.PhoneNumber = command.PhoneNumber ?? applicantToUpdate.PhoneNumber;
            _context.JobApplicants.Update(applicantToUpdate);
            await _context.SaveChangesAsync();

            return applicantToUpdate.Id;
        }

        private void ValidateCommand(UpdateJobApplicantCommand command)
        {
            var validator = new UpdateJobApplicantCommandValidator();
            var validationResult = validator.Validate(command);
            var errorMessage = new StringBuilder();
            if (!validationResult.IsValid)
            {
                foreach (var failure in validationResult.Errors)
                {
                    errorMessage.AppendLine(@$"Property {failure.PropertyName} failed validation. Error was: {failure.ErrorMessage}");
                }

                throw new BadRequestException(errorMessage.ToString());
            }
        }

    }
}
