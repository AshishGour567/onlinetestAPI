using OnlineTest.Dto.AnswerDto;
using OnlineTest.Models;

namespace OnlineTest.Services.AnswerServices
{
    public interface IAnswerService
    {
        Task<ServiceResponse<List<GetAnswerDto>>> GetAnswers();
        Task<ServiceResponse<GetAnswerDto>> GetAnswer(long id);
        Task<ServiceResponse<List<GetAnswerDto>>> PostAnswer(AddAnswerDto addAnswerDto);
        Task<ServiceResponse<GetAnswerDto>> PutAnswer(long id, UpdateAnswerDto updateAnswerDto);
        Task<ServiceResponse<List<GetAnswerDto>>> DeleteAnswer(long id);
        Task<ServiceResponse<List<GetAnswerDto>>> GetAnswerByOptionId(long optionId);
        Task<ServiceResponse<List<GetAnswerDto>>>GetAnswerByQuestionId(long questionId);
    }
}