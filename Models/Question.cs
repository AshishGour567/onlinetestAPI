using OnlineTest.Dto.OptionDto;

namespace OnlineTest.Models
{
    public class Question
    {
        public long Id { get; set; }
        public long questionTypeId  { get; set; }
        public long examId { get; set; }
        public string? question { get; set; }
        public DateTime createdDate { get; set; }
        public DateTime updatedDate { get; set; }
        public long createdBy { get; set; }
        public long updatedBy { get; set; }
        public List<Option> options { get; set; } = null!;
        public QuestionType questionType {get;set;} = null!;
        public Exam exam { get; set; } = null!;
        public Question()
        {
            createdDate = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
            updatedDate = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
        }
    }
}