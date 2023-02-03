namespace OnlineTest.Dto.OptionDto
{
    public class UpdateOptionDto
    {
        public long Id { get; set; }
        public string? option { get; set; }
        public bool isAnswer { get; set; }
        public long questionId { get; set; }
        public long updatedBy { get; set; }
    }
}