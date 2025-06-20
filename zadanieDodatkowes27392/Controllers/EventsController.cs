using Microsoft.AspNetCore.Mvc;
using zadanieDodatkowes27392.Exceptions;

namespace zadanieDodatkowes27392.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EventController : ControllerBase
{
    private readonly IDbService _service;

    public EventController(IDbService service)
    {
        _service = service;
    }

    [HttpPost("create")]
    public async Task<ActionResult<EventGetDto>> CreateEvent([FromBody] EventCreateDto dto)
    {
        try
        {
            return Ok(await _service.CreateEventAsync(dto));
        }
        catch (BadRequestException ex) { return BadRequest(new { message = ex.Message }); }
        catch (Exception ex)           { return StatusCode(500, new { message = "Internal error", error = ex.Message }); }
    }
    [HttpPost("{eventId}/add-speaker/{speakerId}")]
    public async Task<IActionResult> AddSpeaker(int eventId, int speakerId)
    {
        try
        {
            await _service.AddSpeakerToEventAsync(eventId, speakerId);
            return NoContent();
        }
        catch (BadRequestException ex) { return BadRequest(new { message = ex.Message }); }
        catch (NotFoundException ex)    { return NotFound(new { message = ex.Message }); }
        catch (Exception ex)           { return StatusCode(500, new { message = "Internal error", error = ex.Message }); }
    }

    [HttpPost("{eventId}/register/{participantId}")]
    public async Task<ActionResult<RegistrationResultDto>> Register(int eventId, int participantId)
    {
        try
        {
            return Ok(await _service.RegisterParticipantAsync(eventId, participantId));
        }
        catch (BadRequestException ex) { return BadRequest(new { message = ex.Message }); }
        catch (NotFoundException ex)    { return NotFound(new { message = ex.Message }); }
        catch (Exception ex)           { return StatusCode(500, new { message = "Internal error", error = ex.Message }); }
    }

    [HttpDelete("{eventId}/cancel/{participantId}")]
    public async Task<IActionResult> Cancel(int eventId, int participantId)
    {
        try
        {
            await _service.CancelRegistrationAsync(eventId, participantId);
            return NoContent();
        }
        catch (BadRequestException ex) { return BadRequest(new { message = ex.Message }); }
        catch (NotFoundException ex)    { return NotFound(new { message = ex.Message }); }
        catch (Exception ex)           { return StatusCode(500, new { message = "Internal error", error = ex.Message }); }
    }

    [HttpGet("upcoming")]
    public async Task<ActionResult<ICollection<EventGetDto>>> GetUpcoming()
    {
        try
        {
            return Ok(await _service.GetUpcomingEventsAsync());
        }
        catch (Exception ex) { return StatusCode(500, new { message = "Internal error", error = ex.Message }); }
    }
    [HttpGet("participant-report/{participantId}")]
    public async Task<ActionResult<ICollection<ParticipantEventReportDto>>> GetReport(int participantId)
    {
        try
        {
            return Ok(await _service.GetParticipantReportAsync(participantId));
        }
        catch (Exception ex) { return StatusCode(500, new { message = "Internal error", error = ex.Message }); }
    }
}