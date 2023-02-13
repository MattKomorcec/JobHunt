using Microsoft.AspNetCore.Identity;

namespace Web.Models
{
  public class ApplicationUser : IdentityUser
    {
        public List<Job> Jobs { get; set; } = new();
    }
}
