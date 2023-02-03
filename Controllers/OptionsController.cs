#nullable disable
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using OnlineTest.Dto.OptionDto;
using OnlineTest.Models;
using OnlineTest.Services.OptionsServices;

namespace OnlineTest.Controllers
{
    [Route("ExamService/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class OptionsController : ControllerBase
    {
        private readonly IOptionsService optionsService;
        public OptionsController(IOptionsService optionsService) => this.optionsService = optionsService;
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetOptionDto>>> GetOptions()
        {
            var response = await optionsService.GetAllOptions();
            return response.Success ? Ok(response) : NotFound(response);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetOptionDto>>> GetOption(long id)
        {
            var response = await optionsService.GetOptionsById(id);
            return response.Success ? Ok(response) : NotFound(response);
        }
        [HttpGet("questionId/{questionId}")]
        public async Task<ActionResult<ServiceResponse<IEnumerable<GetOptionDto>>>> GetOptionsByQuestionId(long questionId)
        {
            var response= await optionsService.GetOptionsByQuestionId(questionId);
            return response.Success ? Ok(response) : NotFound(response);
        }
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<GetOptionDto>>> PostOption(AddOptionDto options)
        {
            var response = await optionsService.AddOption(options);
            return response.Success ? Ok(response) : NotFound(response);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<GetOptionDto>>> DeleteOption(long id)
        {
            var response =  await optionsService.DeleteOption(id);
            return response.Success ? Ok(response) : NotFound(response);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<ServiceResponse<GetOptionDto>>> PutOption(long id, UpdateOptionDto updateOptionDto)
        {
            var response = await optionsService.UpdateOption(id, updateOptionDto);
            return response.Success ? Ok(response) : NotFound(response);
        }
    }
}
