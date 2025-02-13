using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindSpace.Application.Features.Tests.Commands.CreateTestImport
{
    public class CreateTestImportConstants
    {
        public const string QUESTION_SHEET = "Questions";
        public const string QUESTION_CONTENT_COLUMN = "Question Content";

        public const string PSYCHOLOGY_TEST_OPTION_SHEET = "Options";
        public const string PSYCHOLOGY_TEST_OPTION_TEXT_COLUMN = "Displayed Text";
        public const string PSYCHOLOGY_TEST_OPTION_SCORE_COLUMN = "Score";

        public const string SCORE_RANK_SHEET = "Score Ranks";
        public const string SCORE_RANK_MIN_COLUMN = "Min Score";
        public const string SCORE_RANK_MAX_COLUMN = "Max Score";
        public const string SCORE_RANK_RESULT_COLUMN = "Result";
    }
}
