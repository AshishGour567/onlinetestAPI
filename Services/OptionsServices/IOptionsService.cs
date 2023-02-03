using OnlineTest.Dto.OptionDto;
using OnlineTest.Models;

namespace OnlineTest.Services.OptionsServices
{
    public interface IOptionsService
    {
        Task<ServiceResponse<List<GetOptionDto>>> GetAllOptions();
        Task<ServiceResponse<GetOptionDto>> GetOptionsById(long id);
        Task<ServiceResponse<List<GetOptionDto>>> GetOptionsByQuestionId(long questionId);
        Task<ServiceResponse<List<GetOptionDto>>> AddOption(AddOptionDto optionDto);
        Task<ServiceResponse<List<GetOptionDto>>> DeleteOption(long id);
        Task<ServiceResponse<GetOptionDto>> UpdateOption(long id, UpdateOptionDto updateOptionDto);
    }
}