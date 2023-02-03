using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using OnlineTest.Dto.AnswerDto;
using OnlineTest.Models;
using OnlineTest.Services.AnswerServices;

namespace OnlineTest.Controllers
{
    [Route("ExamService/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class AnswersController : ControllerBase
    {
        private readonly IAnswerService answerService;
        public AnswersController(IAnswerService answerService) => this.answerService = answerService;
        [HttpGet]
        public async Task<ActionResult<ServiceResponse<IEnumerable<GetAnswerDto>>>> GetAnswers()
        {
            var response = await answerService.GetAnswers();
            return response.Success ? Ok(response) : NotFound(response);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetAnswerDto>>> GetAnswer(long id)
        {
            var response = await answerService.GetAnswer(id);
            return response.Success ? Ok(response) : NotFound(response);
        }
        [HttpGet("option/{optionId}")]
        public async Task<ActionResult<ServiceResponse<GetAnswerDto>>> GetAnswerByOptionId(long optionId)
        {
            var response = await answerService.GetAnswerByOptionId(optionId);
            return response.Success ? Ok(response) : NotFound(response);
        }
        [HttpGet("question/{questionId}")]
        public async Task<ActionResult<ServiceResponse<GetAnswerDto>>> GetAnswerByQuestionId(long questionId)
        {
            var response = await answerService.GetAnswerByQuestionId(questionId);
            return response.Success ? Ok(response) : NotFound(response);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<ServiceResponse<GetAnswerDto>>> PutAnswer(long id, UpdateAnswerDto answer)
        {
            var response = await answerService.PutAnswer(id, answer);
            return response.Success ? Ok(response) : NotFound(response);
        }
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<IEnumerable<GetAnswerDto>>>> PostAnswer(AddAnswerDto answer)
        {
            var response = await answerService.PostAnswer(answer);
            return response.Success ? Ok(response) : NotFound(response);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<IEnumerable<GetAnswerDto>>>> DeleteAnswer(long id)
        {
            var response = await answerService.DeleteAnswer(id);
            return response.Success ? Ok(response) : NotFound(response);
        }
    }
}
