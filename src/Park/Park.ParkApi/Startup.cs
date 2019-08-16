using Infrastructure.DI;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Park.Entity;
using Park.UI.Filter;
using Park.UI.Middleware;

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
                options.Filters.Add<AuthorizationFilter>(1);
            });

            services.AddHttpContextAccessor();

            //配置Settings
            Configuration.LoadSettings<ConnectionStrings>();

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
            //app.UseMiddleware<ChannelMiddleware>();//通道中间件

            app.UseMiddleware<HttpContextBodyBufferingMiddleware>();//设置Request/Response.Body可重复读取
            app.UseMiddleware<LogMiddleware>();//日志中间件

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
