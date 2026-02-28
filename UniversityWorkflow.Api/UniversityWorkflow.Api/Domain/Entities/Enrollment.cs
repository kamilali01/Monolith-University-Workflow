namespace UniversityWorkflow.Api.Domain.Entities
{
    public enum EnrollmentStatus { Pending, Approved, Rejected }


    public class Enrollment
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid StudentId { get; set; }
        public Guid CourseId { get; set; }
        public EnrollmentStatus Status { get; set; } = EnrollmentStatus.Pending;
        public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
    }
}
