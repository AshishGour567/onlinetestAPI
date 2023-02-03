namespace OnlineTest.Dto.AnswerDto
{
    public class UpdateAnswerDto
    {
        public long Id { get; set; }
        public long questionId { get; set; }
        public long optionId { get; set; }
        public string? answer { get; set; }
        public long updatedBy { get; set; }
    }
}