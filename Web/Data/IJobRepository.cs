using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Web.Models;

namespace Web.Data
{
    public interface IJobRepository
    {
        Task<List<Job>> GetAllJobsAsync(ClaimsPrincipal userId);

        Task<Job> GetJobAsync(int id, ClaimsPrincipal userId);

        Task CreateJobAsync(Job job, ClaimsPrincipal userId);

        Task EditJobAsync(Job job, ClaimsPrincipal userId);

        Task DeleteJobAsync(Job job, ClaimsPrincipal userId);
    }
}
