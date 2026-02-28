using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniversityWorkflow.Api.Contracts;
using UniversityWorkflow.Api.Domain.Entities;
using UniversityWorkflow.Api.Infrastructure.Data;

namespace UniversityWorkflow.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentsController : ControllerBase
    {

        private readonly AppDbContext _db;
        public EnrollmentsController(AppDbContext db) => _db = db;

        [HttpPost]
        public async Task<IActionResult> Create(CreateEnrollmentRequest req)
        {
            var studentExists = await _db.Students.AnyAsync(x => x.Id == req.StudentId);
            if (!studentExists) return BadRequest("Student not found.");

            var course = await _db.Courses.FirstOrDefaultAsync(x => x.Id == req.CourseId);
            if (course is null) return BadRequest("Course not found.");

            var approvedCount = await _db.Enrollments.CountAsync(x =>
                x.CourseId == req.CourseId && x.Status == EnrollmentStatus.Approved);

            if (approvedCount >= course.Capacity)
                return Conflict("Course capacity is full.");

            var already = await _db.Enrollments.AnyAsync(x =>
                x.StudentId == req.StudentId && x.CourseId == req.CourseId);

            if (already) return Conflict("Student already enrolled in this course.");

            var enrollment = new Enrollment
            {
                StudentId = req.StudentId,
                CourseId = req.CourseId,
                Status = EnrollmentStatus.Pending
            };

            _db.Enrollments.Add(enrollment);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = enrollment.Id }, enrollment);
        }

        [HttpPatch("{id:guid}/status")]
        public async Task<IActionResult> UpdateStatus(Guid id, [FromBody] UpdateEnrollmentStatusRequest req)
        {
            var enrollment = await _db.Enrollments.FindAsync(id);
            if (enrollment is null) return NotFound();

            enrollment.Status = req.Status;

            _db.AuditLogs.Add(new AuditLog
            {
                Action = "EnrollmentStatusChanged",
                EntityType = "Enrollment",
                EntityId = enrollment.Id,
                Details = $"NewStatus={enrollment.Status}"
            });

            if (enrollment.Status == EnrollmentStatus.Approved)
            {
                _db.AuditLogs.Add(new AuditLog
                {
                    Action = "NotificationSent",
                    EntityType = "Enrollment",
                    EntityId = enrollment.Id,
                    Details = "Email notification simulated."
                });
            }

            await _db.SaveChangesAsync();

            return Ok(enrollment);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var e = await _db.Enrollments.FindAsync(id);
            return e is null ? NotFound() : Ok(e);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
            => Ok(await _db.Enrollments.OrderByDescending(x => x.CreatedAtUtc).ToListAsync());

    }
}
