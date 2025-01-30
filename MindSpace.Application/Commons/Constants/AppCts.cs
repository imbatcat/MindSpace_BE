using System.Reflection;

namespace MindSpace.Application.Commons.Constants
{
    public static class AppCts
    {

        /// <summary>
        /// All file path
        /// </summary>
        public static class Locations
        {
            public static readonly string AbsoluteProjectPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty;

            public static class RelativeFilePath
            {
                private static string PathToFakeDataFolder = Path.Combine("Seeders", "FakeData"); 

                public static string QuestionSeeder = Path.Combine(PathToFakeDataFolder, "Question.json");
                public static string SchoolSeeder = Path.Combine(PathToFakeDataFolder, "School.json");
                public static string Test = Path.Combine(PathToFakeDataFolder, "Test.json");
                public static string TestCategory = Path.Combine(PathToFakeDataFolder, "TestCategory.json");
                public static string TestQuestionOption = Path.Combine(PathToFakeDataFolder, "TestQuestionOption.json");
                public static string TestResponse = Path.Combine(PathToFakeDataFolder, "TestResponse.json");

                public static string PsychologistScheduleSeeder = Path.Combine(PathToFakeDataFolder, "PsychologistSchedule.json");
                public static string AppointmentSeeder = Path.Combine(PathToFakeDataFolder, "Appointment.json");
                public static string PaymentSeeder = Path.Combine(PathToFakeDataFolder, "Payment.json");
                public static string QuestionOptionSeeder = Path.Combine(PathToFakeDataFolder, "QuestionOption.json");
                public static string TestResponseItemSeeder = Path.Combine(PathToFakeDataFolder, "TestResponseItem.json");
                public static string TestScoreRankSeeder = Path.Combine(PathToFakeDataFolder, "TestScoreRank.json");
            }
        }
    }
}
