using Microsoft.AspNetCore.Identity;

namespace Farmer.Modern.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        
        public string LastName { get; set; }
    
        public string? Address { get; set; }
    }
}

