using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

Env.Load();
var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("Connection String Not Found");
}

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies()); // Mapper injection
builder.Services.AddOpenApi();
builder.Services.AddControllers();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

#region  Dependency Injections
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IAuthorityService, AuthorityService>();
builder.Services.AddScoped<IPersonelService, PersonelService>();
builder.Services.AddScoped<IStageService, StageSerevice>();
builder.Services.AddScoped<IUnitService, UnitService>();
builder.Services.AddScoped<IWorkService, WorkService>();
builder.Services.AddScoped<ILoginService, LoginService>();
#endregion

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Work API",
        Version = "v1"
    });

    options.OperationFilter<FileUploadOperation>();
});

builder.Services.AddCors(option =>
{
    option.AddDefaultPolicy(policy =>
    {
        policy
        .AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

var app = builder.Build();

// 🔥 STATİK DOSYA ERİŞİMİNİ AKTİF ET 🔥
app.UseStaticFiles(); // wwwroot içindeki statik dosyaları sunar

// Eğer PDF dosyaları doğrudan "uploads/" klasöründe saklanıyorsa:
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads")), // wwwroot/uploads
    RequestPath = "/uploads"
});

app.UseRouting();
app.UseCors();
app.UseAuthorization();
app.UseHttpsRedirection();
app.UseSwagger();
app.UseSwaggerUI();
app.MapOpenApi();
app.MapControllers();

app.Run();
