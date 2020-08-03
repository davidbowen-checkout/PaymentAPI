using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bank.Core.Api;
using Bank.Core.Api.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Payment.Gateway.Api.Controllers;
using Payment.Gateway.Api.Database;
using Payment.Gateway.Api.DTO;
using Payment.Gateway.Api.Interface;
using Payment.Gateway.Api.Logging;
using Payment.Gateway.Api.Service;

namespace Payment.Gateway.Api
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
            services.AddSingleton(typeof(ILogger<PaymentController>) , typeof(PaymentLogger<PaymentController>));
            services.AddSingleton(typeof(IBank), typeof(ExampleBank));

            services.AddTransient(typeof(IRepository<PaymentData>), typeof(PaymentsDatabaseRepository));

            services.AddSingleton(typeof(IPaymentUniqueIDBuilder<PaymentData>), typeof(PaymentMd5UniqueId));

            services.AddTransient(typeof(IPaymentService<PaymentData>), typeof(PaymentService));

            services.AddDbContext<PaymentsContext>();





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
