namespace OnlineTest.Models
{
    public class Option
    {
        public long Id { get; set; }
        public string? option { get; set; }
        public bool isAnswer { get; set; }
        public long questionId { get; set; }
        public DateTime createdDate { get; set; }
        public long createdBy { get; set; }
        public DateTime updatedDate { get; set; }
        public long updatedBy { get; set; }
        public Question question {get;set;} = null!;
        public Option()
        {
            createdDate = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
            updatedDate = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
        }
    }
}