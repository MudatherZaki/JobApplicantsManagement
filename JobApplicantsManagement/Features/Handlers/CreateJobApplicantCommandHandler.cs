using JobApplicantsManagement.Features.Commands;
using JobApplicantsManagement.Features.Validators;
using JobApplicantsManagement.Infrastructure.Exceptions;
using JobApplicantsManagement.Infrastructure.Presistence;
using JobApplicantsManagement.Infrastructure.Presistence.Domain;
using MediatR;
using System.Text;

namespace JobApplicantsManagement.Features.Handlers
{
    public class CreateJobApplicantCommandHandler : IRequestHandler<CreateJobApplicantCommand, Guid>
    {
        private IApplicationDbContext _context;
        public CreateJobApplicantCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Guid> Handle(CreateJobApplicantCommand command, CancellationToken cancellationToken)
        {
            ValidateCommand(command);

            var applicantToCreate = new JobApplicant()
            {
                FirstName = command.FirstName!,
                AvailableFrom = command.AvailableFrom,
                AvailableTo = command.AvailableTo,
                Comment = command.Comment!,
                Email = command.Email!,
                GitHub = command.GitHub,
                LastName = command.LastName!,
                LinkedIn = command.LinkedIn,
                PhoneNumber = command.PhoneNumber
            };
            _context.JobApplicants.Add(applicantToCreate);
            await _context.SaveChangesAsync();

            return applicantToCreate.Id;
        }

        private void ValidateCommand(CreateJobApplicantCommand command)
        {
            var validator = new CreateJobApplicantCommandValidator();
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
