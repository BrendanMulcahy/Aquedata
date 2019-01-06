using Aquedata.Validator.Controller;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace Aquedata
{
    public class Startup
    {
        private const string ServiceName = "Aquedata";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddHangfire(x =>
                x.UseSqlServerStorage(@"Server=.; Database=Hangfire; Integrated Security=SSPI;",
                    new SqlServerStorageOptions
                    {
                        SchemaName = ServiceName,
                        PrepareSchemaIfNecessary = true
                    }));

            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new Info {Title = ServiceName, Version = "v1"}); });

            services.AddSingleton<IRequestValidator, RequestValidator>();
            services.AddSingleton<IValidationJobFactory, ValidationJobFactory>();

            services.AddSingleton(Configuration);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHangfireServer();
            app.UseHangfireDashboard();

            app.UseHttpsRedirection();
            app.UseMvc();

            app.UseSwagger();

            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", ServiceName); });
        }
    }
}