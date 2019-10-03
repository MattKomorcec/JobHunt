using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Threading.Tasks;
using Web.Data;
using Web.DTO_s;
using Web.Helpers;
using Web.Models;

namespace Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IJobRepository _jobRepository;

        public HomeController(IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            var jobs = _jobRepository.GetAllJobsIndexAsync(User, sortOrder, searchString);
            var allJobs = await _jobRepository.GetAllJobsQuery(User).ToListAsync();

            ViewData["CurrentSort"] = sortOrder;
            ViewData["DateSortParam"] = string.IsNullOrEmpty(sortOrder) ? "date_desc" : "";
            ViewData["CompanySortParam"] = sortOrder == "company" ? "company_desc" : "company";
            ViewData["StatusSortParam"] = sortOrder == "status" ? "status_desc" : "status";
            ViewData["PositionSortParam"] = sortOrder == "position" ? "position_desc" : "position";
            ViewData["CurrentFilter"] = searchString;

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            int pageSize = 10;
            var paginatedJobs = await PaginatedList<JobIndexDTO>.CreateAsync(jobs.AsNoTracking(), pageNumber ?? 1, pageSize);
            paginatedJobs.TotalJobs = allJobs.Count;
            paginatedJobs.TotalRejected = allJobs.FindAll(j => j.Status == JobStatus.Rejected).Count;
            paginatedJobs.TotalAccepted = allJobs.FindAll(j => j.Status == JobStatus.Accepted).Count;
            paginatedJobs.TotalInterviewing = allJobs.FindAll(j => j.Status == JobStatus.Interviewing).Count;

            return View(paginatedJobs);
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

            await _jobRepository.CreateJobAsync(job, User);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var job = await _jobRepository.GetJobAsync(id, User);

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

            var job = await _jobRepository.GetJobAsync(editedJob.JobId, User);

            if (job == null)
            {
                return NotFound("Somehow this job doesn't exist!");
            }

            await _jobRepository.EditJobAsync(editedJob, User);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var job = await _jobRepository.GetJobAsync(id, User);

            if (job == null)
            {
                return NotFound("Job not found!");
            }

            return View(job);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Job jobToDelete)
        {
            var job = await _jobRepository.GetJobAsync(jobToDelete.JobId, User);

            if (job == null)
            {
                return NotFound("Job not found!");
            }

            await _jobRepository.DeleteJobAsync(jobToDelete, User);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var job = await _jobRepository.GetJobAsync(id, User);

            if (job == null)
            {
                return NotFound("Job not found!");
            }

            return View(job);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }
    }
}
