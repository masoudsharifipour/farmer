using System.ComponentModel.DataAnnotations;

namespace Farmer.Modern.Dto;

public class ApplicationUserInputDto
{
    [Key]
    [Required(ErrorMessage = "مقدار ورودی اجباری میباشد.")]
    public string Name { get; set; }
    [Required(ErrorMessage = "مقدار ورودی اجباری میباشد.")]
    public string LastName { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    [StringLength(40, MinimumLength = 10 , ErrorMessage = "Password cannot be longer than 40 characters and less than 10 characters")]
    [Required(ErrorMessage = "مقدار ورودی اجباری میباشد.")]
    public string Password { get; set; }
    public string Address { get; set; }
    public string PhoneNumber { get; set; }
}