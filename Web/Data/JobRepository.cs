using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Web.DTO_s;
using Web.Models;

namespace Web.Data
{
    public class JobRepository : IJobRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;

        public JobRepository(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public async Task CreateJobAsync(Job job, ClaimsPrincipal userId)
        {
            // Get currently logged in user's id
            var currentUserId = _userManager.GetUserId(userId);

            job.UserId = currentUserId;

            // If a user didn't select a date, set it to today's date
            if (job.DateApplied == null)
            {
                job.DateApplied = DateTime.Now;
            }

            // Add a job and save it to the DB
            _dbContext.Add<Job>(job);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteJobAsync(Job jobToDelete, ClaimsPrincipal userId)
        {
            // Get currently logged in user's id
            var currentUserId = _userManager.GetUserId(userId);

            var job = await _dbContext.Jobs.FirstOrDefaultAsync(j => j.JobId == jobToDelete.JobId && j.UserId == currentUserId);

            _dbContext.Jobs.Remove(job);
            await _dbContext.SaveChangesAsync();
        }

        public async Task EditJobAsync(Job editedJob, ClaimsPrincipal userId)
        {
            // Get currently logged in user's id
            var currentUserId = _userManager.GetUserId(userId);

            var job = await _dbContext.Jobs.FirstOrDefaultAsync(j => j.JobId == editedJob.JobId && j.UserId == currentUserId);

            // Map all the fields to the existing job
            job.Company = editedJob.Company;
            job.Description = editedJob.Description;
            job.Location = editedJob.Location;
            job.DateApplied = editedJob.DateApplied;
            job.Language = editedJob.Language;
            job.Salary = editedJob.Salary;
            job.Link = editedJob.Link;
            job.Position = editedJob.Position;
            job.Status = editedJob.Status;

            // Save the edited job
            _dbContext.Jobs.Update(job);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Job>> GetAllJobsAsync(ClaimsPrincipal userId)
        {
            // Get currently logged in user's id
            var currentUserId = _userManager.GetUserId(userId);

            return await _dbContext.Jobs
                .Where(j => j.UserId == currentUserId)
                .ToListAsync();
        }

        public async Task<Job> GetJobAsync(int id, ClaimsPrincipal userId)
        {
            // Get currently logged in user's id
            var currentUserId = _userManager.GetUserId(userId);

            var job = await _dbContext.Jobs
                .AsNoTracking()
                .FirstOrDefaultAsync(j => j.JobId == id && j.UserId == currentUserId);

            return job;
        }

        /// <summary>
        /// This method returns all the jobs for the logged in user, sorted by provided sortOrder.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <param name="sortOrder"></param>
        /// <param name="searchString"></param>
        /// <returns></returns>
        public IQueryable<JobIndexDTO> GetAllJobsIndexAsync(ClaimsPrincipal userId, string sortOrder, string searchString)
        {
            // Get currently logged in user's id
            var currentUserId = _userManager.GetUserId(userId);

            // Get the query for all jobs, and map it to JobIndexDTO
            var query = _dbContext.Jobs
                .Where(j => j.UserId == currentUserId)
                .Select(j => new JobIndexDTO
                {
                    Company = j.Company,
                    DateApplied = j.DateApplied,
                    JobId = j.JobId,
                    Position = j.Position,
                    Status = j.Status
                });

            // If a user searched for a job using search
            if (!String.IsNullOrEmpty(searchString))
            {
                query = query.Where(j => j.Company.Contains(searchString) || j.Position.Contains(searchString));
            }

            // Depending on the sortOrder, order the jobs
            switch (sortOrder)
            {
                default:
                case "date":
                    query = query.OrderBy(j => j.DateApplied);
                    break;
                case "date_desc":
                    query = query.OrderByDescending(j => j.DateApplied);
                    break;
                case "position":
                    query = query.OrderBy(j => j.Position);
                    break;
                case "position_desc":
                    query = query.OrderByDescending(j => j.Position);
                    break;
                case "company":
                    query = query.OrderBy(j => j.Company);
                    break;
                case "company_desc":
                    query = query.OrderByDescending(j => j.Company);
                    break;
                case "status":
                    query = query.OrderBy(j => j.Status);
                    break;
                case "status_desc":
                    query = query.OrderByDescending(j => j.Status);
                    break;
            }

            return query.AsNoTracking();
        }

        /// <summary>
        /// This is a helper method that allows the DB connection to stay on, and be used in other methods.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        private IQueryable<Job> GetAllJobsQuery(ClaimsPrincipal userId)
        {
            // Get currently logged in user's id
            var currentUserId = _userManager.GetUserId(userId);

            return _dbContext.Jobs
                .Where(j => j.UserId == currentUserId)
                .AsQueryable();
        }
    }
}
