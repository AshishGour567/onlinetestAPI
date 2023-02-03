using OnlineTest.Dto.OptionDto;

namespace OnlineTest.Dto.QuestionDto
{
    public class UpdateQuestionDto
    {
        public long Id { get; set; }
        public long questionTypeId  { get; set; }
        public long examId { get; set; }
        public string? question { get; set; }
        public long updatedBy { get; set; }
        public List<AddOptionDto> options { get; set; } = null!;
    }
}