using System.Security.Claims;
using EmergencyPatientMonitoring.API.Data;
using EmergencyPatientMonitoring.API.DTOs;
using EmergencyPatientMonitoring.API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmergencyPatientMonitoring.API.Controllers;

[ApiController]
[Route("api/patients/me")]
[Authorize(Roles = "Patient")]
public class PatientsController : ControllerBase
{
    private readonly AppDbContext _db;
    public PatientsController(AppDbContext db) => _db = db;

    private Guid UserId => Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    [HttpGet]
    public async Task<IActionResult> GetMe()
    {
        var p = await _db.Patients
            .Include(x => x.User)
            .Include(x => x.HealthHistory)
            .FirstOrDefaultAsync(x => x.UserId == UserId);
        if (p is null) return NotFound();

        return Ok(new PatientProfileDto
        {
            Id = p.Id,
            Name = p.User.Name,
            Email = p.User.Email!,
            Phone = p.User.PhoneNumber,
            PatientCode = p.PatientCode,
            DateOfBirth = p.DateOfBirth,
            BloodType = p.BloodType,
            HealthHistory = p.HealthHistory is null ? null : new HealthHistoryDto
            {
                ChronicConditions = p.HealthHistory.ChronicConditions,
                Allergies = p.HealthHistory.Allergies
            }
        });
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdatePatientDto dto)
    {
        var p = await _db.Patients.Include(x => x.User).FirstOrDefaultAsync(x => x.UserId == UserId);
        if (p is null) return NotFound();

        p.User.Name = dto.Name;
        p.User.PhoneNumber = dto.Phone;
        p.DateOfBirth = dto.DateOfBirth;
        p.BloodType = dto.BloodType;

        await _db.SaveChangesAsync();
        return NoContent();
    }

    [HttpPut("health-history")]
    public async Task<IActionResult> UpsertHealthHistory(HealthHistoryDto dto)
    {
        var p = await _db.Patients.Include(x => x.HealthHistory).FirstOrDefaultAsync(x => x.UserId == UserId);
        if (p is null) return NotFound();

        if (p.HealthHistory is null)
            p.HealthHistory = new HealthHistory { Id = Guid.NewGuid(), PatientId = p.Id };

        p.HealthHistory.ChronicConditions = dto.ChronicConditions;
        p.HealthHistory.Allergies = dto.Allergies;

        await _db.SaveChangesAsync();
        return NoContent();
    }

    [HttpGet("monitors")]
    public async Task<IActionResult> GetMonitors()
    {
        var patientId = await _db.Patients.Where(x => x.UserId == UserId).Select(x => x.Id).FirstOrDefaultAsync();

        var list = await _db.PatientMonitors
            .Where(x => x.PatientId == patientId)
            .Select(x => new MonitorLinkDto
            {
                MonitorId = x.MonitorId,
                Name = x.Monitor.User.Name,
                Email = x.Monitor.User.Email!,
                Phone = x.Monitor.User.PhoneNumber,
                RelationshipToPatient = x.RelationshipToPatient,
                SmsVerified = x.SmsVerified,
                CallVerified = x.CallVerified,
                VerifiedAt = x.VerifiedAt
            })
            .ToListAsync();

        return Ok(list);
    }

    [HttpPost("monitors")]
    public async Task<IActionResult> AddMonitor(AddMonitorDto dto)
    {
        var patientId = await _db.Patients.Where(x => x.UserId == UserId).Select(x => x.Id).FirstOrDefaultAsync();
        if (patientId == Guid.Empty) return NotFound();

        var email = dto.MonitorEmail.ToUpperInvariant();
        var monitor = await _db.Monitors
            .Include(m => m.User)
            .FirstOrDefaultAsync(m => m.User.NormalizedEmail == email);
        if (monitor is null)
            return NotFound(new { message = "No monitor account found with this email." });

        var exists = await _db.PatientMonitors
            .AnyAsync(x => x.PatientId == patientId && x.MonitorId == monitor.Id);
        if (exists) return Conflict(new { message = "This monitor is already linked." });

        _db.PatientMonitors.Add(new PatientMonitor
        {
            PatientId = patientId,
            MonitorId = monitor.Id,
            RelationshipToPatient = dto.RelationshipToPatient
        });
        await _db.SaveChangesAsync();

        return StatusCode(StatusCodes.Status201Created);
    }

    [HttpDelete("monitors/{monitorId:guid}")]
    public async Task<IActionResult> RemoveMonitor(Guid monitorId)
    {
        var patientId = await _db.Patients.Where(x => x.UserId == UserId).Select(x => x.Id).FirstOrDefaultAsync();
        var link = await _db.PatientMonitors
            .FirstOrDefaultAsync(x => x.PatientId == patientId && x.MonitorId == monitorId);
        if (link is null) return NotFound();

        _db.PatientMonitors.Remove(link);
        await _db.SaveChangesAsync();
        return NoContent();
    }
}