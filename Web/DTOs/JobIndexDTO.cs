using System;
using Web.Models;

namespace Web.DTO_s
{
  public class JobIndexDTO
  {
    public int JobId { get; set; }

    public string Company { get; set; } = string.Empty;

    public string Position { get; set; } = string.Empty;

    public DateTime DateApplied { get; set; }

    public JobStatus Status { get; set; }
  }
}
