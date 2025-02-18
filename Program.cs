using DotNetEnv;
using Microsoft.EntityFrameworkCore;




var builder = WebApplication.CreateBuilder(args);
Env.Load();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies()); // mapper injection
builder.Services.AddOpenApi();
builder.Services.AddControllers();


#region  Dependecy Injections
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IAuthorityService, AuthorityService>();
builder.Services.AddScoped<IPersonelService, PersonelService>();
builder.Services.AddScoped<IStageService, StageSerevice>();
builder.Services.AddScoped<IUnitService, UnitService>();
builder.Services.AddScoped<IWorkService, WorkService>();
builder.Services.AddScoped<ILoginService, LoginService>();
#endregion

var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
if(string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("Connection String Not Found");
}

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

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



// builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
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

app.UseCors();
app.MapOpenApi();
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.MapControllers();
app.UseRouting();
app.UseAuthorization();
app.UseCors();



app.Run();


