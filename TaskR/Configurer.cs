using Microsoft.EntityFrameworkCore;
using TaskR.DB;
using TaskR.Repository;

namespace TaskR;

public class Configurer
{
    private readonly IServiceCollection _serviceCollection;
    private readonly WebApplication _applicationBuilder;

    public Configurer(IServiceCollection serviceCollection,
        WebApplication applicationBuilder)
    {
        _serviceCollection = serviceCollection;
        _applicationBuilder = applicationBuilder;
    }

    public void BuildServices()
    {
        _serviceCollection.AddCors(options => options.AddPolicy("TaskrPolicy",
            policyBuilder =>
            {
                policyBuilder.AllowAnyHeader()
                    .AllowAnyOrigin()
                    .AllowAnyMethod();
            }));
        
        //Add db connection
        _serviceCollection.AddDbContext<TaskrContext>(options =>
        {
            var connectionString =
                _applicationBuilder.Configuration.GetConnectionString("taskr");
            options.UseMySql(connectionString,
                    ServerVersion.AutoDetect(connectionString));
        });
        
        //Add repositories
        _serviceCollection.AddScoped<ITaskrRepository, TaskrRepository>();

        _serviceCollection.AddLogging(builder =>
        {
            builder.AddFilter("Microsoft.EntityFrameworkCore.Database.Command",
                LogLevel.Warning);
        });
    }

    public void ConfigureApp()
    {
        //Setup redirection
        _applicationBuilder.UseHttpsRedirection();
        _applicationBuilder.UseCors("TaskrPolicy");
        _applicationBuilder.UseRouting();
        _applicationBuilder.UseAuthorization();
        _applicationBuilder.MapControllers();
        
        //Setup endpoints if need be.
    }
}