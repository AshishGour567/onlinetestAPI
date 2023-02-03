namespace OnlineTest.Dto.AnswerDto
{
    public class AddCodingAnswer
    {
        public long questionId { get; set; }
        public string? answer { get; set; }
        public long createdBy { get; set; }
        public long updatedBy { get; set; }
    }
}