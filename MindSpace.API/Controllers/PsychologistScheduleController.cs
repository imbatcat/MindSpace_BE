using MediatR;
using Microsoft.AspNetCore.Mvc;
using MindSpace.Application.DTOs.Appointments;
using MindSpace.Application.Features.PsychologistSchedules.Commands.UpdatePsychologistScheduleSimple;
using MindSpace.Application.Features.PsychologistSchedules.Queries.GetPsychologistSchedule;
using MindSpace.Application.Specifications.PsychologistScheduleSpecifications;

namespace MindSpace.API.Controllers
{
    [Route("api/v{version:apiVersion}/psychologist-schedules")]
    public class PsychologistScheduleController : BaseApiController
    {
        // props and fields
        private readonly IMediator _mediator;

        // constructors
        public PsychologistScheduleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<PsychologistScheduleResponseDTO>>> GetPsychologistSchedules([FromQuery] PsychologistScheduleSpecParams specParams)
        {
            var data = await _mediator.Send(new GetPsychologistScheduleQuery(specParams));
            return Ok(data);
        }


        // POST
        /// <summary>
        /// Send a list of chosen PsychologistScheduleDTO from FE in a time interval, simply send a list of timeslots without grouping by date or weekday
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> UpdatePsychologistSchedule([FromBody] UpdatePsychologistScheduleSimpleCommand command)
        {
            await _mediator.Send(command);
            return CreatedAtAction(nameof(GetPsychologistSchedules), new { psychologistId = command.PsychologistId, minDate = command.StartDate, maxDate = command.EndDate }, null);
        }

    }
}
