using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Courses.Application.Interfaces;
using Courses.Application;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Courses.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Courses.Domain.Services;
using Courses.Domain.Entities;
using Courses.Domain.Interfaces;
using Courses.Domain;
using System.Text;
using Courses.Infra.Data.Repositories;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Swashbuckle.AspNetCore.Swagger;


namespace Courses.WebApi
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
            services.AddMvc().AddJsonOptions(
                options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
            services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new Info() {Title = "Courses API", Description = "Swagger Web API Courses"});
                });
            var sqlConnection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ProjectCourseContext>(options => options.UseSqlServer(sqlConnection, b => b.MigrationsAssembly("Courses.Infra.Data")));
            services.AddTransient<ICourseAppService, CourseAppService>();
            services.AddTransient<IServiceCourse, CourseService>();
            services.AddTransient<IAppServiceBase<Courses.Domain.Entities.Course>, AppServiceBase<Courses.Domain.Entities.Course>>();
            services.AddTransient<IServiceBase<Courses.Domain.Entities.Course>, ServiceBase<Courses.Domain.Entities.Course>>();
            services.AddScoped<IRepositoryBase<Courses.Domain.Entities.Course>, RepositoryBase<Courses.Domain.Entities.Course>>();
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<ITeacherRepository, TeacherRepository>();
            services.AddScoped<IStudentRepository, StudentRepository>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Core API"); });

        }
    }
}
