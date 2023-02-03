using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineTest.Data;
using OnlineTest.Dto.AnswerDto;
using OnlineTest.Dto.OptionDto;
using OnlineTest.Models;
using OnlineTest.Services.AnswerServices;

namespace OnlineTest.Services.OptionsServices
{
    public class OptionsService : IOptionsService
    {
        private readonly OnlineTestDbContext context;
        private readonly IMapper mapper;
        private readonly ILogger<OptionsService> logger;
        private readonly ILogger<AnswerService> ansLogger;
        private readonly IAnswerService answerService;
        public OptionsService(IMapper mapper, OnlineTestDbContext context, ILogger<OptionsService> logger, ILogger<AnswerService> ansLogger, IAnswerService answerService)
        {
            this.answerService = answerService;
            this.logger = logger;
            this.ansLogger = ansLogger;
            this.mapper = mapper;
            this.context = context;
        }
        public async Task<ServiceResponse<List<GetOptionDto>>> AddOption(AddOptionDto optionDto)
        {
            var serviceResponse = new ServiceResponse<List<GetOptionDto>>();
            try
            {
                var questionDb = context.Questions.Where(q => q.Id == optionDto.questionId);
                if (questionDb != null)
                {
                    Option option = mapper.Map<Option>(optionDto);
                    context.Options.Add(option);
                    await context.SaveChangesAsync();
                    if(option.isAnswer)
                        await answerService.PostAnswer(mapper.Map<AddAnswerDto>(option));
                    serviceResponse.Data = await context.Options
                        .Include(c => c.question)
                        .Select(e => mapper.Map<GetOptionDto>(e)).ToListAsync();
                }
                else
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Database connection error!";
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
        public async Task<ServiceResponse<List<GetOptionDto>>> DeleteOption(long id)
        {
            var serviceResponse = new ServiceResponse<List<GetOptionDto>>();
            try
            {
                var option = await context.Options.Include(c => c.question).FirstOrDefaultAsync(c => c.Id == id);
                if (option != null)
                {
                    context.Options.Remove(option);
                    await context.SaveChangesAsync();
                    serviceResponse.Data = await context.Options
                        .Include(c => c.question)
                        .Select(c => mapper.Map<GetOptionDto>(c)).ToListAsync();
                    serviceResponse.Success = true;
                    serviceResponse.Message = $"Option deleted with id : {id}";
                }
                else
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = $"No Option found with id : {id}";
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
        public async Task<ServiceResponse<List<GetOptionDto>>> GetAllOptions()
        {
            var serviceResponse = new ServiceResponse<List<GetOptionDto>>();
            try
            {
                var dbOptions = await context.Options.Include(c => c.question).OrderBy(c => c.Id).ToListAsync();
                if (dbOptions.Count() == 0)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "No option found!";
                    throw (new Exception());
                }
                else
                {
                    serviceResponse.Data = dbOptions.Select(c => mapper.Map<GetOptionDto>(c)).ToList();
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
        public async Task<ServiceResponse<GetOptionDto>> GetOptionsById(long id)
        {
            var serviceResponse = new ServiceResponse<GetOptionDto>();
            try
            {
                var dbOption = await context.Options.Include(c => c.question).FirstOrDefaultAsync(c => c.Id == id);
                if (dbOption == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = $"No Option found with id : {id}";
                    throw (new Exception());
                }
                else
                {
                    serviceResponse.Data = mapper.Map<GetOptionDto>(dbOption);
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
        public async Task<ServiceResponse<List<GetOptionDto>>> GetOptionsByQuestionId(long questionId)
        {
            var serviceResponse = new ServiceResponse<List<GetOptionDto>>();
            try
            {
                var dbOptions = await context.Options.Include(c => c.question).Where(c => c.questionId == questionId).ToListAsync();
                if (dbOptions.Count() == 0)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = $"No Option found with id : {questionId}";
                    throw (new Exception());
                }
                else
                {
                    serviceResponse.Data = dbOptions!.Select(opt => mapper.Map<GetOptionDto>(opt)).ToList();
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
        public async Task<ServiceResponse<GetOptionDto>> UpdateOption(long id, UpdateOptionDto updaOptionDto)
        {
            var serviceResponse = new ServiceResponse<GetOptionDto>();
            try
            {
                Option dbOption = context.Options.Include(c => c.question).FirstOrDefault(c => c.Id == id)!;
                if (dbOption.Id == updaOptionDto.Id)
                {
                    dbOption.option = updaOptionDto.option;
                    dbOption.isAnswer = updaOptionDto.isAnswer;
                    dbOption.questionId = updaOptionDto.questionId;
                    dbOption.updatedDate = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
                    context.Options.Include(c => c.question);
                    await context.SaveChangesAsync();
                    serviceResponse.Data = mapper.Map<GetOptionDto>(dbOption);
                    serviceResponse.Message = "Option updated successfully!";
                    await updateAnswer(dbOption);
                }
                else
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = $"No Option found with id : {id}";
                    throw (new Exception());
                }
            }
            catch (AutoMapperMappingException ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Error in mapping.";
                logger.LogError(serviceResponse.Message + ex.StackTrace);
            }
            catch (DbUpdateException ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Error in updating the option";
                logger.LogError(serviceResponse.Message + ex.StackTrace);
            }
            catch (System.Data.Common.DbException ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Database connection error!";
                logger.LogError(serviceResponse.Message + ex.StackTrace);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message += ex.StackTrace;
                logger.LogError(serviceResponse.Message);
            }
            return serviceResponse;
        }
        private async Task updateAnswer(Option option)
        {
            var answerService = new AnswerService(context, mapper, ansLogger);
            try
            {
                var ans = context.Answers.FirstOrDefault(c => c.optionId == option.Id && c.questionId == option.questionId);
                if (ans != null)
                {
                    if (option.isAnswer)
                    {
                        ans.answer = option.option;
                        ans.updatedBy = option.updatedBy;
                        ans.updatedDate = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
                    }
                    else
                    {
                        context.Answers.Remove(ans);
                    }
                    await context.SaveChangesAsync();
                }
                else if (option.isAnswer)
                {
                    await answerService.PostAnswer(mapper.Map<AddAnswerDto>(option));
                }
            }
            catch (DbUpdateException ex)
            {
                logger.LogError("Error in updating the Answer" + ex.StackTrace);
            }
            catch (AutoMapperMappingException ex)
            {
                logger.LogError(ex.Message + ex.StackTrace);
            }
            catch (System.Data.Common.DbException ex)
            {
                logger.LogError("Database connection error!" + ex.StackTrace);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
            }
        }
    }
}