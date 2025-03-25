using MediatR;
using Microsoft.AspNetCore.Mvc;
using MindSpace.API.RequestHelpers;
using MindSpace.Application.DTOs.Appointments;
using MindSpace.Application.Features.PsychologistSchedules.Commands.UpdatePsychologistScheduleSimple;
using MindSpace.Application.Features.PsychologistSchedules.Queries.GetPsychologistSchedule;
using MindSpace.Application.Specifications.PsychologistScheduleSpecifications;

namespace MindSpace.API.Controllers;

[Route("api/v{version:apiVersion}/psychologist-schedules")]
public class PsychologistSchedulesController(IMediator mediator) : BaseApiController
{
    // GET /api/psychologist-schedules
    [Cache(30000)]
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<PsychologistScheduleResponseDTO>>> GetPsychologistSchedules([FromQuery] PsychologistScheduleSpecParams specParams)
    {
        var data = await mediator.Send(new GetPsychologistScheduleQuery(specParams));
        return Ok(data);
    }

    // POST /api/psychologist-schedules
    [InvalidateCache("/api/psychologist-schedules|")]
    [HttpPost]
    public async Task<ActionResult> UpdatePsychologistSchedule([FromBody] UpdatePsychologistScheduleSimpleCommand command)
    {
        await mediator.Send(command);
        return CreatedAtAction(nameof(GetPsychologistSchedules), new { psychologistId = command.PsychologistId, minDate = command.StartDate, maxDate = command.EndDate }, null);
    }
}
