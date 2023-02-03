using System.Text.Json.Serialization;

namespace OnlineTest.Models
{
    public class Exam
    {
        public long Id { get; set; }
        public long  examTypeId { get; set; }
        public long orgId { get; set; }
        public string? name { get; set; }
        public bool isPublic { get; set; }
        public DateTime startDateTime { get; set; }
        public DateTime endDateTime { get; set; }
        public DateTime createdDate { get; set; }
        public DateTime updatedDate { get; set;}
        public long createdBy { get; set; }
        public long updatedBy { get; set; }
        public ExamType examType{get;set;} = null!;
        public Exam()
        {
            this.createdDate = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
            this.updatedDate = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));        }
    }
}