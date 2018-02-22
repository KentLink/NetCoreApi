using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MDES.Api.Model.Test;
using MDES.Api.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace MDES.Api
{
    public class Startup
    {
        //public Startup(IConfiguration configuration)
        //{
        //    Configuration = configuration;
        //}

        public IConfiguration Configuration { get; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                        .SetBasePath(env.ContentRootPath)
                        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                        .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                        .AddEnvironmentVariables();
            Configuration = builder.Build();

            //var siteName = Configuration["Default"];
        }


        public void ConfigureServices(IServiceCollection services)
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
        }




        // This method gets called by the runtime. Use this method to add services to the container.
        //public void ConfigureServices(IServiceCollection services)
        //{
        //    services.AddMvc();
        //}

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
