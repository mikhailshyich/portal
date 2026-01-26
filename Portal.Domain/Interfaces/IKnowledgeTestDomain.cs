using Portal.Domain.DTOs;
using Portal.Domain.Entities.KnowledgeTests;
using Portal.Domain.Responses;

namespace Portal.Domain.Interfaces
{
    public interface IKnowledgeTestDomain
    {
        Task<CustomGeneralResponses> AddTestAsync(KnowledgeTestDTO request);
        Task<CustomGeneralResponses> AddQuestionAsync(List<TestQuestionDTO> request);
        Task<CustomGeneralResponses> AddQuestionAnswerAsync(List<QuestionAnswerDTO> request);
        Task<CustomGeneralResponses> GetAllTestAsync();
        Task<CustomGeneralResponses> GetTestByIdAsync(Guid id);
        Task<CustomGeneralResponses> GetQuestionsByTestIdAsync(Guid id);
    }
}
