using Microsoft.AspNetCore.Identity;

namespace HMS6.Areas.Identity.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string image { get; set; }
    }
}
