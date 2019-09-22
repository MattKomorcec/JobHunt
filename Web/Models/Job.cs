using System;
using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public enum JobStatus
    {
        Applied, Rejected, Accepted
    }

    public enum Language
    {
        English,
        German,
        Spanish,
        French,
        Italian,
        Norwegian,
        Danish,
        Dutch,
        Swedish,
        Polish,
        Czech,
        Croatian
    }

    public class Job
    {
        public int JobId { get; set; }

        [Required]
        public string Company { get; set; }

        public string Description { get; set; }

        [Required]
        [Display(Name = "Date applied")]
        public DateTime DateApplied { get; set; }

        [Required]
        public JobStatus Status { get; set; }

        public Language Language { get; set; }

        public string Position { get; set; }

        public int Salary { get; set; }

        public string Link { get; set; }

        public string Location { get; set; }

        public string Benefits { get; set; }

        public string UserId { get; set; }
    }
}
