using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
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

            var job = await _dbContext.Jobs.FirstOrDefaultAsync(j => j.JobId == id && j.UserId == currentUserId);

            return job;
        }
    }
}
