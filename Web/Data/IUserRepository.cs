using Web.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Web.Data
{
    public interface IUserRepository
    {
        Task<List<ApplicationUser>> GetUsersAsync();

        Task<ApplicationUser> GetUserById(string id);
    }
}
