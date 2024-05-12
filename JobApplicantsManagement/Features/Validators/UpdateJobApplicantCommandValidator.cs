
using FluentValidation;
using JobApplicantsManagement.Features.Commands;

namespace JobApplicantsManagement.Features.Validators
{
    public class UpdateJobApplicantCommandValidator: AbstractValidator<UpdateJobApplicantCommand>
    {
        public UpdateJobApplicantCommandValidator()
        {
            RuleFor(c => c.Email)
                .EmailAddress();

            RuleFor(c => c.PhoneNumber)
                .Must(CommonValidator.IsPhoneNumberValid)
                .WithMessage("Phone number is invalid");

            RuleFor(c => c.AvailableFrom)
                .Must(t => t >= 0 && t <= 2359)
                .WithMessage("Time is invalid. Use military format; 0000 is midnight, 2395 is end of day")
                .Unless(t => t is null);

            RuleFor(c => c.AvailableFrom)
                .Must(t => t >= 0 && t <= 2359)
                .WithMessage("Time is invalid. Use military format; 0000 is midnight, 2395 is end of day")
                .Unless(t => t is null);

            RuleFor(c => c)
                .Must(c => c.AvailableFrom < c.AvailableTo)
                .Unless(c => c.AvailableTo is null || c.AvailableFrom is null)
                .WithMessage("Availability interval is invalid");
        }
    }
}
