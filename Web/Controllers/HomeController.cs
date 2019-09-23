using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;
using Web.Data;
using Web.Models;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IJobRepository _jobRepository;

        public HomeController(IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }

        public async Task<IActionResult> Index()
        {
            var jobs = await _jobRepository.GetAllJobsAsync(User);

            return View(jobs);
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

            return RedirectToAction("Index");
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

            return RedirectToAction("Index");
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

            return RedirectToAction("Index");
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
    }
}
