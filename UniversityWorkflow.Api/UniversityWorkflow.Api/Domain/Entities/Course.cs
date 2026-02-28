namespace UniversityWorkflow.Api.Domain.Entities
{
    public class Course
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Code { get; set; } = default!;
        public string Title { get; set; } = default!;
        public int Capacity { get; set; }
    }
}
