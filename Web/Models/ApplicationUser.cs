using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Web.Models
{
    public class ApplicationUser : IdentityUser
    {
        public List<Job> Jobs { get; set; }
    }
}
