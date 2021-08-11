using Infrastructure.DependencyInjection.Extensions;
using Infrastructure.DependencyInjection.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace WebAPI
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(options
                => options.SwaggerDoc(
                    name: "v1",
                    info: new()
                    {
                        Title = "WebAPI",
                        Version = "v1"
                    }));

            services.AddApplicationServices();

            services.AddEventStoreRepositories();
            services.AddProjectionsRepositories();

            services.AddEventStoreDbContext();
            services.AddProjectionsDbContext();
            
            services.AddMassTransitWithRabbitMq(options
                => _configuration.Bind(nameof(RabbitMqOptions), options));

            services.ConfigureEventStoreOptions(
                _configuration.GetSection(nameof(EventStoreOptions)));

            services.ConfigureMongoDbOptions(
                _configuration.GetSection(nameof(MongoDbOptions)));

            services.ConfigureSqlServerRetryingOptions(
                _configuration.GetSection(nameof(SqlServerRetryingOptions)));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(options
                    => options.SwaggerEndpoint("/swagger/v1/swagger.json", "WebAPI v1"));
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints
                => endpoints.MapControllers());
        }
    }
}