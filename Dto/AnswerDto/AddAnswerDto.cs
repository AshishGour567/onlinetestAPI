namespace OnlineTest.Dto.AnswerDto
{
    public class AddAnswerDto
    {
        public long questionId { get; set; }
        public long optionId { get; set; }
        public string? answer { get; set; }
        public long createdBy { get; set; }
        public long updatedBy { get; set; }
    }
}