

using AutoMapper;
using Microsoft.AspNetCore.Http;
using MindSpace.Application.Features.Tests.Commands.CreateTestImport;
using MindSpace.Domain.Entities.Tests;
using MindSpace.Domain.Exceptions;
using MindSpace.Domain.Interfaces.Repos;
using MindSpace.Domain.Interfaces.Services;

namespace MindSpace.Application.Services
{
    public class TestImportService : ITestImportService
    {
        private readonly IExcelReaderService _excelReaderService;

        public TestImportService(IExcelReaderService excelReaderService)
        {
            _excelReaderService = excelReaderService;
        }

        // common function to read and validate test file
        public async Task<Dictionary<string, List<Dictionary<string, string>>>> ReadAndValidateTestFileAsync(IFormFile file)
        {
            var fileData = await _excelReaderService.ReadAllSheetsAsync(file);

            if (!fileData.ContainsKey(CreateTestImportConstants.QUESTION_SHEET))
            {
                throw new InvalidFileFormatException($"File Excel thiếu sheet '{CreateTestImportConstants.QUESTION_SHEET}'");
            }
            if (!fileData.ContainsKey(CreateTestImportConstants.PSYCHOLOGY_TEST_OPTION_SHEET))
            {
                throw new InvalidFileFormatException($"File Excel thiếu sheet '{CreateTestImportConstants.PSYCHOLOGY_TEST_OPTION_SHEET}'");
            }


            return fileData;
        }

        // extract data of psychology test options from excel file
        public void InsertPsychologyTestOptions(Test testEntity, List<Dictionary<string, string>> optionsData)
        {
            if (optionsData == null || optionsData.Count == 0)
            {
                throw new InvalidFileFormatException("Sheet options không có dữ liệu.");
            }

            foreach (var option in optionsData)
            {
                if (!option.ContainsKey(CreateTestImportConstants.PSYCHOLOGY_TEST_OPTION_TEXT_COLUMN) ||
                    !option.ContainsKey(CreateTestImportConstants.PSYCHOLOGY_TEST_OPTION_SCORE_COLUMN))
                {
                    throw new InvalidFileFormatException("Thiếu cột dữ liệu trong sheet options.");
                }

                if (!int.TryParse(option[CreateTestImportConstants.PSYCHOLOGY_TEST_OPTION_SCORE_COLUMN], out int score))
                {
                    throw new InvalidFileFormatException("Điểm số của options không đúng định dạng số nguyên.");
                }

                // add new option to test
                var optionEntity = new PsychologyTestOption
                {
                    DisplayedText = option[CreateTestImportConstants.PSYCHOLOGY_TEST_OPTION_TEXT_COLUMN],
                    Score = score,
                    PsychologyTest = testEntity
                };
                testEntity.PsychologyTestOptions.Add(optionEntity);
            }
        }

        // extract data of questions from excel file
        public void InsertQuestions(Test testEntity, List<Dictionary<string, string>> questionData)
        {            
            if (questionData == null || questionData.Count == 0)
            {
                throw new InvalidFileFormatException("Sheet câu hỏi không có dữ liệu.");
            }

            foreach (var question in questionData)
            {
                if (!question.ContainsKey(CreateTestImportConstants.QUESTION_CONTENT_COLUMN))
                {
                    throw new InvalidFileFormatException("Thiếu cột nội dung câu hỏi.");
                }

                var questionEntity = new Question
                {
                    Content = question[CreateTestImportConstants.QUESTION_CONTENT_COLUMN]
                };

                // add new question to test
                testEntity.TestQuestions.Add(new TestQuestion
                {
                    Test = testEntity,
                    Question = questionEntity
                });
            }
        }

        // extract data of score ranks from excel file
        public void InsertScoreRanks(Test testEntity, List<Dictionary<string, string>> scoreRankData)
        {
            if (scoreRankData == null || scoreRankData.Count == 0)
            {
                return;
            }

            foreach (var scoreRank in scoreRankData)
            {
                if (
                    !scoreRank.ContainsKey(CreateTestImportConstants.SCORE_RANK_MIN_COLUMN) ||
                    !scoreRank.ContainsKey(CreateTestImportConstants.SCORE_RANK_MAX_COLUMN) ||
                    !scoreRank.ContainsKey(CreateTestImportConstants.SCORE_RANK_RESULT_COLUMN)
                    )
                {
                    throw new InvalidFileFormatException("Thiếu cột dữ liệu trong sheet score ranks.");
                }

                var scoreRankEntity = new TestScoreRank
                {
                    MinScore = int.Parse(scoreRank[CreateTestImportConstants.SCORE_RANK_MIN_COLUMN]),
                    MaxScore = int.Parse(scoreRank[CreateTestImportConstants.SCORE_RANK_MAX_COLUMN]),
                    Result = scoreRank[CreateTestImportConstants.SCORE_RANK_RESULT_COLUMN],
                    Test = testEntity
                };

                // add new score rank to test
                testEntity.TestScoreRanks.Add(scoreRankEntity);
            }
        }
    }
}
