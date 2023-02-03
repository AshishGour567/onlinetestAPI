#nullable disable
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using OnlineTest.Dto.ExamDto;
using OnlineTest.Models;
using OnlineTest.Services.ExamsServices;
namespace OnlineTest.Controllers
{
    [Route("ExamService/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class ExamsController : ControllerBase
    {
        private readonly IExamServices examService;
        public ExamsController(IExamServices examService)
        {
            this.examService = examService;
        }
        [HttpGet]
        public async Task<ActionResult<ServiceResponse<IEnumerable<GetExamDto>>>> GetExams()
        {
            var response = await this.examService.GetExams();
            return response.Success ? Ok(response) : NotFound(response);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetExamDto>>> GetExam(long id)
        {
            var response = await examService.GetExam(id);
            return response.Success ? Ok(response) : NotFound(response); 
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<IEnumerable<GetExamDto>>>> DeleteExam(long id)
        { 
            var response =  await examService.DeleteExam(id);
            return response.Success ? Ok(response) : NotFound(response);
        }
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<IEnumerable<GetExamDto>>>> PostExam(AddExamDto exam)
        {
            var response = await examService.AddExam(exam);
            return response.Success ? Ok(response) : NotFound(response);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<ServiceResponse<GetExamDto>>> PutExam(long id, UpdateExamDto exam) 
        {
            var response = await examService.UpdateExam(id, exam);
            return response.Success ? Ok(response) : NotFound(response);
        }
    }
}