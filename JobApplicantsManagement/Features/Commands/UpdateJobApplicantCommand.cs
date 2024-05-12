using MediatR;

namespace JobApplicantsManagement.Features.Commands
{
    public class UpdateJobApplicantCommand: IRequest<Guid>
    {
        public Guid Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public short? AvailableFrom { get; set; }
        public short? AvailableTo { get; set; }
        public string? LinkedIn { get; set; }
        public string? GitHub { get; set; }
        public string? Comment { get; set; }

        public UpdateJobApplicantCommand()
        {
            
        }

        public UpdateJobApplicantCommand(Guid id, JobApplicantDto inputCommand)
        {
            Id = id;
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
