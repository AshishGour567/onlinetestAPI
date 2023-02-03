#nullable disable
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using OnlineTest.Dto.ExamTypeDto;
using OnlineTest.Models;
using OnlineTest.Services.ExamTypeServices;
namespace OnlineTest.Controllers
{
    [Route("ExamService/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class ExamTypesController : ControllerBase
    {
        private readonly IExamTypeService examTypeService;
        public ExamTypesController(IExamTypeService examTService) => examTypeService = examTService;
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetExamTypeDto>>> GetExamTypes()
        {
            var response = await examTypeService.GetAllExamTypes();
            return response.Success ? Ok(response) : NotFound(response);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetExamTypeDto>>> GetExamType(long id)
        {
            var response = await examTypeService.GetExamTypeById(id);
            return response.Success ? Ok(response) : NotFound(response);
        }
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<IEnumerable<GetExamTypeDto>>>> PostExamType(AddExamTypeDto examTypeDto)
        {
            var response = await examTypeService.AddExamType(examTypeDto);
            return response.Success ? Ok(response) : NotFound(response);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<ServiceResponse<GetExamTypeDto>>> PutExamType(long id, UpdateExamTypeDto examTypeDto) 
        {
            var response = await examTypeService.UpdateExamType(id, examTypeDto);
            return response.Success ? Ok(response) : NotFound(response);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<List<GetExamTypeDto>>>> DeleteExamType(long id)
        { 
            var response =  await examTypeService.DeleteExamType(id);
            return response.Success ? Ok(response) : NotFound(response);
        }
    }
}