using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindSpace.Domain.Entities.Tests
{
    public class TestScoreRank : BaseEntity
    {
        // 1 Test - M TestScoreRanks
        public int TestId { get; set; }
        public Test Test { get; set; }

        // Min Score and Max Score of a rank
        public int MinScore { get; set; }
        public int MaxScore { get; set; }

        // Displayed Text for a rank
        public string Description { get; set; }
    }
}
