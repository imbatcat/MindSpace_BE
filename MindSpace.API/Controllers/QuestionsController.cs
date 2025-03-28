using MediatR;
using Microsoft.AspNetCore.Mvc;
using MindSpace.API.RequestHelpers;
using MindSpace.Application.DTOs.Tests;
using MindSpace.Application.Features.Questions.Queries.GetQuestionById;
using MindSpace.Application.Features.Questions.Queries.GetQuestions;
using MindSpace.Application.Specifications.QuestionSpecifications;

namespace MindSpace.API.Controllers;

[Route("api/v{version:apiVersion}/questions")]
public class QuestionsController(IMediator mediator) : BaseApiController
{
    // ====================================
    // === GET
    // ====================================

    // GET /api/v1/questions
    // Get all questions with pagination and filtering
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

    // GET /api/v1/questions/{id}
    // Get a specific question by ID
    [HttpGet("{id:int}")]
    public async Task<ActionResult<QuestionResponseDTO>> GetQuestionById(int id)
    {
        var question = await mediator.Send(new GetQuestionByIdQuery(id));
        return Ok(question);
    }
}