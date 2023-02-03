using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineTest.Data;
using OnlineTest.Dto.ExamDto;
using OnlineTest.Models;

namespace OnlineTest.Services.ExamsServices
{
    public class ExamServices : IExamServices
    {
        private readonly OnlineTestDbContext context;
        private readonly IMapper mapper;
        private readonly ILogger<ExamServices> logger;
        public ExamServices(IMapper mapper, OnlineTestDbContext context,ILogger<ExamServices> logger)
        {
            this.mapper = mapper;
            this.context = context;
            this.logger = logger;
        }
        public async Task<ServiceResponse<List<GetExamDto>>> AddExam(AddExamDto newExamDto)
        {
            var serviceResponse = new ServiceResponse<List<GetExamDto>>();
            try
            {
                if(!isExamTypeExits(newExamDto.examTypeId))
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = $"No ExamType Found with id : {newExamDto.examTypeId}";
                    throw(new Exception());
                }
                Exam exam = ConvertTime(newExamDto);
                context.Exams.Add(exam);
                await context.SaveChangesAsync();
                serviceResponse.Data = await context.Exams
                    .Include(c => c.examType)
                    .Where(e => e.examType!.Id == exam.examTypeId)
                    .Select(e => mapper.Map<GetExamDto>(e)).ToListAsync();
            }
            catch(AutoMapperMappingException ex)
            {
                logger.LogError("Error in mapping" + ex.StackTrace);
            }
            catch(FormatException ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Invalid date time format! given format cannot be converted to date time";
                logger.LogError(serviceResponse.Message + ex.StackTrace);   
            }
            catch(System.Data.Common.DbException ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Database connection error!";
                logger.LogError(serviceResponse.Message + ex.StackTrace);
            }
            catch(Exception ex)
            {
                serviceResponse.Success = false;
                logger.LogError(serviceResponse.Message + ex.StackTrace);
            }
            return serviceResponse;
        }
        public async Task<ServiceResponse<List<GetExamDto>>> DeleteExam(long id)
        {
            var serviceResponse = new ServiceResponse<List<GetExamDto>>();
            try
            {
                var exam = await context.Exams.FirstOrDefaultAsync(c => c.Id == id);
                if(exam != null)
                {
                    context.Exams.Remove(exam);
                    await context.SaveChangesAsync();
                    serviceResponse.Data = context.Exams
                        .Include(c => c.examType)
                        .Where(c => c.examType.Id == c.examTypeId)
                        .Select(c => mapper.Map<GetExamDto>(c)).ToList();
                    serviceResponse.Success = true;
                    serviceResponse.Message = $"Exam deleted with id : {id}";
                }
                else
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = $"No Exam found with id : {id}";
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
            catch(Exception ex)
            {
                serviceResponse.Success = false;
                logger.LogError(serviceResponse.Message + ex.StackTrace);
            }
            return serviceResponse;
        }
        public async Task<ServiceResponse<GetExamDto>> GetExam(long id)
        {
            var serviceResponse = new ServiceResponse<GetExamDto>();
            try
            {
                var dbExam = await context.Exams.Include(c => c.examType).FirstOrDefaultAsync(c => c.Id == id);
                if(dbExam == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = $"No Exam found with id : {id}";
                    throw(new Exception());
                }
                else
                {
                    serviceResponse.Data = mapper.Map<GetExamDto>(dbExam);
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
            catch(Exception ex)
            {
                serviceResponse.Success = false;
                logger.LogError(serviceResponse.Message + ex.StackTrace);
            }
            return serviceResponse;
        }
        public async Task<ServiceResponse<List<GetExamDto>>> GetExams()
        {
            var serviceResponse = new ServiceResponse<List<GetExamDto>>();
            try
            {
                var dbExams = await context.Exams.Include(c => c.examType).OrderBy(c => c.Id).ToListAsync();
                if(dbExams.Count() == 0)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "No Exam Found!";
                    throw(new Exception());
                }
                else
                {
                    serviceResponse.Data = dbExams.Select(c => mapper.Map<GetExamDto>(c)).ToList();     
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
            catch(Exception ex)
            {
                serviceResponse.Success = false;
                logger.LogError(serviceResponse.Message + ex.StackTrace);
            }
            return serviceResponse;
        }
        public async Task<ServiceResponse<GetExamDto>> UpdateExam(long id, UpdateExamDto updateExamDto)
        {
            var serviceResponse = new ServiceResponse<GetExamDto>();
            try
            {
                 Exam dbExam = context.Exams
                    .Include(c => c.examType)
                    .FirstOrDefault(c => c.Id == id)!;
                if(dbExam.Id == updateExamDto.Id)
                {
                    dbExam.examTypeId = updateExamDto.examTypeId;
                    dbExam.orgId = updateExamDto.orgId;
                    dbExam.isPublic = updateExamDto.isPublic;
                    dbExam.name = updateExamDto.name;
                    dbExam.updatedBy = updateExamDto.updatedBy;
                    dbExam.updatedDate = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
                    dbExam.startDateTime = Convert.ToDateTime(updateExamDto.startDateTime);
                    dbExam.endDateTime =  Convert.ToDateTime(updateExamDto.endDateTime);
                    context.Exams.Include(c => c.examType);
                    await context.SaveChangesAsync();
                    serviceResponse.Data = mapper.Map<GetExamDto>(dbExam);
                    serviceResponse.Message = "Exam updated successfully!";
                }
                else
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = $"No Exam found with id : {id}";
                    throw(new Exception());
                }
            }
            catch(FormatException ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Invalid date time format! given format cannot be converted to date time";
                logger.LogError(serviceResponse.Message + ex.StackTrace);   
            }
            catch(AutoMapperMappingException ex)
            {
                logger.LogError("Error in mapping" + ex.StackTrace);
            }
            catch(DbUpdateException ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Error in updating the exams";
                logger.LogError(serviceResponse.Message + ex.StackTrace);
            }
            catch(System.Data.Common.DbException ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Database connection error!";
                logger.LogError(serviceResponse.Message + ex.StackTrace);
            }
            catch(Exception ex)
            {
                serviceResponse.Success = false;
                logger.LogError(serviceResponse.Message + ex.StackTrace);
            }
            return serviceResponse;
        }
        private bool isExamTypeExits(long id)
        {
            var status = false;
            try
            {
                status = context.ExamTypes.ToList().Any(c => c.Id == id);
            }
            catch(System.Data.Common.DbException ex)
            {
                logger.LogError(ex.StackTrace);
            }
            return status;
        }

        private Exam ConvertTime(AddExamDto newExamDto)
        {
            DateTime startDateTime = Convert.ToDateTime(newExamDto.startDateTime);
            DateTime endDateTime = Convert.ToDateTime(newExamDto.endDateTime);
            return new Exam(){
                        name = newExamDto.name,
                        startDateTime = startDateTime,
                        endDateTime = endDateTime,
                        isPublic = newExamDto.isPublic,
                        orgId = newExamDto.orgId,
                        examTypeId = newExamDto.examTypeId
                    };
        }
    }
}