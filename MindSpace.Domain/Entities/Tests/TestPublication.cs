using MindSpace.Domain.Entities.Constants;
using MindSpace.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindSpace.Domain.Entities.Tests
{
    public class TestPublication : BaseEntity
    {
        // 1 test - M test publications
        public int TestId { get; set; }
        public Test Test { get; set; }

        // 1 manager - M test publications
        public int ManagerId { get; set; }
        public Manager Manager { get; set; }

        public string? Title { get; set; }

        // Start Date and End Date of the test publication
        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public TestPublicationStatus TestPublicationStatus { get; set; }

        // 1 Test Publication - M Test Responses
        public virtual ICollection<TestResponse> TestResponses { get; set; } = new HashSet<TestResponse>();
    }
}
