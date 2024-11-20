using Asp.Versioning;
using Asp.Versioning.Conventions;
using AutoMapper;
using BUE.Inscriptions.Application.Implementation;
using BUE.Inscriptions.Application.Interfaces;
using BUE.Inscriptions.Domain.AppSettings;
using BUE.Inscriptions.Infrastructure;
using BUE.Inscriptions.Infrastructure.Interfaces;
using BUE.Inscriptions.Infrastructure.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
builder.Services.AddSingleton(mapper);
#region REPOSITORIES
//builder.Services.AddScoped<IQueryBuilderPortal, QueryBuilderPortal>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IStudentPortalRepository, StudentPortalRepository>();
builder.Services.AddScoped<IStudentBUERepository, StudentBUERepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IGenaralBueRepository, GenaralBueRepository>();
builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
builder.Services.AddScoped<IContactRepository, ContactRepository>();
builder.Services.AddScoped<IContactPortalRepository, ContactPortalRepository>();
builder.Services.AddScoped<IStudentRepresentativeRepository, StudentRepresentativeRepository>();
builder.Services.AddScoped<IApplicationRepository, ApplicationRepository>();
builder.Services.AddScoped<IFileDownloadRepository, FileDownloadRepository>();
builder.Services.AddScoped<IAuthorizePersonRepository, AuthorizePersonRepository>();
builder.Services.AddScoped<IPortalRequestRepository, PortalRequestRepository>();
builder.Services.AddScoped<IGenaralPortalRepository, GenaralPortalRepository>();
builder.Services.AddScoped<IFinanceInformationRepository, FinanceInformationRepository>();
builder.Services.AddScoped<IAwsS3UploaderRepository, AwsS3UploaderRepository>();
builder.Services.AddScoped<IFireBaseRepository, FireBaseRepository>();
builder.Services.AddScoped<IMedicalRecordRepository, MedicalRecordRepository>();


// elections
builder.Services.AddScoped<IAcademicYearRepository, AcademicYearRepository>();
builder.Services.AddScoped<IElectionRepository, ElectionRepository>();
builder.Services.AddScoped<IPositionRepository, PositionRepository>();
builder.Services.AddScoped<ICandidateRepository, CandidateRepository>();
builder.Services.AddScoped<ICandidateElectionRepository, CandidateElectionRepository>();
builder.Services.AddScoped<IVoteRepository,VoteRepository>();
builder.Services.AddScoped<IOrganizationVoteRepository, OrganizationVoteRepository>();
builder.Services.AddScoped<IOrganizationRepository, OrganizationRepository>();
builder.Services.AddScoped<ICandidateOrganizationRepository, CandidateOrganizationRepository>();
builder.Services.AddScoped<IOrganizationElectionRepository, OrganizationElectionRepository>();

//v2
builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IStudentFamiliesRepository, StudentFamiliesRepository>();
builder.Services.AddScoped<IInscriptionRepository, InscriptionRepository>();
builder.Services.AddScoped<IPaymentInformationRepository, PaymentInformationRepository>();
//builder.Services.AddScoped<IOrganizationRepository, OrganizationRepository>();
#endregion
#region SERVICES
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IStudentBUEService, StudentBUEService>();
builder.Services.AddScoped<IStudentPortalService, StudentPortalService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IGeneralBueService, GeneralBueService>();
builder.Services.AddScoped<IMailNotification, MailNotificationService>();
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<IContactService, ContactService>();
builder.Services.AddScoped<IContactPortalService, ContactPortalService>();
builder.Services.AddScoped<IStudentRepresentativeService, StudentRepresentativeService>();
builder.Services.AddScoped<IApplicationService, ApplicationService>();
builder.Services.AddScoped<IFileDownloadService, FileDownloadService>();
builder.Services.AddScoped<IAuthorizePersonService, AuthorizePersonService>();
builder.Services.AddScoped<IAuthorizePersonService, AuthorizePersonService>();
builder.Services.AddScoped<IPortalRequestService, PortalRequestService>();
builder.Services.AddScoped<IGeneralPortalService, GeneralPortalService>();
builder.Services.AddScoped<IReportService, ReportService>();
builder.Services.AddScoped<IFinanceInformationService, FinanceInformationService>();
builder.Services.AddScoped<IStudentRepresentativeRepository, StudentRepresentativeRepository>();
builder.Services.AddScoped<IManagementFileCloudService, ManagementFileCloudService>();
builder.Services.AddScoped<IMedicalRecordService, MedicalRecordService>();

//services
builder.Services.AddScoped<IAcademicYearService, AcademicYearService>();
builder.Services.AddScoped<IElectionService, ElectionService>();
builder.Services.AddScoped<IPositionService, PositionService>();
builder.Services.AddScoped<ICandidateService, CandidateService>();
builder.Services.AddScoped<ICandidateElectionService, CandidateElectionService>();
builder.Services.AddScoped<IOrganizationService, OrganizationService>();
builder.Services.AddScoped<ICandidateOrganizationService, CandidateOrganizationService>();
builder.Services.AddScoped<IVoteService, VoteService>();
builder.Services.AddScoped<IOrganizationVoteService, OrganizationVoteService>();
builder.Services.AddScoped<IOrganizationElectionService, OrganizationElectionService>();
// v2
builder.Services.AddScoped<IPersonService, PersonService>();
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<IStudentFamiliesService, StudentFamiliesService>();
builder.Services.AddScoped<IInscriptionService, InscriptionService>();
builder.Services.AddScoped<IPaymentInformationService, PaymentInformationService>();
builder.Services.AddScoped<IContractService, ContractService>();
builder.Services.AddScoped<IExcelReportService, ExcelReportService>();  
#endregion
// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor();
builder.Services.AddDbContext<PortalMatriculasDBContext>();
builder.Services.AddDbContext<BueDBContext>();
builder.Services.AddApiVersioning(
    options =>
    {
        options.ReportApiVersions = true;
        options.AssumeDefaultVersionWhenUnspecified = false;
        options.DefaultApiVersion = new ApiVersion(1, 0);
        options.ApiVersionReader = new UrlSegmentApiVersionReader();
    })
    .AddApiExplorer(
        options =>
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        })
    .AddMvc(
      options =>
      {
          options.Conventions.Add(new VersionByNamespaceConvention());
      });
builder.Services.AddSwaggerGen(options =>
    {
        options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
        {
            Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")",
            In = ParameterLocation.Header,
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey
        });
        options.OperationFilter<SecurityRequirementsOperationFilter>();
        options.SwaggerDoc("v1", new OpenApiInfo { Title = "BUE.INSCRIPTIONS.PUBLIC", Version = "v1" });
        options.SwaggerDoc("v2", new OpenApiInfo { Title = "BUE.INSCRIPTIONS.PUBLIC", Version = "v2" });
    });
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
    });
// cors policy
var allowedOrigins = builder.Configuration.GetSection("AllowedOrigins").Get<string[]>();
builder.Services.AddCors(o => o.AddPolicy("LowCorsPolicy", builder =>
{
    builder.AllowAnyOrigin()
        .WithOrigins(allowedOrigins)
        .SetIsOriginAllowed(isOriginAllowed: _ => true)
        .AllowCredentials()
        .AllowAnyHeader()
        .AllowAnyMethod();
}));
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI(
    options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
        options.SwaggerEndpoint("/swagger/v2/swagger.json", "API v2");
    });
}
app.UseHttpsRedirection();
app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Resources")),
    RequestPath = new PathString("/Resources")
});
app.UseCors("LowCorsPolicy");
//app.UseCors(LowAllowSpecificOrigins);
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
