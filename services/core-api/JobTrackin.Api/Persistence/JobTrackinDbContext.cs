using JobTrackin.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace JobTrackin.Api.Persistence;

public sealed class JobTrackinDbContext : DbContext
{
    public JobTrackinDbContext(
        DbContextOptions<JobTrackinDbContext> options)
        : base(options)
    {
    }

    public DbSet<Application> Applications => Set<Application>();

    public DbSet<ApplicationStatus> ApplicationStatuses =>
        Set<ApplicationStatus>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        ConfigureApplicationStatus(modelBuilder);
        ConfigureApplication(modelBuilder);
    }

    private static void ConfigureApplicationStatus(
        ModelBuilder modelBuilder)
    {
        var entity = modelBuilder.Entity<ApplicationStatus>();

        entity.ToTable("APPLICATION_STATUS");

        entity.HasKey(x => x.Id);

        entity.Property(x => x.Id)
            .HasColumnName("ID")
            .HasMaxLength(50)
            .IsRequired();

        entity.Property(x => x.Code)
            .HasColumnName("CODE")
            .HasMaxLength(50)
            .IsRequired();

        entity.HasIndex(x => x.Code)
            .IsUnique();

        entity.Property(x => x.DisplayName)
            .HasColumnName("DISPLAY_NAME")
            .HasMaxLength(100)
            .IsRequired();

        entity.Property(x => x.SortOrder)
            .HasColumnName("SORT_ORDER")
            .IsRequired();

        entity.Property(x => x.IsActive)
            .HasColumnName("IS_ACTIVE")
            .IsRequired();

        entity.HasData(
            new ApplicationStatus
            {
                Id = "SAVED",
                Code = "SAVED",
                DisplayName = "Saved",
                SortOrder = 1,
                IsActive = true
            },
            new ApplicationStatus
            {
                Id = "APPLIED",
                Code = "APPLIED",
                DisplayName = "Applied",
                SortOrder = 2,
                IsActive = true
            },
            new ApplicationStatus
            {
                Id = "UNDER_REVIEW",
                Code = "UNDER_REVIEW",
                DisplayName = "Under Review",
                SortOrder = 3,
                IsActive = true
            },
            new ApplicationStatus
            {
                Id = "INTERVIEW",
                Code = "INTERVIEW",
                DisplayName = "Interview Scheduled",
                SortOrder = 4,
                IsActive = true
            },
            new ApplicationStatus
            {
                Id = "ASSESSMENT",
                Code = "ASSESSMENT",
                DisplayName = "Assessment",
                SortOrder = 5,
                IsActive = true
            },
            new ApplicationStatus
            {
                Id = "OFFER",
                Code = "OFFER",
                DisplayName = "Offer Received",
                SortOrder = 6,
                IsActive = true
            },
            new ApplicationStatus
            {
                Id = "REJECTED",
                Code = "REJECTED",
                DisplayName = "Rejected",
                SortOrder = 7,
                IsActive = true
            },
            new ApplicationStatus
            {
                Id = "WITHDRAWN",
                Code = "WITHDRAWN",
                DisplayName = "Withdrawn",
                SortOrder = 8,
                IsActive = true
            },
            new ApplicationStatus
            {
                Id = "ACCEPTED",
                Code = "ACCEPTED",
                DisplayName = "Offer Accepted",
                SortOrder = 9,
                IsActive = true
            },
            new ApplicationStatus
            {
                Id = "UNKNOWN",
                Code = "UNKNOWN",
                DisplayName = "Unknown",
                SortOrder = 10,
                IsActive = true
            }
        );
    }

    private static void ConfigureApplication(
        ModelBuilder modelBuilder)
    {
        var entity = modelBuilder.Entity<Application>();

        entity.ToTable("APPLICATION");

        entity.HasKey(x => x.Id);

        entity.Property(x => x.Id)
            .HasColumnName("ID");

        entity.Property(x => x.TenantId)
            .HasColumnName("TENANT_ID")
            .IsRequired();

        entity.Property(x => x.Company)
            .HasColumnName("COMPANY")
            .HasMaxLength(255)
            .IsRequired();

        entity.Property(x => x.RoleTitle)
            .HasColumnName("ROLE_TITLE")
            .HasMaxLength(255)
            .IsRequired();

        entity.Property(x => x.JobType)
            .HasColumnName("JOB_TYPE")
            .HasConversion<string>()
            .HasMaxLength(50)
            .IsRequired();

        entity.Property(x => x.Location)
            .HasColumnName("LOCATION")
            .HasMaxLength(255);

        entity.Property(x => x.AppliedAt)
            .HasColumnName("APPLIED_AT")
            .IsRequired();

        entity.Property(x => x.CurrentStatusId)
            .HasColumnName("CURRENT_STATUS_ID")
            .HasMaxLength(50)
            .IsRequired();

        entity.Property(x => x.WorkMode)
            .HasColumnName("WORK_MODE")
            .HasConversion<string>()
            .HasMaxLength(50)
            .IsRequired();

        entity.Property(x => x.ApplicationSource)
            .HasColumnName("APPLICATION_SOURCE_ID")
            .HasConversion<string>()
            .HasMaxLength(50);

        entity.Property(x => x.DiscoverySource)
            .HasColumnName("DISCOVERY_SOURCE_ID")
            .HasConversion<string>()
            .HasMaxLength(50)
            .IsRequired();

        entity.Property(x => x.SalaryMin)
            .HasColumnName("SALARY_MIN")
            .HasPrecision(18, 2);

        entity.Property(x => x.SalaryMax)
            .HasColumnName("SALARY_MAX")
            .HasPrecision(18, 2);

        entity.Property(x => x.CurrencyCode)
            .HasColumnName("CURRENCY_CODE")
            .HasMaxLength(10);

        entity.Property(x => x.JobUrl)
            .HasColumnName("JOB_URL")
            .HasMaxLength(2000);

        entity.Property(x => x.EmailReference)
            .HasColumnName("EMAIL_REFERENCE")
            .HasMaxLength(1000);

        entity.Property(x => x.Notes)
            .HasColumnName("NOTES")
            .HasMaxLength(4000);

        entity.Property(x => x.CreatedAt)
            .HasColumnName("CREATED_AT")
            .IsRequired();

        entity.Property(x => x.UpdatedAt)
            .HasColumnName("UPDATED_AT")
            .IsRequired();

        entity.HasIndex(x => new
        {
            x.TenantId,
            x.AppliedAt
        });

        entity.HasOne(x => x.CurrentStatus)
            .WithMany()
            .HasForeignKey(x => x.CurrentStatusId)
            .HasPrincipalKey(x => x.Id)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
