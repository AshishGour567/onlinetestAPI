using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineTest.Data;
using OnlineTest.Dto.AnswerDto;
using OnlineTest.Models;

namespace OnlineTest.Services.AnswerServices
{
    public class AnswerService : IAnswerService
    {
        private readonly OnlineTestDbContext context;
        private readonly IMapper mapper;
        private readonly ILogger<AnswerService> logger;

        public AnswerService(OnlineTestDbContext context, IMapper mapper, ILogger<AnswerService> logger)
        {
            this.logger = logger;
            this.context = context;
            this.mapper = mapper;
        }
        public async Task<ServiceResponse<List<GetAnswerDto>>> DeleteAnswer(long id)
        {
            var serviceResponse = new ServiceResponse<List<GetAnswerDto>>();
            try
            {
                var answer = await context.Answers.Include(c => c.option).FirstOrDefaultAsync(c => c.Id == id);
                if (answer != null)
                {
                    context.Answers.Remove(answer);
                    await context.SaveChangesAsync();
                    serviceResponse.Data = context.Answers
                        .Include(c => c.option)
                        .Where(c => c.option.Id == c.optionId)
                        .Select(c => mapper.Map<GetAnswerDto>(c)).ToList();
                    serviceResponse.Message = $"Answer deleted with id : {id}";
                }
                else
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = $"No Answer found with id : {id}";
                    throw(new Exception());
                }
            }
            catch(AutoMapperMappingException ex)
            {
                logger.LogError("Error in mapping" + ex.StackTrace);
            }
            catch(System.Data.Common.DbException ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Database connection error!";
                logger.LogError(serviceResponse.Message + ex.StackTrace);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                logger.LogError(serviceResponse.Message + ex.StackTrace);
            }
            return serviceResponse;
        }
        public async Task<ServiceResponse<GetAnswerDto>> GetAnswer(long id)
        {
            var serviceResponse = new ServiceResponse<GetAnswerDto>();
            try
            {
                var dbAnswer = await context.Answers.Include(c => c.option).FirstOrDefaultAsync(c => c.Id == id);
                if (dbAnswer == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = $"No Answer found with id : {id}";
                    throw(new Exception());
                }
                else
                {
                    serviceResponse.Data = mapper.Map<GetAnswerDto>(dbAnswer);
                }
            }
            catch(AutoMapperMappingException ex)
            {
                logger.LogError("Error in mapping" + ex.StackTrace);
            }
            catch(System.Data.Common.DbException ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Database connection error!";
                logger.LogError(serviceResponse.Message + ex.StackTrace);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                logger.LogError(serviceResponse.Message + ex.StackTrace);
            }
            return serviceResponse;
        }
        public async Task<ServiceResponse<List<GetAnswerDto>>> GetAnswerByOptionId(long optionId)
        {
            var serviceResponse = new ServiceResponse<List<GetAnswerDto>>();
            try
            {
                var dbAnswer = await context.Answers.Include(c => c.option).Where(c => c.option.Id == optionId).ToListAsync();
                if (dbAnswer.Count() == 0)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = $"No Answer found with id : {optionId}";
                    throw(new Exception());
                }
                else
                {
                    serviceResponse.Data = dbAnswer!.Select(c => mapper.Map<GetAnswerDto>(c)).ToList();
                }
            }
            catch(AutoMapperMappingException ex)
            {
                logger.LogError("Error in mapping" + ex.StackTrace);
            }
            catch(System.Data.Common.DbException ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Database connection error!";
                logger.LogError(serviceResponse.Message + ex.StackTrace);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                logger.LogError(serviceResponse.Message + ex.StackTrace);
            }
            return serviceResponse;
        }
        public async Task<ServiceResponse<List<GetAnswerDto>>> GetAnswerByQuestionId(long questionId)
        {
            var serviceResponse = new ServiceResponse<List<GetAnswerDto>>();
            try
            {
                var dbAnswer = await context.Answers.Include(c => c.question).Where(c => c.question.Id == questionId).ToListAsync();
                if (dbAnswer.Count() == 0)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = $"No Answer found with id : {questionId}";
                    throw(new Exception());
                }
                else
                {
                    serviceResponse.Data = dbAnswer!.Select(c => mapper.Map<GetAnswerDto>(c)).ToList();
                }
            }
            catch(AutoMapperMappingException ex)
            {
                logger.LogError("Error in mapping" + ex.StackTrace);
            }
            catch(System.Data.Common.DbException ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Database connection error!";
                logger.LogError(serviceResponse.Message + ex.StackTrace);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                logger.LogError(serviceResponse.Message + ex.StackTrace);
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetAnswerDto>>> GetAnswers()
        {
            var serviceResponse = new ServiceResponse<List<GetAnswerDto>>();
            try
            {
                var dbAnswer = await context.Answers.Include(c => c.option).OrderBy(c => c.Id).ToListAsync();
                if (dbAnswer.Count() == 0)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "No answer has been added yet.";
                    throw(new Exception());
                }
                else
                {
                    serviceResponse.Data = dbAnswer.Select(c => mapper.Map<GetAnswerDto>(c)).ToList();
                }
            }
            catch(AutoMapperMappingException ex)
            {
                logger.LogError("Error in mapping" + ex.StackTrace);
            }
            catch(System.Data.Common.DbException ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Database connection error!";
                logger.LogError(serviceResponse.Message + ex.StackTrace);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                logger.LogError(serviceResponse.Message + ex.StackTrace);
            }
            return serviceResponse;
        }
        public async Task<ServiceResponse<List<GetAnswerDto>>> PostAnswer(AddAnswerDto answerDto)
        {
            var serviceResponse = new ServiceResponse<List<GetAnswerDto>>();
            try
            {
                if (!isOptionIdExits(answerDto.optionId))
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = $"No Option found with option id : {answerDto.optionId}";
                    throw(new Exception());
                }
                else if (!isQuestionIdExits(answerDto.questionId))
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = $"No Question found with option id : {answerDto.questionId}";
                    throw(new Exception());
                }
                var optionsList = context.Options.Where(c => c.isAnswer).ToList();
                if (!optionsList.Any(c => c.Id == answerDto.optionId && c.isAnswer))
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = $"Given option id is not an answer : {answerDto.optionId}";
                    throw(new Exception());
                }
                else 
                {
                    Answer answer = mapper.Map<Answer>(answerDto);
                    answer.answer = context.Options.FirstOrDefault(c => c.Id == answerDto.optionId)!.option;
                    context.Answers.Add(answer);
                    await context.SaveChangesAsync();
                    serviceResponse.Data = await context.Answers
                        .Include(c => c.option)
                        .Where(e => e.option!.Id == answer.optionId)
                        .Select(e => mapper.Map<GetAnswerDto>(e)).ToListAsync();
                }
            }
            catch(AutoMapperMappingException ex)
            {
                logger.LogError("Error in mapping" + ex.StackTrace);
            }
            catch (System.Data.Common.DbException ex)
            {
                serviceResponse.Success = false;
                logger.LogError(serviceResponse.Message + ex.StackTrace);
            }
            catch(Exception ex)
            {
                serviceResponse.Success = false;
                logger.LogError(serviceResponse.Message + ex.StackTrace);
            }
        
            return serviceResponse;
        }
        public async Task<ServiceResponse<GetAnswerDto>> PutAnswer(long id, UpdateAnswerDto answerDto)
        {
            var serviceResponse = new ServiceResponse<GetAnswerDto>();
            try
            {
                Answer dbAnswer = context.Answers
                .Include(c => c.option)
                .FirstOrDefault(c => c.Id == id)!;
                if (dbAnswer.Id == answerDto.Id)
                {
                    dbAnswer.optionId = answerDto.optionId;
                    dbAnswer.answer = answerDto.answer;
                    dbAnswer.questionId = answerDto.questionId;
                    dbAnswer.updatedBy = answerDto.updatedBy;
                    dbAnswer.updatedDate = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
                    context.Answers.Include(c => c.option);
                    await context.SaveChangesAsync();
                    serviceResponse.Data = mapper.Map<GetAnswerDto>(dbAnswer);
                    serviceResponse.Message = "Answer updated successfully!";
                }
                else
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = $"No Answer found with id : {id}";
                    throw(new Exception());
                }
            }
            catch(AutoMapperMappingException ex)
            {
                logger.LogError("Error in mapping" + ex.StackTrace);
            }
            catch(DbUpdateException ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Error in updating the answer!";
                logger.LogError(serviceResponse.Message + ex.StackTrace);
            }
            catch(System.Data.Common.DbException ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Database connection error!";
                logger.LogError(serviceResponse.Message + ex.StackTrace);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                logger.LogError(serviceResponse.Message + ex.StackTrace);
            }
            return serviceResponse;
        }
        private bool isQuestionIdExits(long id)
        {
            var status = false;
            try
            {
                status  = context.Questions.ToList().Any(c => c.Id == id);
            }
            catch(System.Data.Common.DbException ex)
            {
                logger.LogError("Database connection error!" + ex.StackTrace);
            }
            catch(Exception e)
            {
                logger.LogError(e.StackTrace);
            }
            return status;
        }
        private bool isOptionIdExits(long id)
        {
            var status = false;
            try
            {
                status = context.Options.ToList().Any(c => c.Id == id);
            }
            catch(System.Data.Common.DbException ex)
            {
                logger.LogError("Database connection error!" + ex.StackTrace);
            }
            catch(Exception e)
            {
                logger.LogError(e.StackTrace);
            }
            return status;
        }
    }
}