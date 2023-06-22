namespace Practical_18_Core.Models
{
    public class StudentDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? MobileNumber { get; set; }
        public string? Address { get; set; }
    }
}
