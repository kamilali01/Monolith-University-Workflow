namespace UniversityWorkflow.Api.Contracts
{
    public record CreateEnrollmentRequest(Guid StudentId, Guid CourseId);
}
