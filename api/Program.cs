using System.Text;
using api.Data;
using api.Repository;
using api.Repository.RepositoryImp;
using api.Services;
using api.Services.ServicesImp;
using api.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContextPool<ApplicationDBContext>(options =>{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddTransient<TokenHelper>();
builder.Services.AddTransient<HandlePassword>();
builder.Services.AddTransient<TokenUtil>();


builder.Services.AddTransient<IStudentRepo, StudentRepo>();
builder.Services.AddTransient<IStudentService, StudentService>();

builder.Services.AddTransient<IMajorRepo, MajorRepo>();
builder.Services.AddTransient<IMajorService, MajorService>();


builder.Services.AddTransient<INewsFeedRepo, NewsFeedRepo>();
builder.Services.AddTransient<INewsFeedService, NewsFeedService>();

builder.Services.AddTransient<INewsFeedRepo, NewsFeedRepo>();
builder.Services.AddTransient<INewsFeedService, NewsFeedService>();

builder.Services.AddTransient<IEducationRepo, EducationRepo>();
builder.Services.AddTransient<IEducationService, EducationService>();

builder.Services.AddTransient<IStudentSubjectRepo, StudentSubjectRepo>();
builder.Services.AddTransient<IStudentSubjectService, StudentSubjectService>();

builder.Services.AddTransient<IExampleScheduleRepo, ExampleScheduleRepo>();
builder.Services.AddTransient<IExampleScheduleService, ExampleScheduleService>();

builder.Services.AddTransient<IExamScheduleStudentRepo, ExamScheduleStudentRepo>();
builder.Services.AddTransient<IExamScheduleSubjectRepo, ExamScheduleSubjectRepo>();

builder.Services.AddTransient<Lazy<IStudentService>>(provider => new Lazy<IStudentService>(provider.GetRequiredService<IStudentService>));
builder.Services.AddTransient<Lazy<IMajorService>>(provider => new Lazy<IMajorService>(provider.GetRequiredService<IMajorService>));
builder.Services.AddTransient<Lazy<IEducationService>>(provider => new Lazy<IEducationService>(provider.GetRequiredService<IEducationService>));
builder.Services.AddTransient<Lazy<ISubjectService>>(provider => new Lazy<ISubjectService>(provider.GetRequiredService<ISubjectService>));
builder.Services.AddTransient<Lazy<INewsFeedService>>(provider => new Lazy<INewsFeedService>(provider.GetRequiredService<INewsFeedService>));
builder.Services.AddTransient<Lazy<IExampleScheduleService>>(provider => new Lazy<IExampleScheduleService>(provider.GetRequiredService<IExampleScheduleService>));
builder.Services.AddTransient<Lazy<IStudentSubjectService>>(provider => new Lazy<IStudentSubjectService>(provider.GetRequiredService<IStudentSubjectService>));






builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
             .GetBytes(builder.Configuration.GetSection("Token:SecretKey").Value)),
            ValidateIssuer = false,
            ValidateAudience = false,

        };  
    });

builder.Services.AddControllers().AddNewtonsoftJson(options =>{
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});


builder.Services.AddAuthorization();
var app = builder.Build();
app.UseAuthentication(); // Kích hoạt middleware xác thực JWT
app.UseAuthorization();
app.MapControllers();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
#if !DEBUG
app.UseHttpsRedirection();
#endif
app.Run();


