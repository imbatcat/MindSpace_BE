using MindSpace.Application.Specifications;
using MindSpace.Domain.Entities.Tests;

public class TestCategorySpecification : BaseSpecification<TestCategory>
{
    public TestCategorySpecification(int testCategoryId) : base(x => x.Id == testCategoryId)
    {
    }

    // Get all test categories
    public TestCategorySpecification() : base(x => true)
    {
    }
}