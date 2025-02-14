
using MediatR;
using MindSpace.Application.DTOs.Tests;

namespace MindSpace.Application.Features.Questions.Queries.GetQuestionById
{
    public class GetQuestionByIdQuery : IRequest<QuestionResponseDTO>
    {
        public int Id { get; private set; }
        public GetQuestionByIdQuery(int id)
        {
            Id = id;
        }
    }
}
