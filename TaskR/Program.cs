using System.Diagnostics;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using TaskR;
using TaskR.DB;
using TaskR.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "TaskR",
        Version = "v1",
        Description = "API for creating and displaying tasks"
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

builder.Services.AddScoped<ITaskrRepository, TaskrRepository>();

builder.Services.AddCors(options => options.AddPolicy("TaskrPolicy",
    policyBuilder =>
    {
        policyBuilder.AllowAnyHeader()
            .AllowAnyOrigin()
            .AllowAnyMethod();
    })
);

// builder.Services.AddDbContext<TaskrContext>(options =>
// {
//     var connectionString =
//         builder.Configuration.GetConnectionString("taskr");
//     options.UseMySql(connectionString,
//         ServerVersion.AutoDetect(connectionString));
// });
builder.Services.AddDbContext<TaskrContext>(options =>
{
    Console.WriteLine($"{builder.Configuration.GetConnectionString("taskr_inmem")}");
    options.UseInMemoryDatabase("task_in_mem" ??
                                throw (new Exception("Could not find connection string!")));
});

var app = builder.Build();

SeedDb.SeedTaskrDB(app);
// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "TaskR V1");});

app.UseHttpsRedirection();
app.UseCors("TaskrPolicy");

app.UseRouting();
// app.UseAuthorization();
app.MapControllers();

app.Run();
