using AutoMapper;
using OnlineTest.Dto.AnswerDto;
using OnlineTest.Dto.ExamDto;
using OnlineTest.Dto.ExamTypeDto;
using OnlineTest.Dto.OptionDto;
using OnlineTest.Dto.QuestionDto;
using OnlineTest.Dto.QuestionTypeDto;
using OnlineTest.Models;

namespace OnlineTest.Mapper
{
    public class AutoMapperProfile : Profile
    {
       public AutoMapperProfile()
       {
            CreateMap<Answer,GetAnswerDto>().ReverseMap();
            CreateMap<Answer,AddAnswerDto>().ReverseMap();
            CreateMap<Answer,UpdateAnswerDto>().ReverseMap();
            CreateMap<Exam, GetExamDto>().ReverseMap();
            CreateMap<Exam, AddExamDto>().ReverseMap();
            CreateMap<Exam, UpdateExamDto>().ReverseMap();
            CreateMap<ExamType,GetExamTypeDto>().ReverseMap();
            CreateMap<ExamType,AddExamTypeDto>().ReverseMap();
            CreateMap<ExamType,UpdateExamTypeDto>().ReverseMap();
            CreateMap<Option, GetOptionDto>().ReverseMap();
            CreateMap<Option, AddOptionDto>().ReverseMap();
            CreateMap<Option, UpdateOptionDto>().ReverseMap();
            CreateMap<Question,AddQuestionDto>().ReverseMap();
            CreateMap<Question, UpdateQuestionDto>().ReverseMap();
            CreateMap<Question, GetQuestionDto>().ReverseMap();
            CreateMap<QuestionType, UpdateQuestionTypeDto>().ReverseMap();
            CreateMap<QuestionType, AddQuestionTypeDto>().ReverseMap();
            CreateMap<QuestionType, GetQuestionTypeDto>().ReverseMap();
            CreateMap<Option,AddAnswerDto>()
                    .ForMember(dest => dest.optionId, opt => opt.MapFrom(src => src.Id))
                    .ForMember(dest => dest.questionId, opt => opt.MapFrom(src => src.questionId))
                    .ForMember(dest => dest.answer, opt => opt.MapFrom(src => src.option))
                    .ForMember(dest => dest.createdBy, opt => opt.MapFrom(src => src.createdBy))
                    .ForMember(dest => dest.updatedBy, opt => opt.MapFrom(src => src.updatedBy))
                    .ReverseMap();
       }
    }
}