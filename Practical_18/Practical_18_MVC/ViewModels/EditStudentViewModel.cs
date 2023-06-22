using System.ComponentModel.DataAnnotations;

namespace Practical18_MVC.ViewModels;

public class EditStudentViewModel
{
    [Display(Name = "Student Id")]
    [Required]
    public Guid Id { get; set; }

    [Display(Name = "First Name")]
    [Required]
    [StringLength(50, MinimumLength = 3)]
    public string? FirstName { get; set; }

    [Display(Name = "Last Name")]
    [Required]
    [StringLength(50, MinimumLength = 3)]
    public string? LastName { get; set; }

    [Display(Name = "Email")]
    [Required]
    [StringLength(200, MinimumLength = 10)]
    [EmailAddress]
    public string? Email { get; set; }

    [Display(Name = "Mobile Number")]
    [Required]
    [StringLength(10, MinimumLength = 10)]
    [Phone]
    public string? MobileNumber { get; set; }

    [Display(Name = "Address")]
    [MaxLength(500)]
    public string? Address { get; set; }
}
