using MediatR;

namespace JobApplicantsManagement.Features.Commands
{
    public class CreateJobApplicantCommand: IRequest<Guid>
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        public short? AvailableFrom { get; set; }
        public short? AvailableTo { get; set; }
        public string? LinkedIn { get; set; }
        public string? GitHub { get; set; }
        public string Comment { get; set; } = string.Empty;

        public CreateJobApplicantCommand()
        {
            
        }

        public CreateJobApplicantCommand(JobApplicantDto inputCommand)
        {
            AvailableFrom = inputCommand.AvailableFrom;
            AvailableTo = inputCommand.AvailableTo;
            Comment = inputCommand.Comment!;
            Email = inputCommand.Email!;
            FirstName = inputCommand.FirstName!;
            LastName = inputCommand.LastName!;
            GitHub = inputCommand.GitHub;
            LinkedIn = inputCommand.LinkedIn;
            PhoneNumber = inputCommand.PhoneNumber;
        }
    }
}
