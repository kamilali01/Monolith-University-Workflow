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
    public class CoursesController : ControllerBase
    {

        private readonly AppDbContext _db;
        public CoursesController(AppDbContext db) => _db = db;

        [HttpPost]
        public async Task<IActionResult> Create(CreateCourseRequest req)
        {
            var exists = await _db.Courses.AnyAsync(x => x.Code == req.Code);
            if (exists) return Conflict("Course with this code already exists.");

            var course = new Course { Code = req.Code, Title = req.Title, Capacity = req.Capacity };
            _db.Courses.Add(course);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = course.Id }, course);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var course = await _db.Courses.FindAsync(id);
            return course is null ? NotFound() : Ok(course);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
            => Ok(await _db.Courses.ToListAsync());

    }
}
