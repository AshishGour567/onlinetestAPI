using Microsoft.EntityFrameworkCore;
using OnlineTest.Data;
using OnlineTest.Services.AnswerServices;
using OnlineTest.Services.ExamsServices;
using OnlineTest.Services.ExamTypeServices;
using OnlineTest.Services.OptionsServices;
using OnlineTest.Services.QuestionServices;
using OnlineTest.Services.QuestionTypeServices;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IExamServices, ExamServices>();
builder.Services.AddScoped<IExamTypeService,ExamTypeService>();
builder.Services.AddScoped<IOptionsService,OptionsService>();
builder.Services.AddScoped<IQuestionService, QuestionService>();
builder.Services.AddScoped<IQuestionTypeService, QuestionTypeService>();
builder.Services.AddScoped<IAnswerService, AnswerService>();
builder.Services.AddAutoMapper(typeof(Program));
//Configure the logger (serilog)
var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(configuration)
    .Enrich.FromLogContext()
    .CreateLogger();
builder.Host.UseSerilog();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//services cors
builder.Services.AddCors(p => p.AddPolicy(name:"AllowOrigin", builder =>
{
    builder.AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader();
}));
var serverVersion = new MySqlServerVersion(new Version(8,0,28));
builder.Services.AddDbContext<OnlineTestDbContext>(options =>
options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),serverVersion,
option => option.EnableRetryOnFailure(
    maxRetryCount : 5,
    maxRetryDelay : System.TimeSpan.FromSeconds(30),
    errorNumbersToAdd: null)
));
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseSerilogRequestLogging();

app.UseHttpsRedirection();
//app cors
app.UseCors("AllowOrigin");

app.UseAuthorization();

app.MapControllers();

app.Run();