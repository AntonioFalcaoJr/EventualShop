using Application.DependencyInjection.Extensions;
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
                    info: new() {Title = "WebAPI", Version = "v1"}));

            services.AddMediatR();
            services.AddEventStore();
            services.AddRepositories();
            services.AddEventStoreDbContext();

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