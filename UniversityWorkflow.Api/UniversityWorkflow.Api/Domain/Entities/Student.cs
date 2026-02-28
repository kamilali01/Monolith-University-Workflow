namespace UniversityWorkflow.Api.Domain.Entities
{
    public class Student
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string FullName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
    }
}
