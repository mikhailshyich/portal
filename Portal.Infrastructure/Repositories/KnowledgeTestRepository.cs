using DocumentFormat.OpenXml.Drawing.Charts;
using Microsoft.EntityFrameworkCore;
using Portal.Domain.DTOs;
using Portal.Domain.Entities.KnowledgeTests;
using Portal.Domain.Interfaces;
using Portal.Domain.Responses;
using Portal.Infrastructure.Data;

namespace Portal.Infrastructure.Repositories
{
    public class KnowledgeTestRepository : IKnowledgeTestDomain
    {
        private readonly PortalDbContext context;

        public KnowledgeTestRepository(PortalDbContext context)
        {
            this.context = context;
        }

        public async Task<CustomGeneralResponses> AddQuestionAnswerAsync(List<QuestionAnswerDTO> request)
        {
            if (request is null) return new CustomGeneralResponses(false, "Передаваемый объект равен null.");
            if (request.Count == 0) return new CustomGeneralResponses(false, "Передаваемый список с ответами пуст.");
            var uploadedAnswers = new List<QuestionAnswer>();

            foreach (var answer in request) 
            {
                if (answer.QuestionId != Guid.Empty)
                {
                    var question = await context.TestQuestions.FirstOrDefaultAsync(q => q.Id == answer.QuestionId);
                    if (question != null)
                    {
                        if (!string.IsNullOrEmpty(answer.AnswerText))
                        {
                            var questionAnswer = new QuestionAnswer()
                            {
                                QuestionId = answer.QuestionId,
                                AnswerText = answer.AnswerText,
                                IsRight = answer.IsRight
                            };
                            uploadedAnswers.Add(questionAnswer);
                        }
                    }
                }
            }
            if (uploadedAnswers.Count > 0) 
            {
                context.QuestionAnswers.AddRange(uploadedAnswers);
                await context.SaveChangesAsync();
                return new CustomGeneralResponses(true, $"Ответы успешно добавлены! Количество добавленных ответов - {uploadedAnswers.Count}.");
            }
            else
            {
                return new CustomGeneralResponses(false, $"Ответы на вопрос не добавлены. Проверьте введённые значения.");
            }

        }

        public async Task<CustomGeneralResponses> AddQuestionAsync(List<TestQuestionDTO> request)
        {
            if (request is null) return new CustomGeneralResponses(false, "Передаваемый объект равен null.");
            if (request.Count == 0) return new CustomGeneralResponses(false, "Передаваемый список с вопросами пуст.");
            var uploadedQuestions = new List<TestQuestion>();

            foreach(var question in request)
            {
                if (question.KnowledgeTestId != Guid.Empty)
                {
                    var knowledgeTest = await context.KnowledgeTests.FirstOrDefaultAsync(k => k.Id == question.KnowledgeTestId);
                    if (knowledgeTest != null) 
                    {
                        if (!string.IsNullOrEmpty(question.QuestionText))
                        {
                            var questionDB = new TestQuestion()
                            {
                                KnowledgeTestId = question.KnowledgeTestId,
                                QuestionText = question.QuestionText,
                            };
                            context.TestQuestions.Add(questionDB);
                            await context.SaveChangesAsync();
                            uploadedQuestions.Add(questionDB);
                        }
                    }
                }
            }

            return new CustomGeneralResponses(true, $"Вопросы для теста успешно загружены. Количество загруженных вопросов - {uploadedQuestions.Count}.", uploadedQuestions);
        }

        public async Task<CustomGeneralResponses> AddTestAsync(KnowledgeTestDTO request)
        {
            if (request is null) return new CustomGeneralResponses(false, "Передаваемый объект равен null.");
            if (request.ResponsibleId == Guid.Empty) return new CustomGeneralResponses(false, "Передаваемый id ответственного пустой");
            if (string.IsNullOrEmpty(request.TestName)) return new CustomGeneralResponses(false, "Название теста не может быть пустым.");

            var responsible = await context.Users.FirstOrDefaultAsync(r => r.Id == request.ResponsibleId);
            if (responsible is null) return new CustomGeneralResponses(false, $"Пользователя с id {request.ResponsibleId} не найдено.");

            var knowledgeTest = new KnowledgeTest()
            {
                ResponsibleId = request.ResponsibleId,
                TestName = request.TestName,
                DateTimeCreated = DateTime.Now
            };

            if (request.DepartmentId != Guid.Empty) 
            {
                knowledgeTest.DepartmentId = request.DepartmentId;
            }

            context.KnowledgeTests.Add(knowledgeTest);
            await context.SaveChangesAsync();

            return new CustomGeneralResponses(true, $"Тест [{request.TestName}] успешно добавлен!", knowledgeTest);
        }

        public async Task<CustomGeneralResponses> GetAllTest()
        {
            var listTests = await context.KnowledgeTests.ToListAsync();
            if (listTests.Count == 0) return new CustomGeneralResponses(false, "Тесты не найдены.");
            return new CustomGeneralResponses(true, "Найдены тесты.", listTests);
        }

        public async Task<CustomGeneralResponses> GetTestById(Guid id)
        {
            if (id == Guid.Empty) return new CustomGeneralResponses(false, "Передаваемый id теста пустой.");

            var knowledgeTest = await context.KnowledgeTests.FirstOrDefaultAsync(k => k.Id == id);
            if (knowledgeTest is null) return new CustomGeneralResponses(false, "Теста с указанным id не найдено.");
            var questions = await context.TestQuestions.Where(q => q.KnowledgeTestId == knowledgeTest.Id).ToListAsync();
            knowledgeTest.TestQuestions = questions;
            return new CustomGeneralResponses(true, "Тест найден!", knowledgeTest);
        }
    }
}
