using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Web.DTO_s;
using Web.DTOs;
using Web.Models;

namespace Web.Data
{
    public interface IJobRepository
    {
        Task<List<Job>> GetAllJobsAsync(ClaimsPrincipal userId);

        (IQueryable<JobIndexDTO>, int) GetAllJobsIndexAsync(ClaimsPrincipal userId, string sortOrder, string searchString);

        IQueryable<Job> GetAllJobsQuery(ClaimsPrincipal userId);

        Task<Job?> GetJobAsync(int id, ClaimsPrincipal userId);

        Task CreateJobAsync(JobDto job, ClaimsPrincipal userId);

        Task EditJobAsync(Job job, ClaimsPrincipal userId);

        Task DeleteJobAsync(Job job, ClaimsPrincipal userId);
    }
}
