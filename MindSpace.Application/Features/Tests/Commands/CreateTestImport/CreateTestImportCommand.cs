using MediatR;
using Microsoft.AspNetCore.Http;
using MindSpace.Application.DTOs.Tests;
using MindSpace.Domain.Entities.Constants;


namespace MindSpace.Application.Features.Tests.Commands.CreateTestImport
{
    public class CreateTestImportCommand : IRequest<TestOverviewResponseDTO>
    {
        public string Title { get; set; }
        public string TestCode { get; set; }
        public TargetUser? TargetUser { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int AuthorId { get; set; }
        public int? SchoolId { get; set; }
        public int TestCategoryId { get; set; }
        public int SpecializationId { get; set; }
        public IFormFile TestFile { get; set; }
    }
}
