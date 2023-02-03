namespace OnlineTest.Dto.OptionDto
{
    public class AddOptionDto
    {
        public string? option { get; set; }
        public bool isAnswer { get; set; }
        public long questionId { get; set; }
        public long createdBy { get; set; }
        public long updatedBy { get; set; }
    }
}