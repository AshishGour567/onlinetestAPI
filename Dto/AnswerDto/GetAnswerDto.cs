namespace OnlineTest.Dto.AnswerDto
{
    public class GetAnswerDto
    {
        public long Id { get; set; }
        public long questionId { get; set; }
        public long optionId { get; set; }
        public string? answer { get; set; }
        public DateTime createdDate { get; set; }
        public DateTime updatedDate { get; set; }
        public long createdBy { get; set; }
        public long updatedBy { get; set; }
    }
}