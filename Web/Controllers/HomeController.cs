using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Web.Data;
using Web.Models;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            // Get currently logged in user's id
            var currentUserId = _userManager.GetUserId(User);

            // Find all jobs for that user
            var jobs = await _dbContext.Jobs
                .Where(j => j.UserId == currentUserId)
                .ToListAsync();

            // Simply return the jobs for this user
            return View(jobs);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Job job)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            // Get currently logged in user's id
            var currentUserId = _userManager.GetUserId(User);

            job.UserId = currentUserId;

            // If a user didn't select a date, set it to today's date
            if (job.DateApplied == null)
            {
                job.DateApplied = DateTime.Now;
            }

            // Add a job and save it to the DB
            _dbContext.Add<Job>(job);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var job = await _dbContext.Jobs.FirstOrDefaultAsync(j => j.JobId == id);

            if (job == null)
            {
                return NotFound("Job not found!");
            }

            return View(job);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Job editedJob)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            // Find a job
            var job = await _dbContext.Jobs.FirstOrDefaultAsync(j => j.JobId == editedJob.JobId);

            // This shouldn't happen, but if it somehow does, return a 404
            if (job == null)
            {
                return NotFound("Somehow this job doesn't exist!");
            }

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

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var job = await _dbContext.Jobs.FirstOrDefaultAsync(j => j.JobId == id);

            if (job == null)
            {
                return NotFound("Job not found!");
            }

            return View(job);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Job jobToDelete)
        {
            var job = await _dbContext.Jobs.FirstOrDefaultAsync(j => j.JobId == jobToDelete.JobId);

            if (job == null)
            {
                return NotFound("Job not found!");
            }

            _dbContext.Jobs.Remove(job);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
