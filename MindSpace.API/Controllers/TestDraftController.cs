using MediatR;
using Microsoft.AspNetCore.Mvc;
using MindSpace.Application.Features.Draft.Commands.DeleteTestDraft;
using MindSpace.Application.Features.Draft.Commands.UpdateTestDraft;
using MindSpace.Application.Features.Draft.Queries.GetTestDraftById;
using MindSpace.Domain.Entities.Drafts.TestPeriodics;

namespace MindSpace.API.Controllers 
{
    
    public class TestDraftController : BaseApiController
    {

        // ====================================
        // === Props & Fields
        // ====================================

        private readonly IMediator _mediator;

        // ====================================
        // === Constructors
        // ====================================

        public TestDraftController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<TestDraft>> GetTestDraftById([FromRoute] string id)
        {
            var testDraft = await _mediator.Send(new GetTestDraftByIdQuery(id));
            return Ok(testDraft);
        }

        // ====================================
        // === COMMANDS
        // ====================================

        /// <summary>
        /// Delete a Test Draft
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteTestDraft(
            [FromRoute] string id)
        {
            await _mediator.Send(new DeleteTestDraftCommand(id));
            return NoContent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="testDraft"></param>
        /// <returns></returns>
        /// <exception cref="NotFoundException"></exception>
        [HttpPost]
        public async Task<ActionResult<TestDraft>> UpdateTestDraft(
            [FromBody] TestDraft testDraft)
        {
            // Set or update new
            var updatedTestDraft = await _mediator.Send(new UpdateTestDraftCommand(testDraft));

            return Ok(updatedTestDraft);
        }
    }
}
