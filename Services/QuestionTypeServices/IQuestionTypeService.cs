using OnlineTest.Dto.QuestionTypeDto;
using OnlineTest.Models;

namespace OnlineTest.Services.QuestionTypeServices
{
    public interface IQuestionTypeService
    {
        Task<ServiceResponse<List<GetQuestionTypeDto>>> GetQuestionTypes();
        Task<ServiceResponse<GetQuestionTypeDto>> GetQuestionTypeById(long id);
        Task<ServiceResponse<List<GetQuestionTypeDto>>> PostQuestionType(AddQuestionTypeDto addQuestionTypeDto);
        Task<ServiceResponse<GetQuestionTypeDto>> PutQuestionType(long id, UpdateQuestionTypeDto updateQuestionTypeDto);
        Task<ServiceResponse<List<GetQuestionTypeDto>>> DeleteQuestionType(long id);
    }
}