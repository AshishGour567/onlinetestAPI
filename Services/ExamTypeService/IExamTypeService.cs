using OnlineTest.Dto.ExamTypeDto;
using OnlineTest.Models;

namespace OnlineTest.Services.ExamTypeServices
{
    public interface IExamTypeService
    {
        Task<ServiceResponse<List<GetExamTypeDto>>> GetAllExamTypes();
        Task<ServiceResponse<GetExamTypeDto>> GetExamTypeById(long id); 
        Task<ServiceResponse<List<GetExamTypeDto>>> AddExamType(AddExamTypeDto addExamType);
        Task<ServiceResponse<GetExamTypeDto>> UpdateExamType(long id, UpdateExamTypeDto updateExamType);
        Task<ServiceResponse<List<GetExamTypeDto>>> DeleteExamType(long id);
    }
}