namespace JobApplicantsManagement.Infrastructure.Presistence.Domain
{
    public class JobApplicant
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        public short? AvailableFrom { get; set; }
        public short? AvailableTo { get; set; }
        public string? LinkedIn { get; set; }
        public string? GitHub { get; set; }
        public string Comment { get; set; } = string.Empty;
    }
}
