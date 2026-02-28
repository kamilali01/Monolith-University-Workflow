using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniversityWorkflow.Api.Infrastructure.Data;

namespace UniversityWorkflow.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuditLogsController : ControllerBase
    {

        private readonly AppDbContext _db;
        public AuditLogsController(AppDbContext db) => _db = db;

        // last 50 log
        [HttpGet]
        public async Task<IActionResult> GetLatest()
        {
            var logs = await _db.AuditLogs
                .OrderByDescending(x => x.CreatedAtUtc)
                .Take(50)
                .ToListAsync();

            return Ok(logs);
        }

        // logs for specific entity
        [HttpGet("entity/{entityType}/{entityId:guid}")]
        public async Task<IActionResult> GetByEntity(string entityType, Guid entityId)
        {
            var logs = await _db.AuditLogs
                .Where(x => x.EntityType == entityType && x.EntityId == entityId)
                .OrderByDescending(x => x.CreatedAtUtc)
                .ToListAsync();

            return Ok(logs);
        }

    }
}
