using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.gRPC.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();
var connStr = builder.Configuration.GetConnectionString("CareerCloudConString");
builder.Services.AddDbContext<CareerCloudContext>(options =>
{
    options.UseSqlServer(connStr);
});
var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<ApplicantEducationService>();
app.MapGrpcService<ApplicantJobApplicationService>();
app.MapGrpcService<ApplicantProfileService>();
app.MapGrpcService<CompanyDescriptionService>();
app.MapGrpcService<CompanyJobEducationService>();
app.MapGrpcService<CompanyJobService>();
app.MapGrpcService<SecurityLoginService>();
app.MapGrpcService<SecurityLoginsLogService>();
app.MapGrpcService<SystemLanguageCodeService>();

app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
