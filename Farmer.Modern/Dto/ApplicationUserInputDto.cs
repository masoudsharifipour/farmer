using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Farmer.Modern.Helper.Seeds;

namespace Farmer.Modern.Dto
{
    public class ApplicationUserInputDto
    {
        public string? Id { get; set; }

        [Key]
        [Required(ErrorMessage = "مقدار ورودی اجباری میباشد.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "مقدار ورودی اجباری میباشد.")]
        public string LastName { get; set; }

        [StringLength(40, MinimumLength = 5,
            ErrorMessage = "Password cannot be longer than 40 characters and less than 10 characters")]
        [Required(ErrorMessage = "مقدار ورودی اجباری میباشد.")]
        public string Password { get; set; }

        public string? Address { get; set; }
        public string PhoneNumber { get; set; }

        [DisplayName("سطح دسترسی")] public Roles Roles { get; set; }
    }
}