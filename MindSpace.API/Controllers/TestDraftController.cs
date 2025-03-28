using MediatR;
using Microsoft.AspNetCore.Mvc;
using MindSpace.Application.Features.Draft.Commands.DeleteTestDraft;
using MindSpace.Application.Features.Draft.Commands.UpdateTestDraft;
using MindSpace.Application.Features.Draft.Queries.GetTestDraftById;
using MindSpace.Domain.Entities.Drafts.TestPeriodics;

namespace MindSpace.API.Controllers;

[Route("api/v{version:apiVersion}/test-draft")]
public class TestDraftController(IMediator mediator) : BaseApiController
{
    // ====================================
    // === GET
    // ====================================

    // GET /api/v1/test-draft/{id}
    // Get a test draft by ID
    [HttpGet("{id}")]
    public async Task<ActionResult<TestDraft>> GetTestDraftById([FromRoute] string id)
    {
        var testDraft = await mediator.Send(new GetTestDraftByIdQuery(id));
        return Ok(testDraft);
    }

    // ==============================
    // === POST, PUT, DELETE, PATCH
    // ==============================

    // DELETE /api/v1/test-draft/{id}
    // Delete a test draft
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteTestDraft(
        [FromRoute] string id)
    {
        await mediator.Send(new DeleteTestDraftCommand(id));
        return NoContent();
    }

    // POST /api/v1/test-draft
    // Create or update a test draft
    [HttpPost]
    public async Task<ActionResult<TestDraft>> UpdateTestDraft(
        [FromBody] TestDraft testDraft)
    {
        var updatedTestDraft = await mediator.Send(new UpdateTestDraftCommand(testDraft));
        return Ok(updatedTestDraft);
    }
}
