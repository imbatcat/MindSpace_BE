using MediatR;
using Microsoft.AspNetCore.Http;
using MindSpace.Application.DTOs.Tests;


namespace MindSpace.Application.Features.Tests.Commands.CreateTestImport
{
    public class CreateTestImportCommand : IRequest<TestResponseDTO>
    {
        public CreateTestWithoutQuestionsDTO TestInfo { get; set; }
        public IFormFile TestFile { get; set; }
    }
}
