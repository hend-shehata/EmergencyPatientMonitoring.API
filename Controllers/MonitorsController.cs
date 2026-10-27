using System.Security.Claims;
using EmergencyPatientMonitoring.API.Data;
using EmergencyPatientMonitoring.API.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmergencyPatientMonitoring.API.Controllers;

[ApiController]
[Route("api/monitors/me")]
[Authorize(Roles = "Monitor")]
public class MonitorsController : ControllerBase
{
    private readonly AppDbContext _db;
    public MonitorsController(AppDbContext db) => _db = db;

    private Guid UserId => Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    [HttpGet("patients")]
    public async Task<IActionResult> GetPatients()
    {
        var monitorId = await _db.Monitors.Where(x => x.UserId == UserId).Select(x => x.Id).FirstOrDefaultAsync();

        var list = await _db.PatientMonitors
            .Where(x => x.MonitorId == monitorId)
            .Select(x => new MonitoredPatientDto
            {
                PatientId = x.PatientId,
                Name = x.Patient.User.Name,
                PatientCode = x.Patient.PatientCode,
                RelationshipToPatient = x.RelationshipToPatient
            })
            .ToListAsync();

        return Ok(list);
    }
}