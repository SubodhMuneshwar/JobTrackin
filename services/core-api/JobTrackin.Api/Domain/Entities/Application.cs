using Enums = JobTrackin.Api.Domain.Enums;

namespace JobTrackin.Api.Domain.Entities;

public sealed class Application
{
    public Guid Id { get; set; } = Guid.NewGuid();

    // Tenant boundary (mandatory per Section 1 & 5)
    public Guid TenantId { get; set; }

    public string Company { get; set; } = string.Empty;
    public string RoleTitle { get; set; } = string.Empty;

    public Enums.JobType JobType { get; set; } = Enums.JobType.Job;
    public Enums.WorkMode WorkMode { get; set; } = Enums.WorkMode.Onsite;

    public string? Location { get; set; }

    // UTC DateTime: drives the dashboard newest-first sorting
    public DateTime AppliedAt { get; set; } = DateTime.UtcNow;

    // Natural status code matching APPLICATION_STATUSES lookup
    public string CurrentStatusId { get; set; } = "APPLIED";

    // Separate where it was posted from how JobTrackin found it
    public Enums.ApplicationSource? ApplicationSource { get; set; } = Enums.ApplicationSource.Unknown;
    public Enums.DiscoverySource DiscoverySource { get; set; } = Enums.DiscoverySource.Manual;

    // Compensation
    public decimal? SalaryMin { get; set; }
    public decimal? SalaryMax { get; set; }
    public string? CurrencyCode { get; set; } = "INR";

    // Direct navigation links
    public string? JobUrl { get; set; }
    public string? EmailReference { get; set; }
    public string? Notes { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}