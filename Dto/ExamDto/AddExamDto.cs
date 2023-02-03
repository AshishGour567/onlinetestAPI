namespace OnlineTest.Dto.ExamDto
{
    public class AddExamDto
    {
        public string? name { get; set; }
        public bool isPublic { get; set; }
        public long  examTypeId { get; set; }
        public long orgId { get; set; }
        public string startDateTime { get; set; } = null!;
        public string endDateTime { get; set; } = null!;
    }
}