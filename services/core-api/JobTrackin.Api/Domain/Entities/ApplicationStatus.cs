namespace JobTrackin.Api.Domain.Entities;

public sealed class ApplicationStatus
{
    public string Id { get; set; } = string.Empty;

    public string Code { get; set; } = string.Empty;

    public string DisplayName { get; set; } = string.Empty;

    public int SortOrder { get; set; }

    public bool IsActive { get; set; } = true;
}