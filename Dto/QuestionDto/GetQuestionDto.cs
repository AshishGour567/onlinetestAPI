using OnlineTest.Dto.OptionDto;
using OnlineTest.Dto.QuestionTypeDto;

namespace OnlineTest.Dto.QuestionDto
{
    public class GetQuestionDto
    {
        public long Id { get; set; }
        public long questionTypeId  { get; set; }
        public long examId { get; set; }
        public string? question { get; set; }
        public DateTime createdDate { get; set; }
        public DateTime updatedDate { get; set; }
        public long createdBy { get; set; }
        public long updatedBy { get; set; }
        public List<GetOptionDto> options { get; set; } = null!;
        public GetQuestionTypeDto questionType { get; set; } = null!;
    }
}