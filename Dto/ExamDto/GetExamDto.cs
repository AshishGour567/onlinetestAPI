using OnlineTest.Dto.ExamTypeDto;

namespace OnlineTest.Dto.ExamDto
{
    public class GetExamDto
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
        public GetExamTypeDto? examType{get;set;}
    }
}