using MediatR;
using Microsoft.AspNetCore.Mvc;
using MindSpace.Application.DTOs.Appointments;
using MindSpace.Application.Features.PsychologistSchedules.Queries;
using MindSpace.Application.Specifications.PsychologistScheduleSpecifications;

namespace MindSpace.API.Controllers
{
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
        /// Send a list of chosen PsychologistScheduleDTO from FE on a time interval (always a week, from FE-passed start date to FE-passed end date)
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        //[HttpPost]
        //public async Task<ActionResult> CreatePsychologistSchedule([FromBody] UpdatePsychologistScheduleCommand command)
        //{
        //    var result = await _mediator.Send(command);
        //    return CreatedAtAction(nameof(GetPsychologistScheduleById), new { result.Id }, null);
        //}

    }
}
