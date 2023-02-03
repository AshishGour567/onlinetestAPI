namespace OnlineTest.Dto.QuestionTypeDto
{
    public class GetQuestionTypeDto
    {
        public long Id { get; set; }
        public string? type { get; set; }
        public DateTime createdDate { get; set; }
        public long createdBy { get; set; }
        public DateTime updatedDate { get; set; }
        public long updatedBy { get; set; }
    }
}