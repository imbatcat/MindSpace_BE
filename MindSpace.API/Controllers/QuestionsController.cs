using MediatR;
using Microsoft.AspNetCore.Mvc;
using MindSpace.Application.DTOs.Tests;
using MindSpace.Application.Features.Questions.Queries.GetQuestionById;
using MindSpace.Application.Features.Questions.Queries.GetQuestions;
using MindSpace.Application.Specifications.QuestionSpecifications;

namespace MindSpace.API.Controllers;

public class QuestionsController(IMediator mediator) : BaseApiController
{
    // ====================================
    // === GET
    // ====================================

    // GET /api/questions
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<QuestionResponseDTO>>> GetQuestions(
        [FromQuery] QuestionSpecParams specParams)
    {
        var questionDtos = await mediator.Send(new GetQuestionsQuery(specParams));

        return PaginationOkResult<QuestionResponseDTO>(
            questionDtos.Data,
            questionDtos.Count,
            specParams.PageIndex,
            specParams.PageSize
        );
    }

    // GET /api/questions/{id}
    [HttpGet("{id:int}")]
    public async Task<ActionResult<QuestionResponseDTO>> GetQuestionById(int id)
    {
        var question = await mediator.Send(new GetQuestionByIdQuery(id));
        return Ok(question);
    }
}