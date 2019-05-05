using AutoMapper;
using Consul;
using HospitalService.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

[assembly: ApiController]
namespace HospitalService
{
    public class StartupOptions
    {
        public string DoctorHelpRestUrl { get; set; }
        public string RedisServiceName { get; set; }
        public string ConsulRestUrl { get; set; }
    }

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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.Configure<StartupOptions>(Configuration);
            services.AddStackExchangeRedisCache(options =>
            {
                var consulResturl = Configuration.GetValue<string>("ConsulRestUrl");
                using (var consul = new ConsulClient(x => x.Address = new System.Uri(consulResturl)))
                {
                    var redisServiceName = Configuration.GetValue<string>("RedisServiceName");
                    var redisServices = consul.Catalog.Service(redisServiceName).GetAwaiter().GetResult().Response;
                    options.Configuration = string.Join(",", redisServices.Select(x => $"{x.ServiceAddress}:{x.ServicePort}"));
                }
            });
            services.AddOptions<StartupOptions>();
            services.AddSingleton<IHospitalStorageService, HospitalStorageService>();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new DomainProfile());
            });

            var mapper = config.CreateMapper();
            services.AddSingleton(mapper);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
