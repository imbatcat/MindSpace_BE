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

    /// <summary>
    /// Get Questions By Params and Support Pagination
    /// </summary>
    /// <param name="specParams"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Get Question By Id
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id:int}")]
    public async Task<ActionResult<QuestionResponseDTO>> GetQuestionById(int id)
    {
        var question = await mediator.Send(new GetQuestionByIdQuery(id));
        return Ok(question);
    }
}