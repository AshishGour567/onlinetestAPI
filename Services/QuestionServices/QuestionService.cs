using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineTest.Data;
using OnlineTest.Dto.AnswerDto;
using OnlineTest.Dto.QuestionDto;
using OnlineTest.Models;
using OnlineTest.Services.AnswerServices;
using OnlineTest.Services.OptionsServices;

namespace OnlineTest.Services.QuestionServices
{
    public class QuestionService : IQuestionService
    {
        private readonly IMapper mapper;
        private readonly OnlineTestDbContext context;
        private readonly ILogger<QuestionService> logger;
        private readonly ILogger<AnswerService> ansLogger;
        private readonly IOptionsService optionsService;

        public IAnswerService answerService { get; }

        public QuestionService(IMapper mapper, OnlineTestDbContext context, ILogger<QuestionService> logger, ILogger<AnswerService> ansLogger, IOptionsService optionsService, IAnswerService answerService)
        {
            this.optionsService = optionsService;
            this.answerService = answerService;
            this.ansLogger = ansLogger;
            this.logger = logger;
            this.mapper = mapper;
            this.context = context;
        }
        public async Task<ServiceResponse<List<GetQuestionDto>>> DeleteQuestion(long id)
        {
            var serviceResponse = new ServiceResponse<List<GetQuestionDto>>();
            try
            {
                var question = await context.Questions.FirstOrDefaultAsync(q => q.Id == id);
                if (question != null)
                {
                    context.Questions.Remove(question);
                    await context.SaveChangesAsync();
                    serviceResponse.Data = await context.Questions
                        .Include(c => c.questionType)
                        .Where(c => c.questionType!.Id == c.questionTypeId)
                        .Select(c => mapper.Map<GetQuestionDto>(c)).ToListAsync();
                    serviceResponse.Success = true;
                    serviceResponse.Message = $"Question deleted with id : {id}";
                }
                else
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = $"No Question found with id : {id}";
                    throw (new Exception());
                }
            }
            catch (System.Data.Common.DbException ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Database connection error!";
                logger.LogError(serviceResponse.Message + ex.StackTrace);
            }
            catch (AutoMapperMappingException ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Error in mapping.";
                logger.LogError(serviceResponse.Message + ex.StackTrace);
            }
            catch (Exception ex)
            {
                logger.LogError(serviceResponse.Message + ex.StackTrace);
            }
            return serviceResponse;
        }
        public async Task<ServiceResponse<GetQuestionDto>> GetQuestionById(long id)
        {
            var serviceResponse = new ServiceResponse<GetQuestionDto>();
            try
            {
                var dbQues = await context.Questions.Include(c => c.options).Include(c => c.questionType).FirstOrDefaultAsync(c => c.Id == id);
                if (dbQues == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = $"No Question found with id : {id}";
                    throw (new Exception());
                }
                else
                {
                    serviceResponse.Data = mapper.Map<GetQuestionDto>(dbQues);
                }
            }
            catch (System.Data.Common.DbException ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Database connection error!";
                logger.LogError(serviceResponse.Message + ex.StackTrace);
            }
            catch (AutoMapperMappingException ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Error in mapping.";
                logger.LogError(serviceResponse.Message + ex.StackTrace);
            }
            catch (Exception ex)
            {
                logger.LogError(serviceResponse.Message + ex.StackTrace);
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetQuestionDto>>> GetQuestionsByExamId(long examId)
        {
            var serviceResponse = new ServiceResponse<List<GetQuestionDto>>();
            try
            {
                var dbQues = await context.Questions.Include(c => c.options).Include(c => c.questionType).Where(c => c.examId == examId).ToListAsync();
                if (dbQues.Count() == 0)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = $"No Question in exam : {examId}";
                    throw (new Exception());
                }
                else
                {
                    serviceResponse.Data = dbQues.Select(c => mapper.Map<GetQuestionDto>(c)).ToList();
                }
            }
            catch (System.Data.Common.DbException ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Database connection error!";
                logger.LogError(serviceResponse.Message + ex.StackTrace);
            }
            catch (AutoMapperMappingException ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Error in mapping.";
                logger.LogError(serviceResponse.Message + ex.StackTrace);
            }
            catch (Exception ex)
            {
                logger.LogError(serviceResponse.Message + ex.StackTrace);
            }
            return serviceResponse;
        }
        public async Task<ServiceResponse<List<GetQuestionDto>>> GetQuestions()
        {
            var serviceResponse = new ServiceResponse<List<GetQuestionDto>>();
            try
            {
                var dbQues = await context.Questions
                    .Include(c => c.options)
                    .Include(c => c.questionType)
                    .OrderBy(c => c.Id)
                    .ToListAsync();
                if (dbQues.Count() == 0)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = $"No Question found";
                    throw (new Exception());
                }
                else
                {
                    serviceResponse.Data = dbQues.Select(c => mapper.Map<GetQuestionDto>(c)).ToList();
                }
            }
            catch (System.Data.Common.DbException ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Database connection error!";
                logger.LogError(serviceResponse.Message + ex.StackTrace);
            }
            catch (AutoMapperMappingException ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Error in mapping.";
                logger.LogError(serviceResponse.Message + ex.StackTrace);
            }
            catch (Exception ex)
            {
                logger.LogError(serviceResponse.Message + ex.StackTrace);
            }
            return serviceResponse;
        }
        public async Task<ServiceResponse<List<GetQuestionDto>>> PostQuestion(AddQuestionDto quesDto)
        {
            var serviceResponse = new ServiceResponse<List<GetQuestionDto>>();
            try
            {
                if (!isQuestionTypeExits(quesDto.questionTypeId))
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = $"No Question Type Found with id : {quesDto.questionTypeId}";
                    throw (new Exception());
                }
                else if (!isExamExits(quesDto.examId))
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = $"No Exam Found with id : {quesDto.examId}";
                    throw (new Exception());
                }
                else if (quesDto.questionTypeId == 1)
                {
                    var question = mapper.Map<Question>(quesDto);
                    if (question.options.Count() > 0)
                    {
                        serviceResponse.Success = false;
                        serviceResponse.Message = "its a coding question, dont give options";
                        throw (new Exception());
                    }
                    else
                    {
                        var x = context.Add(question);
                        await context.SaveChangesAsync();
                        serviceResponse.Data = await context.Questions
                            .Include(c => c.questionType)
                            .Select(c => mapper.Map<GetQuestionDto>(c)).ToListAsync();
                        serviceResponse.Message = $"Question added with id : {x.Entity.Id}";
                    }
                }
                else if (quesDto.questionTypeId == 2)
                {
                    var question = mapper.Map<Question>(quesDto);
                    if (question.options.Count() == 0)
                    {
                        serviceResponse.Success = false;
                        serviceResponse.Message = "You have to give atleast one option";
                        throw (new Exception());
                    }
                    else
                    {
                        var x = context.Questions.Add(question);
                        await context.SaveChangesAsync();
                        // add correctOptions in answer
                        var correctOptionList = context.Questions.FirstOrDefault(c => c.Id == x.Entity.Id)!.options.Where(c => c.isAnswer).ToList();
                        var answerService = new AnswerService(context, mapper, ansLogger);
                        foreach (var option in correctOptionList)
                            await answerService.PostAnswer(mapper.Map<AddAnswerDto>(option));
                        serviceResponse.Data = await context.Questions
                            .Include(c => c.options)
                            .Include(c => c.questionType)
                            .Select(c => mapper.Map<GetQuestionDto>(c)).ToListAsync();
                        serviceResponse.Message = $"Question added with id : {x.Entity.Id}";
                    }
                }
            }
            catch (System.Data.Common.DbException ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Database connection error!";
                logger.LogError(serviceResponse.Message + ex.StackTrace);
            }
            catch (AutoMapperMappingException ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Error in mapping.";
                logger.LogError(serviceResponse.Message + ex.StackTrace);
            }
            catch (Exception ex)
            {
                logger.LogError(serviceResponse.Message + ex.StackTrace);
            }
            return serviceResponse;
        }
        public async Task<ServiceResponse<GetQuestionDto>> PutQuestion(long id, UpdateQuestionDto updateQuestionDto)
        {
            var serviceResponse = new ServiceResponse<GetQuestionDto>();
            try
            {
                var dbQues = context.Questions
                .Include(c => c.options)
                .FirstOrDefault(c => c.Id == id)!;
                if (dbQues.Id == updateQuestionDto.Id)
                {
                    dbQues.questionTypeId = updateQuestionDto.questionTypeId;
                    dbQues.examId = updateQuestionDto.examId;
                    dbQues.question = updateQuestionDto.question;
                    dbQues.updatedBy = updateQuestionDto.updatedBy;
                    dbQues.updatedDate = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
        
                    for(int i=0; i<dbQues.options.Count(); i++)
                    {
                        await optionsService.DeleteOption(dbQues.options[i].Id);
                    }

                    for(int i=0; i<updateQuestionDto.options.Count(); i++)
                    {
                        updateQuestionDto.options[i].questionId=updateQuestionDto.Id;
                        await optionsService.AddOption(updateQuestionDto.options[i]);
                    }
                    serviceResponse.Data = mapper.Map<GetQuestionDto>(dbQues);
                    serviceResponse.Message = "Question Updated";
                }
                else
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = $"No Question Found with id : {updateQuestionDto.Id}";
                    throw (new Exception());
                }
            }
            catch (System.Data.Common.DbException ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Database connection error!";
                logger.LogError(serviceResponse.Message + ex.StackTrace);
            }
            catch (AutoMapperMappingException ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Error in mapping.";
                logger.LogError(serviceResponse.Message + ex.StackTrace);
            }
            catch (Exception ex)
            {
                logger.LogError(serviceResponse.Message + ex.StackTrace);
            }
            return serviceResponse;
        }
        private bool isQuestionTypeExits(long id)
        {
            bool value = false;
            try
            {
                value = context.QuestionTypes.ToList().Any(c => c.Id == id);
            }
            catch (System.Data.Common.DbException ex)
            {
                logger.LogError("Database connection error!" + ex.StackTrace);
            }
            return value;
        }
        private bool isExamExits(long id)
        {
            bool value = false;
            try
            {
                value = context.Exams.ToList().Any(c => c.Id == id);
            }
            catch (System.Data.Common.DbException ex)
            {
                logger.LogError("Database connection error!" + ex.StackTrace);
            }
            return value;
        }
    }
}