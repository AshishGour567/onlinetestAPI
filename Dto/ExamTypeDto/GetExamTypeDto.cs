namespace OnlineTest.Dto.ExamTypeDto
{
    public class GetExamTypeDto
    {
        public long Id { get; set; }
        public string? type { get; set; }
        public DateTime createdDate { get; set; }
        public DateTime updatedDate { get; set; }
        public long createdBy { get; set; }
        public long updatedBy { get; set; }
    }
}