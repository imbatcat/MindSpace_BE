using MediatR;
using MindSpace.Application.DTOs;
using MindSpace.Application.Features.Questions.Specifications;
using MindSpace.Application.Features.SupportingPrograms.Specifications;
using MindSpace.Domain.Entities.SupportingPrograms;
using MindSpace.Domain.Entities.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
