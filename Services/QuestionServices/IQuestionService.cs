using OnlineTest.Dto.QuestionDto;
using OnlineTest.Models;

namespace OnlineTest.Services.QuestionServices
{
    public interface IQuestionService
    {
        Task<ServiceResponse<List<GetQuestionDto>>> GetQuestions();
        Task<ServiceResponse<GetQuestionDto>> GetQuestionById(long id);
        Task<ServiceResponse<List<GetQuestionDto>>> GetQuestionsByExamId(long examId);
        Task<ServiceResponse<List<GetQuestionDto>>> PostQuestion(AddQuestionDto questionDto);
        Task<ServiceResponse<GetQuestionDto>> PutQuestion(long id, UpdateQuestionDto updateQuesDto);
        Task<ServiceResponse<List<GetQuestionDto>>> DeleteQuestion(long id);
    }
}