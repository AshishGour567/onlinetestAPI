using OnlineTest.Models;
using OnlineTest.Dto.ExamDto;

namespace OnlineTest.Services.ExamsServices
{
    public interface IExamServices
    {
        Task<ServiceResponse<List<GetExamDto>>> GetExams();
        Task<ServiceResponse<GetExamDto>> GetExam(long id);
        Task<ServiceResponse<List<GetExamDto>>> AddExam(AddExamDto examDto);
        Task<ServiceResponse<List<GetExamDto>>> DeleteExam(long id);
        Task<ServiceResponse<GetExamDto>> UpdateExam(long id, UpdateExamDto updateExamDto);
    }
}
