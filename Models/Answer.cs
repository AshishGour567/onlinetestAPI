namespace OnlineTest.Models
{
    public class Answer
    {
        public long Id { get; set; }
        public long questionId { get; set; }
        public long optionId { get; set; }
        public string? answer { get; set; }
        public DateTime createdDate { get; set; }
        public DateTime updatedDate { get; set; }
        public long createdBy { get; set; }
        public long updatedBy { get; set; }
        public Question question {get;set;} = null!;
        public Option option { get; set; } = null!;
        public Answer()
        {
            createdDate = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
            updatedDate = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
        }
    }
}