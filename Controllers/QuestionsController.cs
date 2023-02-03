#nullable disable
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using OnlineTest.Dto.QuestionDto;
using OnlineTest.Models;
using OnlineTest.Services.QuestionServices;

namespace OnlineTest.Controllers
{
    [Route("ExamService/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class QuestionsController : ControllerBase
    {
        private readonly IQuestionService questionService;
        public QuestionsController(IQuestionService questionService) => this.questionService = questionService;
        [HttpGet]
        public async Task<ActionResult<ServiceResponse<IEnumerable<GetQuestionDto>>>> GetQuestions()
        {
            var response = await questionService.GetQuestions();
            return response.Success ? Ok(response) : NotFound(response);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetQuestionDto>>> GetQuestion(long id)
        {
            var response = await questionService.GetQuestionById(id);
            return response.Success ? Ok(response) : NotFound(response);
        }
        [HttpGet("exam/{examId}")]
        public async Task<ActionResult<ServiceResponse<IEnumerable<GetQuestionDto>>>> GetQuestionsByExamId(long examId)
        {
            var response = await questionService.GetQuestionsByExamId(examId);
            return response.Success ? Ok(response) : NotFound(response);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<ServiceResponse<GetQuestionDto>>> PutQuestion(long id, UpdateQuestionDto ques)
        {
            var response = await questionService.PutQuestion(id, ques);
            return response.Success ? Ok(response) : NotFound(response);
        }
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<GetQuestionDto>>> PostQuestion(AddQuestionDto ques)
        {
            var response = await questionService.PostQuestion(ques);
            return response.Success ? Ok(response) : NotFound(response);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<IEnumerable<GetQuestionDto>>>> DeleteQuestion(long id)
        {
            var response = await questionService.DeleteQuestion(id);
            return response.Success? Ok(response): NotFound(response);
        }
    }
}
