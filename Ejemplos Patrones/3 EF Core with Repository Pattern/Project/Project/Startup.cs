using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Lib.Core;
using Common.Lib.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Project.Lib.DAL.EFCore.Context;
using Project.Web.App;

namespace Project
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
            services.AddControllers();

            var dbConnection = Configuration.GetConnectionString("ProjectDb");
            services.AddDbContext<ProjectDbContext>(options => options.UseSqlite(dbConnection, b => b.MigrationsAssembly("Project.Web")));

            var bootstrapper = new Bootstrapper();
            Entity.DepCon = new SimpleDependencyContainer();

            bootstrapper.Init(Entity.DepCon, GetDbConstructor(dbConnection));

        }

        private static Func<ProjectDbContext> GetDbConstructor(string dbConnection)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ProjectDbContext>();
            optionsBuilder.UseSqlite(dbConnection, b => b.MigrationsAssembly("Project.Web"));

            var dbContextConst = new Func<ProjectDbContext>(() =>
            {                
                return new ProjectDbContext(optionsBuilder.Options);
            });
            return dbContextConst;
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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
