using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Practical_18_API.Entities;

public class Student
{
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    [Column(TypeName = "VARCHAR(50)")]
    public string? FirstName { get; set; }

    [Required]
    [Column(TypeName = "VARCHAR(50)")]
    public string? LastName { get; set; }

    [Required]
    [Column(TypeName = "VARCHAR(200)")]
    public string? Email { get; set; }

    [Required]
    [Column(TypeName = "VARCHAR(10)")]
    public string? MobileNumber { get; set; }

    [Column(TypeName = "VARCHAR(500)")]
    public string? Address { get; set; }
}
