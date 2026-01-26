using Portal.Domain.DTOs;
using Portal.Domain.Interfaces;
using Portal.Domain.Responses;

namespace Portal.Application.Services
{
    public class KnowledgeTestService : IKnowledgeTestService
    {
        private readonly IKnowledgeTestDomain knowledgeTestDomain;

        public KnowledgeTestService(IKnowledgeTestDomain knowledgeTestDomain)
        {
            this.knowledgeTestDomain = knowledgeTestDomain;
        }

        public async Task<CustomGeneralResponses> AddQuestionAnswerAsync(List<QuestionAnswerDTO> request)
        {
            return await knowledgeTestDomain.AddQuestionAnswerAsync(request);
        }

        public async Task<CustomGeneralResponses> AddQuestionAsync(List<TestQuestionDTO> request)
        {
            return await knowledgeTestDomain.AddQuestionAsync(request);
        }

        public async Task<CustomGeneralResponses> AddTestAsync(KnowledgeTestDTO request)
        {
            return await knowledgeTestDomain.AddTestAsync(request);
        }

        public async Task<CustomGeneralResponses> GetAllTestAsync()
        {
            return await knowledgeTestDomain.GetAllTestAsync();
        }

        public async Task<CustomGeneralResponses> GetQuestionsByTestIdAsync(Guid id)
        {
            return await knowledgeTestDomain.GetQuestionsByTestIdAsync(id);
        }

        public async Task<CustomGeneralResponses> GetTestByIdAsync(Guid id)
        {
            return await knowledgeTestDomain.GetTestByIdAsync(id);
        }
    }
}
