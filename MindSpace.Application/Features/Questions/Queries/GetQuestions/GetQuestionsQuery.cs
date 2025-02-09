using MediatR;
using MindSpace.Application.DTOs;
using MindSpace.Application.Specifications.QuestionSpecifications;

namespace MindSpace.Application.Features.Questions.Queries.GetQuestions
{
    public class GetQuestionsQuery : IRequest<PagedResultDTO<QuestionResponseDTO>>
    {
        public QuestionSpecParams SpecParams { get; private set; }

        public GetQuestionsQuery(QuestionSpecParams specParams)
        {
            this.SpecParams = specParams;
        }
    }
}