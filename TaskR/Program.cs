using TaskR;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Add services to the container.
var configurer = new Configurer(builder.Services, app);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

configurer.BuildServices();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

configurer.ConfigureApp();

app.Run();
