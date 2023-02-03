namespace OnlineTest.Dto.QuestionDto
{
    public class AddCodingQuestionDto
    {
        public long questionTypeId  { get; set; }
        public long examId { get; set; }
        public string? question { get; set; }
        public long createdBy { get; set; }
        public long updatedBy { get; set; }
    }
}