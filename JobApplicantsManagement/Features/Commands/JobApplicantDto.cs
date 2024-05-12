using MediatR;

namespace JobApplicantsManagement.Features.Commands
{
    public class JobApplicantDto
    {
        public string? FirstName { get; set; } 
        public string? LastName { get; set; } 
        public string? Email { get; set; } 
        public string? PhoneNumber { get; set; }
        public short? AvailableFrom { get; set; }
        public short? AvailableTo { get; set; }
        public string? LinkedIn { get; set; }
        public string? GitHub { get; set; }
        public string? Comment { get; set; }

    }
}
