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
    public class StudentsController : ControllerBase
    {

        private readonly AppDbContext _db;
        public StudentsController(AppDbContext db) => _db = db;

        [HttpPost]
        public async Task<IActionResult> Create(CreateStudentRequest req)
        {
            var exists = await _db.Students.AnyAsync(x => x.Email == req.Email);
            if (exists) return Conflict("Student with this email already exists.");

            var student = new Student { FullName = req.FullName, Email = req.Email };
            _db.Students.Add(student);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = student.Id }, student);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var student = await _db.Students.FindAsync(id);
            return student is null ? NotFound() : Ok(student);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
            => Ok(await _db.Students.OrderByDescending(x => x.CreatedAtUtc).ToListAsync());

    }
}
