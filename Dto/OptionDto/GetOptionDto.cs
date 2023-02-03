namespace OnlineTest.Dto.OptionDto
{
    public class GetOptionDto
    {
        public long Id { get; set; }
        public string? option { get; set; }
        public long questionId { get; set; }
        public DateTime createdDate { get; set; }
        public long createdBy { get; set; }
        public DateTime updatedDate { get; set; }
        public long updatedBy { get; set; }
    }
}