using Infrastructure.DI;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(options =>
            {
                options.Filters.Add<ExceptionHandlerFilterAttribute>(1);
                options.Filters.Add<AuthorizationAttribute>(1);
                options.Filters.Add<ValidateModelAttribute>(2);
            });

            services.AddHttpContextAccessor();

            IocManager.Init(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            //app.UseWebSockets();

            //app.UseMiddleware<ChannelWSMiddleware>();//通道中间件
            app.UseMiddleware<ProfileMiddleware>();//性能中间件
            app.UseMiddleware<LogMiddleware>();//日志中间件

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
