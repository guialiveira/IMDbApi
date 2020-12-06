using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebIMDb.Data;
using Microsoft.AspNetCore.Rewrite;

namespace WebIMDb
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options => options.AddDefaultPolicy(builder =>
            {
                builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
            }));

            services.AddControllers();

            services.AddDbContext<WebIMDbContext>(options =>
                    options.UseMySql(Configuration.GetConnectionString("WebIMDbContext"),

                    builder =>
                       builder.MigrationsAssembly("WebIMDb")));

            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1",
                    new Microsoft.OpenApi.Models.OpenApiInfo
                {
                        Title = "REST API",
                        Version = "v1",
                        Description = "API simulando o site IMDb para classificação de filmes",
                        Contact = new Microsoft.OpenApi.Models.OpenApiContact
                        {
                            Name = "Guilherme Alves de Oliveira",
                            Url = new Uri("https://www.linkedin.com/in/guilherme-alves-498074b1/")
                        }
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors();

            app.UseSwagger();

            app.UseSwaggerUI(c =>{
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "REST API");
            });

            var option = new RewriteOptions();
            option.AddRedirect("^$","swagger");

            app.UseRewriter(option);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
