using System.ComponentModel.DataAnnotations;

namespace Practical18_MVC.ViewModels;

public class StudentViewModel
{
    [Display(Name = "Student Id")]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Display(Name = "First Name")]
    public string? FirstName { get; set; }

    [Display(Name = "Last Name")]
    public string? LastName { get; set; }

    [Display(Name = "Email")]
    public string? Email { get; set; }

    [Display(Name = "Mobile Number")]
    public string? MobileNumber { get; set; }

    [Display(Name = "Address")]
    public string? Address { get; set; }
}
