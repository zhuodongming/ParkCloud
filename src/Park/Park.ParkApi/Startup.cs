using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.DI;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Park.Entity.Base;
using Park.ParkApi.Filters;
using Park.ParkApi.Middleware;

namespace Park.ParkApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddMvc(options =>
            {
                options.Filters.Add<ExceptionHandlerFilterAttribute>(1);
                options.Filters.Add<AuthorizationAttribute>(1);
                options.Filters.Add<ValidateModelAttribute>(2);
            })
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.Configure<ConnectionStrings>(Configuration.GetSection("ConnectionStrings"));

            IocManager.Init(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseWebSockets();

            //app.UseMiddleware<ChannelWSMiddleware>();//通道中间件
            app.UseMiddleware<ProfileMiddleware>();//性能中间件
            app.UseMiddleware<LogMiddleware>();//日志中间件

            app.UseMvc();
            //app.UseMvc(config =>
            //{
            //    config.MapRoute(
            //        name: "default",
            //        template: "auth/{controller=Auth}/{action=Index}/{id?}");
            //});
        }
    }
}
