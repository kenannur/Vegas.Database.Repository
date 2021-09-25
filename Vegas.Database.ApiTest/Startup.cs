﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Vegas.Database.DynamoDB.DependencyInjection;
using Vegas.Database.DynamoDB.Setting;

namespace Vegas.Database.ApiTest
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDynamoAsyncRepository(new DynamoDBSettings
            {
                AccessKey = Configuration.GetValue<string>("DynamoDBSettings:AccessKey"),
                SecretKey = Configuration.GetValue<string>("DynamoDBSettings:SecretKey"),
                Region = Configuration.GetValue<string>("DynamoDBSettings:Region"),
            });

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Vegas.Database.ApiTest", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Vegas.Database.ApiTest v1"));
            }

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
