using Microsoft.EntityFrameworkCore;
using OnlineTest.Models;
namespace OnlineTest.Data
{
    public class OnlineTestDbContext : DbContext
    {
        public OnlineTestDbContext(DbContextOptions<OnlineTestDbContext> options) : base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var date = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
            modelBuilder.Entity<ExamType>().HasData(
            new ExamType{Id = 1, type = "Developer",createdBy= 0,createdDate=date,updatedDate=date,updatedBy=0},
            new ExamType{Id = 2, type = "Freshers",createdBy= 0,createdDate=date,updatedDate=date,updatedBy=0},
            new ExamType{Id = 3, type = "Demo",createdBy= 0,createdDate=date,updatedDate=date,updatedBy=0}

            );
            modelBuilder.Entity<QuestionType>().HasData(
            new ExamType{Id = 1, type = "Coding",createdBy= 0,createdDate=date,updatedDate=date,updatedBy=0},
            new ExamType{Id = 2, type = "MCQs",createdBy= 0,createdDate=date,updatedDate=date,updatedBy=0}
            );
        }
        public DbSet<QuestionType> QuestionTypes {get;set;} = null!;
        public DbSet<Question> Questions {get;set;} = null!;
        public DbSet<Option> Options {get;set;} = null!;
        public DbSet<ExamType> ExamTypes {get;set;} = null!;
        public DbSet<Exam> Exams {get;set;} = null!;
        public DbSet<Answer> Answers {get;set;} = null!;
    }
}