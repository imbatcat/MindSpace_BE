using System.Reflection;

namespace MindSpace.Application.Commons.Constants
{
    public static class AppCts
    {
        public static class CheckoutSession
        {
            public static readonly int CheckoutSessionExpireTimeInMinutes = 15;
        }

        /// <summary>
        /// All file path
        /// </summary>
        public static class Locations
        {
            public static readonly string AbsoluteProjectPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty;

            public static class RelativeFilePath
            {
                private static string PathToFakeDataFolder = Path.Combine(AbsoluteProjectPath, "Seeders", "FakeData");

                public static string SpecializationSeeder = Path.Combine(PathToFakeDataFolder, "Specialization.json");
                public static string SchoolSeeder = Path.Combine(PathToFakeDataFolder, "School.json");

                public static string TestCategorySeeder = Path.Combine(PathToFakeDataFolder, "TestCategory.json");
                public static string TestSeeder = Path.Combine(PathToFakeDataFolder, "Test.json");
                public static string TestScoreRankSeeder = Path.Combine(PathToFakeDataFolder, "TestScoreRank.json");
                public static string QuestionSeeder = Path.Combine(PathToFakeDataFolder, "Question.json");
                public static string QuestionOptionSeeder = Path.Combine(PathToFakeDataFolder, "QuestionOption.json");
                public static string TestQuestionSeeder = Path.Combine(PathToFakeDataFolder, "TestQuestion.json");
                public static string PsychologyTestOptionSeeder = Path.Combine(PathToFakeDataFolder, "PsychologyTestOption.json");
                public static string TestResponseSeeder = Path.Combine(PathToFakeDataFolder, "TestResponse.json");
                public static string TestResponseItemSeeder = Path.Combine(PathToFakeDataFolder, "TestResponseItem.json");

                public static string PsychologistScheduleSeeder = Path.Combine(PathToFakeDataFolder, "PsychologistSchedule.json");
                public static string AppointmentSeeder = Path.Combine(PathToFakeDataFolder, "Appointment.json");
                public static string PaymentSeeder = Path.Combine(PathToFakeDataFolder, "Payment.json");

                public static string SupportingProgramSeeder = Path.Combine(PathToFakeDataFolder, "SupportingProgram.json");
                public static string FeedbackSeeder = Path.Combine(PathToFakeDataFolder, "Feedback.json");
                public static string SupportingProgramHistorySeeder = Path.Combine(PathToFakeDataFolder, "SupportingProgramHistory.json");
                public static string ResourceSeeder = Path.Combine(PathToFakeDataFolder, "Resource.json");
                public static string ResourceSectionsSeeder = Path.Combine(PathToFakeDataFolder, "ResourceSections.json");
            }
        }

        /// <summary>
        /// Default password for all registered users
        /// </summary>
        public static string DefaultPassword = "Password1!";

        /// <summary>
        /// Database sharding for 1 instance (1 server)
        /// </summary>
        public static class Redis
        {
            public static int DatabaseNo_Blog = 1; // For Blog Draft
            public static int DatabaseNo_Test = 2; // For Test Draft
            public static int DatabaseNo_Response = 3; // For Caching Response
        }
        public static class StripePayment
        {
            /// <summary>
            /// The time in minutes before a checkout session expires
            /// </summary>
            public static readonly int CheckoutSessionExpireTimeInMinutes = 15;

            /// <summary>
            /// The currency used for payment processing
            /// </summary>
            public static readonly string PaymentCurrency = "VND";


            public enum StripeCheckoutSessionStatus
            {
                open,
                expired,
                completed
            }
        }

        public static class WebRTC
        {
            /// <summary>
            /// The extra duration of the room in minutes, the room will be created before the meeting start time by 10 minutes, an extra 5 minutes in case that the client join the meeting late
            /// </summary>
            public static readonly int ExtraRoomDurationInMinutes = 15;

            /// <summary>
            /// The extra lifetime of the room in minutes in case the client request to extend the room lifetime, the room will be deleted after the meeting end time by 20 minutes
            /// </summary>
            public static readonly int ExtraRoomLifetimeInMinutes = 20;
        }

    }
}