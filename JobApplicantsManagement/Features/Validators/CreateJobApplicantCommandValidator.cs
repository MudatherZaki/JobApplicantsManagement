using FluentValidation;
using JobApplicantsManagement.Features.Commands;

namespace JobApplicantsManagement.Features.Validators
{
    public class CreateJobApplicantCommandValidator: AbstractValidator<CreateJobApplicantCommand>
    {
        public CreateJobApplicantCommandValidator()
        {
            RuleFor(c => c.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(c => c.FirstName)
                .NotEmpty();

            RuleFor(c => c.LastName)
                .NotEmpty();

            RuleFor(c => c.PhoneNumber)
                .Must(CommonValidator.IsPhoneNumberValid)
                .WithMessage("Phone number is invalid");

            RuleFor(c => c.AvailableFrom)
                .Must(t => t >= 0 && t <= 2395)
                .WithMessage("Time is invalid. Use military format; 0000 is midnight, 2395 is end of day");

            RuleFor(c => c.AvailableFrom)
                .Must(t => t >= 0 && t <= 2395)
                .WithMessage("Time is invalid. Use military format; 0000 is midnight, 2395 is end of day");

            RuleFor(c => c)
                .Must(c => c.AvailableFrom < c.AvailableTo)
                .WithMessage("Availability interval is invalid");

            RuleFor(c => c.Comment)
                .NotEmpty();
        }
    }
}
