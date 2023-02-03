using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineTest.Data;
using OnlineTest.Dto.ExamTypeDto;
using OnlineTest.Models;

namespace OnlineTest.Services.ExamTypeServices
{
    public class ExamTypeService : IExamTypeService
    {
        private readonly OnlineTestDbContext context;
        private readonly IMapper mapper;
        private readonly ILogger<ExamTypeService> logger;
        public ExamTypeService(IMapper mapper, OnlineTestDbContext context, ILogger<ExamTypeService> logger)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.context = context;
        }
        public async Task<ServiceResponse<List<GetExamTypeDto>>> AddExamType(AddExamTypeDto addExamType)
        {
            var serviceResponse = new ServiceResponse<List<GetExamTypeDto>>();
            try
            {
                ExamType examType = mapper.Map<ExamType>(addExamType);
                if (isExamTypeByNameExists(addExamType.type!))
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = $"{addExamType.type} already exists in database.";
                    throw(new Exception());
                }
                else
                {
                    context.ExamTypes.Add(examType);
                    await context.SaveChangesAsync();
                    serviceResponse.Data = await context.ExamTypes
                        .Select(e => mapper.Map<GetExamTypeDto>(e)).ToListAsync();
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
                logger.LogError("Error in mapping" + ex.StackTrace);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                logger.LogError(serviceResponse.Message + ex.StackTrace);
            }
            return serviceResponse;
        }
        public async Task<ServiceResponse<List<GetExamTypeDto>>> DeleteExamType(long id)
        {
            var serviceResponse = new ServiceResponse<List<GetExamTypeDto>>();
            try
            {
                var examType = await context.ExamTypes.FirstOrDefaultAsync(c => c.Id == id);
                if (examType != null)
                {
                    context.ExamTypes.Remove(examType);
                    await context.SaveChangesAsync();
                    serviceResponse.Data = context.ExamTypes
                        .Select(c => mapper.Map<GetExamTypeDto>(c)).ToList();
                    serviceResponse.Success = true;
                    serviceResponse.Message = $"ExamType deleted with id : {id}";
                }
                else
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = $"No ExamType found with id : {id}";
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

        public async Task<ServiceResponse<List<GetExamTypeDto>>> GetAllExamTypes()
        {
            var serviceResponse = new ServiceResponse<List<GetExamTypeDto>>();
            try
            {
                var dbExamTypes = await context.ExamTypes.ToListAsync();
                if (dbExamTypes.Count() == 0)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = $"No ExamType found!";
                    throw(new Exception());
                }
                else
                {
                    serviceResponse.Data = dbExamTypes.Select(c => mapper.Map<GetExamTypeDto>(c)).OrderBy(c => c.Id).ToList();
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
        public async Task<ServiceResponse<GetExamTypeDto>> GetExamTypeById(long id)
        {
            var serviceResponse = new ServiceResponse<GetExamTypeDto>();
            try
            {
                var examType = await context.ExamTypes.FindAsync(id);
                if (examType == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = $"No ExamType found with id : {id}";
                    throw(new Exception());
                }
                else
                {
                    serviceResponse.Data = mapper.Map<GetExamTypeDto>(examType);
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
        public async Task<ServiceResponse<GetExamTypeDto>> UpdateExamType(long id, UpdateExamTypeDto updateExamType)
        {
            var serviceResponse = new ServiceResponse<GetExamTypeDto>();
            try
            {
                var dbExamType = context.ExamTypes.FirstOrDefault(c => c.Id == id)!;
                if (isExamTypeByNameExists(updateExamType.type!))
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = $"{updateExamType.type} already exists in database.";
                    throw(new Exception());
                }
                else if (dbExamType.Id == updateExamType.Id)
                {
                    dbExamType.type = updateExamType.type;
                    dbExamType.createdBy = updateExamType.createdBy;
                    dbExamType.updatedBy = updateExamType.updatedBy;
                    dbExamType.updatedDate = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
                    await context.SaveChangesAsync();
                    serviceResponse.Data = mapper.Map<GetExamTypeDto>(dbExamType);
                    serviceResponse.Message = "ExamType updated successfully!";
                }
                else
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = $"No ExamType found with id : {id}";
                    throw(new Exception());
                }
            }
            catch(AutoMapperMappingException ex)
            {
                logger.LogError("Error in mapping" + ex.StackTrace);
            }
            catch(DbUpdateException ex)
            {
                logger.LogError("Error in updating the exam types" + ex.StackTrace);
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
        private bool isExamTypeByNameExists(string examTypeName)
        {
            var status = false;
            try
            {
                status = context.ExamTypes.ToList().Any(c => String.Equals(c.type, examTypeName));
            }
            catch(System.Data.Common.DbException ex)
            { 
                logger.LogError("Database connection error!" + ex.StackTrace);
            }
            catch(AutoMapperMappingException ex)
            {
                logger.LogError("Error in mapping" + ex.StackTrace);
            }
            catch(Exception ex)
            {
                logger.LogError(ex,ex.StackTrace);
            }
            return status;
        }
    }
}