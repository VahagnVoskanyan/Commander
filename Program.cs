using Commander.Data;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

//Changed
builder.Services.AddControllers().AddNewtonsoftJson(s =>
{
    s.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Uncomment this for SQL Server and comment the Mock implementation
//builder.Services.AddScoped<ICommanderRepo, SqlCommanderRepo>();
builder.Services.AddScoped<ICommanderRepo, MockCommanderRepo>();

//Get Config string from appsettings.json
builder.Services.AddDbContext<CommanderContext>(opt => opt.UseSqlServer
                            (builder.Configuration.GetConnectionString("CommanderConnection")));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
