using System.ComponentModel.DataAnnotations;
using Web.Models;

namespace Web.DTOs;

public class JobDto
{
    public int JobId { get; set; }

    [Required]
    public string Company { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    [Required]
    public DateTime DateApplied { get; set; }

    [Required]
    public JobStatus Status { get; set; }

    public Language Language { get; set; }

    public string Position { get; set; } = string.Empty;

    public int Salary { get; set; }

    public string Link { get; set; }= string.Empty;

    public string Location { get; set; }= string.Empty;

    public string Benefits { get; set; }= string.Empty;


    public static explicit operator Job(JobDto jobDto)
    {
        return new Job
        {
            Company = jobDto.Company,
            Description = jobDto.Description,
            DateApplied = jobDto.DateApplied,
            Status = jobDto.Status,
            Language = jobDto.Language,
            Position = jobDto.Position,
            Salary = jobDto.Salary,
            Link = jobDto.Link,
            Location = jobDto.Location,
            Benefits = jobDto.Benefits
        };
    }
}

