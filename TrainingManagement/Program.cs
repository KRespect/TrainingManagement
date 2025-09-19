using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;
using TrainingManagement;
using SqlSugar;
using TrainingManagement.Data;
using TrainingManagement.Service.Interfaces;
using TrainingManagement.Service.Implementations;
using TrainingManagement.Service;
using Microsoft.AspNetCore.Authentication;
using TrainingManagement.Filters;

var builder = WebApplication.CreateBuilder(args);

// 添加全局过滤器
builder.Services.AddControllers(options =>
{
    options.Filters.Add<ApiResponseFilter>();
});

// 添加Serilog日志配置
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();

// 添加服务


builder.Services.AddControllers();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<IQuestionnaireService, QuestionnaireService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITrainingService, TrainingService>();
builder.Services.AddScoped<IAttachmentService, AttachmentService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => 
{
    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "TrainingManagement.xml"));
});

// 添加JWT认证
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var secretKey = jwtSettings["SecretKey"];
var issuer = jwtSettings["Issuer"];
var audience = jwtSettings["Audience"];

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = issuer,
        ValidAudience = audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey))
    };
});

// 添加SqlSugar服务
SqlSugarConfig.ConfigureSqlSugar(builder.Services, builder.Configuration);

// 添加异常处理
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

var app = builder.Build();

// 初始化数据库
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ISqlSugarClient>();
    DatabaseInitializer.InitializeDatabase(db);
}

// 配置HTTP请求管道
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();