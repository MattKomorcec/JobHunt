using System;
using System.ComponentModel.DataAnnotations;

namespace Web.Models;

public enum JobStatus
{
    Applied, Rejected, Accepted, Interviewing
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
    public string Company { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    [Required]
    [Display(Name = "Date applied")]
    public DateTime DateApplied { get; set; }

    [Required]
    public JobStatus Status { get; set; }

    public Language Language { get; set; }

    public string Position { get; set; } = string.Empty;

    public int Salary { get; set; }

    public string Link { get; set; } = string.Empty;

    public string Location { get; set; } = string.Empty;

    public string Benefits { get; set; } = string.Empty;

    public string UserId { get; set; } = string.Empty;
}
