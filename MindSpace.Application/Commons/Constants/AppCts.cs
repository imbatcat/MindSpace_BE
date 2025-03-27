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
            public static int DatabaseNo_Job = 4; // For Caching Job
        }

        /// <summary>
        /// Stripe Payment Constants
        /// </summary>
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

            public static readonly string Provider = "Stripe";

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
            /// The extra lifetime of the room in minutes in case the client request to extend the room lifetime, the room will be deleted after the meeting end time by 20 minutes
            /// </summary>
            /// NOT YET DEVELOPED
            public static readonly int ExtraRoomLifetimeInMinutes = 20;
        }

        public static class AiChatKeywords
        {
            // Spec Keywords
            public static string[] SpecificationsKeywords = {
                // Vietnamese
                "Tâm lý", "Chuyên ngành tâm lý", "Tư vấn tâm lý", "Trị liệu tâm lý",
                "Hành vi", "Cảm xúc", "Sức khỏe tâm thần", "Tâm lý học lâm sàng",
                "Tâm lý học trẻ em", "Tâm lý học xã hội", "Tâm lý học nhận thức",
                "Trầm cảm", "Lo âu", "Rối loạn tâm lý", "Phát triển tâm lý",
                "Tự kỷ", "Giới tính", "Tình yêu", "Mối quan hệ", "Đánh giá tâm lý",
                
                // English
                "Psychology", "Psychological Specialization", "Psychological Counseling", "Psychological Therapy",
                "Behavior", "Emotion", "Mental Health", "Clinical Psychology",
                "Child Psychology", "Social Psychology", "Cognitive Psychology",
                "Depression", "Anxiety", "Psychological Disorders", "Psychological Development",
                "Autism", "Gender Psychology", "Love Psychology", "Relationship Psychology", "Psychological Assessment"
            };

            public static string[] PsychologicalKeywords =
            {
                // Vietnamese Names
                "Bài kiểm tra IQ", "Bài kiểm tra cảm xúc", "Bài kiểm tra trí tuệ cảm xúc",
                "Bài kiểm tra rối loạn lo âu", "Bài kiểm tra trầm cảm", "Bài kiểm tra nhân cách",
                "Bài kiểm tra hành vi", "Bài kiểm tra tự kỷ", "Bài kiểm tra tâm lý học phát triển",
                "Bài kiểm tra", "Kiểm tra",
    
                // English Names
                "Psychology Test", "Emotional Test", "Emotional Intelligence Test",
                "Anxiety Disorder Test", "Depression Test", "Personality Test",
                "Behavior Test", "Autism Test", "Developmental Psychology Test",
                "Test", "Periodict Test", "Periodic", "GPD7", "MMPI-2", "CPI", "Rorschach", "WAIS", "Bender-Gestalt",
                "SAT", "TAT", "MCMI", "Raven's Progressive Matrices", "Beck Depression Inventory",
                "Minnesota Multiphasic Personality Inventory", "WISC", "Piers-Harris",
                "DASS-21", "HADS", "Zung Self-Rating Depression Scale", "Hamilton Anxiety Scale",
                "Wechsler Memory Scale", "Test of Variables of Attention (TOVA)", "Cattell's 16PF"
            };

            public static string[] SupportingPrograms =
            {
                // Vietnamese Program Names
                "Chương trình tư vấn tâm lý", "Hỗ trợ sức khỏe tinh thần", "Tư vấn hôn nhân", "Chương trình quản lý stress",
                "Hỗ trợ trẻ em", "Chương trình phục hồi chức năng tâm lý", "Tư vấn nhóm", "Tư vấn trực tuyến",
                "Chương trình tâm lý học lâm sàng", "Hỗ trợ cộng đồng", "Chương trình ngừng sử dụng chất kích thích",
                "Hỗ trợ tâm lý sau chấn thương", "Tư vấn trị liệu hành vi nhận thức", "Chương trình rối loạn lo âu",
                "Chương trình rối loạn trầm cảm", "Chương trình cho phụ nữ mang thai", "Chương trình hỗ trợ nhân viên",
                "Chương trình tư vấn cho người cao tuổi", "Tư vấn cho người bị tổn thương tâm lý", "Chương trình phòng chống bạo lực gia đình"
            };
        }
    }
}