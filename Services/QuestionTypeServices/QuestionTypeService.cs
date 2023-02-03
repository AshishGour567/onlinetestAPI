using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineTest.Data;
using OnlineTest.Dto.QuestionTypeDto;
using OnlineTest.Models;

namespace OnlineTest.Services.QuestionTypeServices
{
    public class QuestionTypeService : IQuestionTypeService
    {
        private readonly IMapper mapper;
        private readonly OnlineTestDbContext context;
        private readonly ILogger<QuestionTypeService> logger;

        public QuestionTypeService(IMapper mapper, OnlineTestDbContext context, ILogger<QuestionTypeService> logger)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.context = context;
        }
        public async Task<ServiceResponse<List<GetQuestionTypeDto>>> DeleteQuestionType(long id)
        {
            var serviceResponse = new ServiceResponse<List<GetQuestionTypeDto>>();
            try
            {
                var questionType = await context.QuestionTypes.FirstOrDefaultAsync(c => c.Id == id);
                if (questionType != null)
                {
                    context.QuestionTypes.Remove(questionType);
                    await context.SaveChangesAsync();
                    serviceResponse.Data = context.QuestionTypes.Select(c => mapper.Map<GetQuestionTypeDto>(c)).ToList();
                    serviceResponse.Success = true;
                    serviceResponse.Message = $"Question type deleted with id : {id}";
                }
                else
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = $"No Question type found with id : {id}";
                    throw(new Exception());
                }
            }
            catch(System.Data.Common.DbException ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Database connection error!";
                logger.LogError(serviceResponse.Message + ex.StackTrace);
            }
            catch(AutoMapperMappingException ex)
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
        public async Task<ServiceResponse<GetQuestionTypeDto>> GetQuestionTypeById(long id)
        {
            var serviceResponse = new ServiceResponse<GetQuestionTypeDto>();
            try
            {
                var dbQuestionType = await context.QuestionTypes.FirstOrDefaultAsync(c => c.Id == id);
                if (dbQuestionType == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = $"No Question type found with id : {id}";
                    throw(new Exception());
                }
                else
                {
                    serviceResponse.Data = mapper.Map<GetQuestionTypeDto>(dbQuestionType);
                }
            }
            catch(System.Data.Common.DbException ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Database connection error!";
                logger.LogError(serviceResponse.Message + ex.StackTrace);
            }
            catch(AutoMapperMappingException ex)
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
        public async Task<ServiceResponse<List<GetQuestionTypeDto>>> GetQuestionTypes()
        {
            var serviceResponse = new ServiceResponse<List<GetQuestionTypeDto>>();
            try
            {
                var dbQuestionTypes = await context.QuestionTypes.ToListAsync();
                if (dbQuestionTypes.Count() == 0)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = $"No Question type found!";
                    throw(new Exception());
                }
                else
                {
                    serviceResponse.Data = dbQuestionTypes.Select(c => mapper.Map<GetQuestionTypeDto>(c)).ToList();
                }
            }
            catch(System.Data.Common.DbException ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Database connection error!";
                logger.LogError(serviceResponse.Message + ex.StackTrace);
            }
            catch(AutoMapperMappingException ex)
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
        public async Task<ServiceResponse<List<GetQuestionTypeDto>>> PostQuestionType(AddQuestionTypeDto addQuestionTypeDto)
        {
            var serviceResponse = new ServiceResponse<List<GetQuestionTypeDto>>();
            try
            {
                var questionType = mapper.Map<QuestionType>(addQuestionTypeDto);
                if (isQuestionTypeNameExits(addQuestionTypeDto.type!))
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = $"{addQuestionTypeDto.type} is already present in database";
                    throw(new Exception());
                }
                else
                {
                    context.QuestionTypes.Add(questionType);
                    await context.SaveChangesAsync();
                    serviceResponse.Data = await context.QuestionTypes.Select(e => mapper.Map<GetQuestionTypeDto>(e)).ToListAsync();
                }
            }
            catch(System.Data.Common.DbException ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Database connection error!";
                logger.LogError(serviceResponse.Message + ex.StackTrace);
            }
            catch(AutoMapperMappingException ex)
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
        public async Task<ServiceResponse<GetQuestionTypeDto>> PutQuestionType(long id, UpdateQuestionTypeDto updateQuestionTypeDto)
        {
            var serviceResponse = new ServiceResponse<GetQuestionTypeDto>();
            try
            {
                var dbQuestionType = context.QuestionTypes.FirstOrDefault(c => c.Id == id)!;
                if (isQuestionTypeNameExits(updateQuestionTypeDto.type!))
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = $"{updateQuestionTypeDto.type} is already present in database";
                    throw(new Exception());
                }
                else if (dbQuestionType.Id == updateQuestionTypeDto.Id)
                {
                    dbQuestionType.updatedBy = updateQuestionTypeDto.updatedBy;
                    dbQuestionType.type = updateQuestionTypeDto.type;
                    dbQuestionType.updatedDate = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
                    await context.SaveChangesAsync();
                    serviceResponse.Data = mapper.Map<GetQuestionTypeDto>(dbQuestionType);
                    serviceResponse.Message = "Question type updated successfully!";
                }
                else
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = $"No Question type found with id : {id}";
                    throw(new Exception());
                }
            }
            catch(System.Data.Common.DbException ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Database connection error!";
                logger.LogError(serviceResponse.Message + ex.StackTrace);
            }
            catch(AutoMapperMappingException ex)
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
        private bool isQuestionTypeNameExits(string questionTypeName)
        {
            bool value = false;
            try
            {
               value = context.QuestionTypes.ToList().Any(c => String.Equals(questionTypeName, c.type));
            }
            catch(System.Data.Common.DbException ex)
            {
                logger.LogError(ex.Message + ex.StackTrace);
            }
            return value;
        }
    }
}