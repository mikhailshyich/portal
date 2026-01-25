using Portal.Domain.DTOs;
using Portal.Domain.Responses;

namespace Portal.Application.Services
{
    public interface IKnowledgeTestService
    {
        Task<CustomGeneralResponses> AddTestAsync(KnowledgeTestDTO request);
        Task<CustomGeneralResponses> AddQuestionAsync(List<TestQuestionDTO> request);
        Task<CustomGeneralResponses> AddQuestionAnswerAsync(List<QuestionAnswerDTO> request);
        Task<CustomGeneralResponses> GetAllTest();
        Task<CustomGeneralResponses> GetTestById(Guid id);


    }
}
