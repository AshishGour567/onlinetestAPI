#nullable disable
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using OnlineTest.Dto.QuestionTypeDto;
using OnlineTest.Models;
using OnlineTest.Services.QuestionTypeServices;

namespace OnlineTest.Controllers
{
    [Route("ExamService/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class QuesTypesController : ControllerBase
    {
        private readonly IQuestionTypeService quesTypesService;
        public QuesTypesController(IQuestionTypeService quesTypesService) => this.quesTypesService = quesTypesService;
        [HttpGet]
        public async Task<ActionResult<ServiceResponse<IEnumerable<GetQuestionTypeDto>>>> GetQuestionTypes()
        {
            var response = await quesTypesService.GetQuestionTypes();
            return response.Success ? Ok(response) : NotFound(response);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetQuestionTypeDto>>> GetQuestionType(long id)
        {
            var response = await quesTypesService.GetQuestionTypeById(id);
            return response.Success ? Ok(response) : NotFound(response);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<ServiceResponse<GetQuestionTypeDto>>> PutQuestion(long id, UpdateQuestionTypeDto updateQuestionTypeDto)
        {
            var response = await quesTypesService.PutQuestionType(id, updateQuestionTypeDto);
            return response.Success ? Ok(response) : NotFound(response);
        }
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<GetQuestionTypeDto>>> PostQuestion(AddQuestionTypeDto addQuestionTypeDto)
        {
            var response = await quesTypesService.PostQuestionType(addQuestionTypeDto);
            return response.Success ? Ok(response) : NotFound(response);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<IEnumerable<GetQuestionTypeDto>>>> DeleteQuesType(long id)
        {
            var response = await quesTypesService.DeleteQuestionType(id);
            return response.Success ? Ok(response) : NotFound(response);
        }
    }
}
