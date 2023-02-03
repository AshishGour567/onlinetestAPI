namespace OnlineTest.Models
{
    public class QuestionType
    {
        public long Id { get; set; }
        public string? type { get; set; }
        public DateTime createdDate { get; set; }
        public long createdBy { get; set; }
        public DateTime updatedDate { get; set; }
        public long updatedBy { get; set; }
        public QuestionType()
        {
            createdDate = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
            updatedDate = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
        }
    }
}