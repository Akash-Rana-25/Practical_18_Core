using System.ComponentModel.DataAnnotations;

namespace Practical_18_API.Models;

public class CreateStudentDto
{
    [Required]
    [StringLength(50, MinimumLength = 3)]
    public string? FirstName { get; set; }

    [Required]
    [StringLength(50, MinimumLength = 3)]
    public string? LastName { get; set; }

    [Required]
    [StringLength(200, MinimumLength = 10)]
    public string? Email { get; set; }

    [Required]
    [StringLength(10, MinimumLength = 10)]
    public string? MobileNumber { get; set; }

    [StringLength(200)]
    public string? Address { get; set; }
}
