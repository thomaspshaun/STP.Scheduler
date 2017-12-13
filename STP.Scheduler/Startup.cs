using Hangfire;
using Hangfire.MySql;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System;

namespace STP.Scheduler
{
  public class Startup
  {

    public IConfigurationRoot Configuration { get; }

    public Startup(IHostingEnvironment env)
    {
      var builder = new ConfigurationBuilder()
          .SetBasePath(env.ContentRootPath)
          .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
          .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
          .AddEnvironmentVariables();

      Configuration = builder.Build();
    }


    // This method gets called by the runtime. Use this method to add services to the container.
    // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
    public void ConfigureServices(IServiceCollection services)
    {

      services.AddMvc();
      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc(
          "version 1.0",
          new Info
          {
            Title = "STP Scheduler",
            Version = "verson 1.0"
          });
        c.CustomSchemaIds(x => x.FullName);
      });

      services.AddHangfire(x =>
      {
        x.UseStorage(new MySqlStorage(Configuration.GetConnectionString("hangfire")));
      });

     
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      app.UseSwagger();


      // Enable middleware to serve swagger-ui (HTML, JS, CSS etc.), specifying the Swagger JSON endpoint.
      app.UseSwaggerUI(c =>
      {
        c.SwaggerEndpoint("/swagger/version 1.0/swagger.json", "Refunds Orchestrator Service Version 1.0");
      });


      // Hangfire Config
      var options = new DashboardOptions
      {
        AppPath = null,
        Authorization = new[] { new HangfireAuthorizationFilter() }
      };

      app.UseHangfireServer(
          new BackgroundJobServerOptions()
          {
            WorkerCount = 1,
            ServerName = "STP.Scheduler:" + Environment.MachineName,
            Queues = new[] { "scheduler" }
          });

      app.UseHangfireDashboard("/scheduler", options);

      GlobalJobFilters.Filters.Add(new SkipWhenPreviousJobIsRunningAttribute());
    }
  }
}
