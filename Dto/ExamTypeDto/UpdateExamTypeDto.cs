namespace OnlineTest.Dto.ExamTypeDto
{
    public class UpdateExamTypeDto
    {
        public long Id { get; set; }
        public string? type { get; set; }
        public long createdBy { get; set; }
        public long updatedBy { get; set; }
    }
}