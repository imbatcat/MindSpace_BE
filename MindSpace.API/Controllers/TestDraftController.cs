using MediatR;
using Microsoft.AspNetCore.Mvc;
using MindSpace.Application.Features.Draft.Commands.DeleteTestDraft;
using MindSpace.Application.Features.Draft.Commands.UpdateTestDraft;
using MindSpace.Application.Features.Draft.Queries.GetTestDraftById;
using MindSpace.Domain.Entities.Drafts.TestPeriodics;

namespace MindSpace.API.Controllers;

public class TestDraftController(IMediator mediator) : BaseApiController
{
    [HttpGet("{id}")]
    public async Task<ActionResult<TestDraft>> GetTestDraftById([FromRoute] string id)
    {
        var testDraft = await mediator.Send(new GetTestDraftByIdQuery(id));
        return Ok(testDraft);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteTestDraft(
        [FromRoute] string id)
    {
        await mediator.Send(new DeleteTestDraftCommand(id));
        return NoContent();
    }

    [HttpPost]
    public async Task<ActionResult<TestDraft>> UpdateTestDraft(
        [FromBody] TestDraft testDraft)
    {
        var updatedTestDraft = await mediator.Send(new UpdateTestDraftCommand(testDraft));
        return Ok(updatedTestDraft);
    }
}
