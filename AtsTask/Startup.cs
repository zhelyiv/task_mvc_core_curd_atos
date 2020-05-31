using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Db.Context.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper; 
using DAL;
using DAL.Contracts;
using ViewModels;

namespace AtsTask
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
             
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
             
            services.AddDbContext<AtsTestContext>
                (options => options.UseSqlServer(AtsTestContext.TestConnectionString));

            services.AddAutoMapper(mc => {
                mc.AddProfile(new ViewModelMapperProfile());
            });

            services.AddScoped<ICrudOperation<BlogViewModel>, BlogOperations>();
            services.AddScoped<ICrudOperation<CommentViewModel>, CommentOperations>();
            services.AddScoped<ICrudOperation<PostViewModel>, PostOperations>();
            services.AddScoped<ICrudOperation<TagViewModel>, TagOperations>();
            services.AddScoped<ICrudOperation<UserViewModel>, UserOperations>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,
            AtsTestContext dbContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();

            dbContext.Database.Migrate();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Users}/{action=Index}/{id?}");
            });
        }
    }
}
