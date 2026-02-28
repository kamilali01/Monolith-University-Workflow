namespace UniversityWorkflow.Api.Domain.Entities
{
    public class AuditLog
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Action { get; set; } = default!;
        public string EntityType { get; set; } = default!;
        public Guid EntityId { get; set; }
        public string? Details { get; set; }
        public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
    }
}
