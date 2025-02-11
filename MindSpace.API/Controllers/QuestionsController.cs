using MediatR;
using Microsoft.AspNetCore.Mvc;
using MindSpace.Application.DTOs.Tests;
using MindSpace.Application.Features.Questions.Queries.GetQuestionById;
using MindSpace.Application.Features.Questions.Queries.GetQuestions;
using MindSpace.Application.Specifications.QuestionSpecifications;

namespace MindSpace.API.Controllers
{
    public class QuestionsController : BaseApiController
    {
        // ====================================
        // === Props & Fields
        // ====================================

        private readonly IMediator _mediator;

        // ====================================
        // === Constructors
        // ====================================

        public QuestionsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // ====================================
        // === GET
        // ====================================

        /// <summary>
        /// Get Questions By Params and Support Pagination
        /// </summary>
        /// <param name="specParams"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<QuestionResponseDTO>>> GetQuestions(
            [FromQuery] QuestionSpecParams specParams)
        {
            var questionDtos = await _mediator.Send(new GetQuestionsQuery(specParams));

            return PaginationOkResult<QuestionResponseDTO>(
                questionDtos.Data,
                questionDtos.Count,
                specParams.PageIndex,
                specParams.PageSize
            );
        }

        /// <summary>
        /// Get Question By Id
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<QuestionResponseDTO>> GetQuestionById(int id)
        {
            var question = await _mediator.Send(new GetQuestionByIdQuery(id));
            return Ok(question);
        }
    }
}