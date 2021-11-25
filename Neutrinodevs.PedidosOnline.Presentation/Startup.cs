using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Neutrinodevs.PedidosOnline.Domain.Contracts.Repositories;
using Neutrinodevs.PedidosOnline.Domain.Contracts.Services;
using Neutrinodevs.PedidosOnline.Domain.Services;
using Neutrinodevs.PedidosOnline.Infraestructure.Helpers;
using Neutrinodevs.PedidosOnline.Infraestructure.Hubs.Hubs;
using Neutrinodevs.PedidosOnline.Infraestructure.Models;
using Neutrinodevs.PedidosOnline.Infraestructure.Repositories;
using System;

namespace Neutrinodevs.PedidosOnline.Presentation
{
    public class Startup
    {
        private IHostingEnvironment _environment;
        
        public Startup(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            Configuration = configuration;


            _environment = hostingEnvironment;

            var builder = new ConfigurationBuilder()
                .SetBasePath(_environment.ContentRootPath)
                .AddJsonFile("appsetting.json")
                .AddEnvironmentVariables();
            
            //Configuration = builder.Build();
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

            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromHours(1);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });


            services.AddSingleton(Configuration);
            services.AddDbContext<ND_PEDIDOS_ONLINEContext>(options =>
            {
                options.UseSqlServer(Configuration.GetSection("ConnectionStrings:pedidos_online").Value);
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });

            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<UserRepository, UserRepository>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<DishRepository, DishRepository>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IDeliveryRepository, DeliveryRepository>();

            var mapperConfig = new MapperConfiguration(m => {
                m.AddProfile(new MappingProfile());
            });
            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddSignalR();
            services.AddDistributedMemoryCache();
            services.AddSession(options => {
                options.IdleTimeout = TimeSpan.FromMinutes(10);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

           

            
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddFile("Logs/Log-{Date}.txt");

          
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseSession();

            app.UseMvc(path => {
                path.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}"    
                );
            });

            app.UseSignalR(hub => {
                hub.MapHub<OrderHub>("/OrdersHub");
            });
        }
    }
}
