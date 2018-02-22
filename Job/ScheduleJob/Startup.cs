using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Utility.Log;
using MDES.Api.Model.Test;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ScheduleJob.Code.Scheduling;

namespace ScheduleJob
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

            try
            {
                //Skip …
                var configManager = new ConfigManager();
                Configuration.GetSection("Config").Bind(configManager);
                //ConfigProvider.Default = configManager.Default;
                //ConfigProvider.MDESUrl = configManager.MDESUrl;

                Type configProviderType = typeof(ConfigProvider);
                Type temType = typeof(ConfigManager);

                int idx = 0;
                foreach (var prop in temType.GetProperties())
                {
                    configProviderType.GetProperties()[idx].SetValue(prop.Name, prop.GetValue(configManager));
                    idx++;
                }

                services.AddMvc();

                services.AddScheduler((sender, args) =>
                {
                    Console.Write(args.Exception.Message);
                    args.SetObserved();
                });
                LogUtility.Info();
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex.ToString());
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
