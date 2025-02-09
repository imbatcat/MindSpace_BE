using MediatR;
using Microsoft.AspNetCore.Mvc;
using MindSpace.Application.DTOs;
using MindSpace.Application.Features.Questions.Queries.GetQuestions;
using MindSpace.Application.Specifications.QuestionSpecifications;
using MindSpace.Domain.Entities.Tests;

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
        public async Task<ActionResult<IReadOnlyList<Question>>> GetQuestions(
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
    }
}