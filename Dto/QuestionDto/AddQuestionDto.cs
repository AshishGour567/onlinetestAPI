using System.ComponentModel.DataAnnotations.Schema;
using OnlineTest.Dto.OptionDto;
using OnlineTest.Models;

namespace OnlineTest.Dto.QuestionDto
{
    public class AddQuestionDto
    {
        public long questionTypeId  { get; set; }
        public long examId { get; set; }
        public string? question { get; set; }
        public long createdBy { get; set; }
        public long updatedBy { get; set; }
        public List<AddOptionDto> options { get; set; } = new List<AddOptionDto>();
    }
}