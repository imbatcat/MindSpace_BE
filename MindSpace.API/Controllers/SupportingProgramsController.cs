using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MindSpace.API.RequestHelpers;
using MindSpace.Application.DTOs.SupportingPrograms;
using MindSpace.Application.Features.SupportingPrograms.Commands.CreateSupportingProgram;
using MindSpace.Application.Features.SupportingPrograms.Commands.PatchSupportingProgram;
using MindSpace.Application.Features.SupportingPrograms.Commands.RegisterSupportingProgram;
using MindSpace.Application.Features.SupportingPrograms.Commands.ToggleSupportingProgramStatus;
using MindSpace.Application.Features.SupportingPrograms.Commands.UnregisterSupportingProgram;
using MindSpace.Application.Features.SupportingPrograms.Queries.GetSupportingProgramByHistory;
using MindSpace.Application.Features.SupportingPrograms.Queries.GetSupportingProgramById;
using MindSpace.Application.Features.SupportingPrograms.Queries.GetSupportingPrograms;
using MindSpace.Application.Specifications.SupportingProgramHistorySpecifications;
using MindSpace.Application.Specifications.SupportingProgramSpecifications;
using MindSpace.Domain.Entities.Constants;

namespace MindSpace.API.Controllers;

//[Authorize]
[Route("api/v{version:apiVersion}/supporting-programs")]
public class SupportingProgramsController(IMediator mediator) : BaseApiController
{
    // ====================================
    // === GET
    // ====================================

    // GET /api/v1/supporting-programs
    // Get all supporting programs with pagination and filtering
    [Cache(300)]
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<SupportingProgramResponseDTO>>> GetSupportingPrograms(
        [FromQuery] SupportingProgramSpecParams specParams)
    {
        var pagedResultDTO = await mediator.Send(new GetSupportingProgramsQuery(specParams));

        return PaginationOkResult<SupportingProgramResponseDTO>(
            pagedResultDTO.Data,
            pagedResultDTO.Count,
            specParams.PageIndex,
            specParams.PageSize
        );
    }

    // GET /api/v1/supporting-programs/history?studentId={studentId}
    // Get supporting program history for a specific student
    [Cache(300)]
    [HttpGet("history")]
    public async Task<ActionResult<IReadOnlyList<SupportingProgramResponseDTO>>> GetSupportingProgramsHistory(
        [FromQuery] SupportingProgramHistorySpecParams specParams)
    {
        // Get from the Table Supporting Program History to track number of SP by Student Id
        var pagedResultDTO = await mediator.Send(new GetSupportingProgramByHistoryQuery(specParams));

        return PaginationOkResult<SupportingProgramResponseDTO>(
            pagedResultDTO.Data,
            pagedResultDTO.Count,
            specParams.PageIndex,
            specParams.PageSize
        );
    }

    // GET /api/v1/supporting-programs/{id}
    // Get a specific supporting program by ID
    [Cache(300)]
    [HttpGet("{id:int}")]
    public async Task<ActionResult<SupportingProgramSingleResponseDTO>> GetSupportingProgramById(
        [FromRoute] int id)
    {
        var supportProgram = await mediator.Send(new GetSupportingProgramByIdQuery(id));
        return Ok(supportProgram);
    }

    // ====================================
    // === CREATE, PATCH, DELETE, PUT
    // ====================================

    // POST /api/v1/supporting-programs
    // Create a new supporting program
    [InvalidateCache("/api/supporting-programs|")]
    [HttpPost]
    public async Task<ActionResult> CreateSupportingProgram(
        [FromBody] CreateSupportingProgramCommand newSP)
    {
        var createdSP = await mediator.Send(newSP);
        return CreatedAtAction(nameof(GetSupportingProgramById), new { createdSP.Id }, null);
    }

    // PATCH /api/v1/supporting-programs/{id}
    // Update a supporting program
    [InvalidateCache("/api/supporting-programs|")]
    [HttpPatch("{id:int}")]
    public async Task<ActionResult> PatchSupportingProgram(
       [FromRoute] int id,
       [FromBody] PatchSupportingProgramCommand updatedSP)
    {
        updatedSP.Id = id;
        await mediator.Send(updatedSP);
        return NoContent();
    }

    // POST /api/v1/supporting-programs/register
    // Register a student for a supporting program
    [InvalidateCache("/api/supporting-programs/history|")]
    [HttpPost("register")]
    public async Task<ActionResult> RegisterSupportingProgram(
        [FromBody] RegisterSupportingProgramCommand registerSP)
    {
        await mediator.Send(registerSP);
        return NoContent();
    }

    // POST /api/v1/supporting-programs/unregister
    // Unregister a student from a supporting program
    [InvalidateCache("/api/supporting-programs/history|")]
    [HttpPost("unregister")]
    public async Task<ActionResult> UnregisterSupportingProgram(
        [FromBody] UnregisterSupportingProgramCommand unregisterSP)
    {
        await mediator.Send(unregisterSP);
        return NoContent();
    }

    // PUT /api/v1/supporting-programs/{id}/toggle-status
    // Toggle the status of a supporting program (Admin and SchoolManager only)
    [InvalidateCache("/api/supporting-programs|")]
    [HttpPut("{id}/toggle-status")]
    [Authorize(Roles = $"{UserRoles.Admin},{UserRoles.SchoolManager}")]
    public async Task<IActionResult> ToggleSupportingProgramStatus([FromRoute] int id)
    {
        await mediator.Send(new ToggleSupportingProgramStatusCommand { Id = id });
        return NoContent();
    }
}